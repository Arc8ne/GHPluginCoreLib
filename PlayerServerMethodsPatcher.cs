using System;
using System.Collections.Generic;
using System.Linq;
using CompressString;
using HarmonyLib;
using NetworkMessages;
using Util;
using Newtonsoft.Json;

namespace GHPluginCoreLib
{
	class PlayerServerMethodsPatcher
	{
		public static bool PrepareCommandServerRpc_PrefixPatch(string pathCurrentFolder, byte[] zipComandoCompleto, string propActiveUser, string prevConnIp, int numBounces, int terminalPID, int parentPID, bool shellFtp, PlayerServerMethods __instance)
		{
			// Original implementation code
			int num = (parentPID > 0) ? parentPID : terminalPID;
			bool flag = false;
			Computer.Procesos proceso = __instance.pc.GetProceso(num);
			string text;
			
			if (num == -10)
			{
				text = __instance.pc.GetID();
			}
			else
			{
				if (proceso == null)
				{
					__instance.CloseConnection(terminalPID, XmlGlobal.GetTexto("ERROR_UNKPROC", false, false), false);
					
					return false;
				}
				text = proceso.GetRemoteNetID();
				flag = proceso.isProtected;
			}
			
			string[] array;

			try
			{
				array = JsonConvert.DeserializeObject<string[]>(StringCompressor.Unzip(zipComandoCompleto));
			}
			catch
			{
				__instance.CloseConnection(terminalPID, "Unknown error: invalid command.", false);
				
				return false;
			}
			if (!Networking.IsNetworkActive(__instance.player.GetComputer().GetPublicIP()) && !__instance.player.GetComputer().GetID().Equals(text))
			{
				__instance.CloseConnection(terminalPID, "Error: No internet connection", false);

				return false;
			}
			Computer remoteComputer = RedGlobal.Singleton.GetRemoteComputer(text, false, true, "", -1);
			if (remoteComputer == null)
			{
				__instance.CloseConnection(terminalPID, "Error: device not found.", false);

				return false;
			}
			if (shellFtp)
			{
				__instance.RunFtpCommand(array, pathCurrentFolder, propActiveUser, remoteComputer, terminalPID, prevConnIp, numBounces);
				return false;
			}
			List<string> list = new List<string>();
			list.Add(pathCurrentFolder);
			list.Add("/bin");
			list.Add("/usr/bin");
			List<string> list2 = list;
			FileSystem fileSystem = remoteComputer.GetFileSystem();
			string activeUser = __instance.userConnections.GetActiveUser(remoteComputer.GetID(), propActiveUser);
			Computer.User user = remoteComputer.GetUser(activeUser);
			FileSystem.Archivo archivo = null;

			for (int i = 0; i < list2.Count; i++)
			{
				if (fileSystem.GetCarpeta(list2[i], null) != null)
				{
					archivo = fileSystem.GetArchivo(list2[i] + "/" + array[0]);

					if (archivo != null)
					{
						if (!archivo.IsBinario())
						{
							__instance.CloseConnection(terminalPID, archivo.nombre + " is not a binary file.", false);

							return false;
						}
						if (archivo.IsTypeFile(FileSystem.Fichero.TypeFile.Script))
						{
							if (num == -10 || terminalPID == -10 || !proceso.isTerminal)
							{
								__instance.CloseConnection(terminalPID, "Error: script is not attached to any existing terminal", false);

								return false;
							}
							if (Enumerable.Contains<string>(new string[]
							{
							"dsession",
							"kernel_task",
							"xorg",
							"ssh_enc"
							}, array[0].ToLower()))
							{
								__instance.CloseConnection(terminalPID, "Error: " + array[0] + " is a reserved process name", false);

								return false;
							}
							__instance.RunScript(remoteComputer, array, archivo.GetRuta(true), pathCurrentFolder, propActiveUser, terminalPID, prevConnIp, numBounces);
							return false;
						}
						else
						{
							try
							{
								if (archivo.GetNombreProceso().Equals("cd"))
								{
									__instance.CdTerminal(array, pathCurrentFolder, remoteComputer, activeUser, terminalPID);
									return false;
								}
								if (!archivo.IsEjecutable())
								{
									__instance.CloseConnection(terminalPID, archivo.nombre + " is not an executable file.", false);

									return false;
								}
								/*
								TODO: Fix bug causing exception to be thrown when the
								TienePermisoEjecucion method is called as seen below.
								*/
								/*
								if (!archivo.TienePermisoEjecucion(user))
								{
									__instance.CloseConnection(terminalPID, "Can't launch program. Permission denied.", false);

									ModCore.instance.LogInfo("Can't launch program. Permission denied.");

									return false;
								}
								*/
							}
							catch(Exception exception)
							{
								GHPluginCore.instance.logger.LogInfo("Exception occurred.");

								GHPluginCore.instance.logger.LogInfo("Exception message: " + exception.Message);

								GHPluginCore.instance.logger.LogInfo("Exception stack trace:");

								GHPluginCore.instance.logger.LogInfo(exception.StackTrace);
							}

							break;
						}
					}
				}
			}

			if (archivo == null)
			{
				__instance.CloseConnection(terminalPID, string.Join(" ", array) + ": command not found", false);

				return false;
			}

			byte[] value = null;
			int value2 = -1;
			string comando = archivo.GetComando();

			if (comando.Contains(".exe"))
			{
				if (flag)
				{
					__instance.CloseConnection(terminalPID, "Error: Desktop GUI is not running.", false);

					return false;
				}
				if ((remoteComputer.IsRouter() || remoteComputer.IsCctv()) && !PlayerUtils.IsAllowedCommand(comando))
				{
					__instance.CloseConnection(terminalPID, "Error: this program can only be run on computers.", false);

					return false;
				}
				if (comando.Equals("Chat.exe") && !__instance.pc.GetID().Equals(remoteComputer.GetID()))
				{
					__instance.CloseConnection(terminalPID, "Error: this program can not be executed remotely", false);

					return false;
				}

				value2 = __instance.StartProgram(remoteComputer, archivo, activeUser, true, terminalPID, comando, out value);
			}

			byte[] value3 = StringCompressor.Zip(JsonConvert.SerializeObject(archivo));
			MessageClient messageClient = new MessageClient(IdClient.SendCommandClientRpc);
			messageClient.AddInt(terminalPID);
			messageClient.AddByte(value3);
			messageClient.AddString(archivo.GetRuta(false));
			messageClient.AddInt(value2);
			messageClient.AddByte(value);
			__instance.player.SendData(messageClient);

			return false;
		}

		public static void UserLogin_PostfixPatch(ulong steamID, bool forceDeletePlayer, PlayerServerMethods __instance)
		{
			GHPluginCore.instance.logger.LogInfo("Initiating GreyOSProgram filesystem registration procedure...");

			foreach (GreyOSProgram registeredGreyOSProgram in GHPluginCore.instance.greyOSProgramManager.registeredGreyOSPrograms)
			{
				GHPluginCore.instance.logger.LogInfo("Registration started for GreyOSProgram with name: " + registeredGreyOSProgram.programName);

				try
				{
					FileSystem.Archivo greyOSProgramFile = (FileSystem.Archivo)AccessTools.CreateInstance(typeof(FileSystem.Archivo));

					greyOSProgramFile.SetNombre(
						registeredGreyOSProgram.programName.Replace(" ", "") + ".exe"
					);

					greyOSProgramFile.SetComando(
						"/usr/bin/" + registeredGreyOSProgram.programName.Replace(" ", "") + ".exe"
					);

					greyOSProgramFile.isProtected = false;

					greyOSProgramFile.permisos = new FileSystem.Permisos("-rwxr-xr-x");

					greyOSProgramFile.allowImport = false;

					greyOSProgramFile.isBinario = true;

					greyOSProgramFile.isEditedOtherPlayer = false;

					greyOSProgramFile.precio = 0;

					__instance.pc.GetFileSystem().AddFileRef(
						greyOSProgramFile,
						"/usr/bin"
					);
				}
				catch (Exception exception)
				{
					GHPluginCore.instance.logger.LogInfo("Exception occurred while attempting to register GreyOSProgram: " + exception.Message);

					GHPluginCore.instance.logger.LogInfo("Exception stack trace:");

					GHPluginCore.instance.logger.LogInfo(exception.StackTrace);
				}

				GHPluginCore.instance.logger.LogInfo("Registered GreyOSProgram with name: " + registeredGreyOSProgram.programName + " to filesystem successfully.");
			}

			GHPluginCore.instance.logger.LogInfo("GreyOSProgram filesystem registration procedure completed successfully.");
		}
	}
}
