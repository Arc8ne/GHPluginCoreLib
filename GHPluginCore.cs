using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using Component = UnityEngine.Component;
using System.Reflection;
using System.Collections.ObjectModel;
using System.CodeDom;
using static Miniscript.Intrinsic;

namespace GHPluginCoreLib
{
	/// <summary>
	/// The GHPluginCore class can be inherited from by your BepInEx plugin, however it is
	/// not mandatory for your BepInEx plugin to inherit from this class.
	/// The GHPluginCore class is responsible for implementing and exposing the functionality
	/// that this library offers (e.g. the ability to create and implement custom
	/// programs that can be used in GreyOS). It can be accessed through its "instance" field.
	/// </summary>
    public class GHPluginCore
    {
        private const string harmonyID = "gh.plugin.core";

        private Harmony harmony = new Harmony(harmonyID);

		internal GHPluginLogger logger = new GHPluginLogger("GH Plugin Core");

		/// <summary>
		/// An instance of the GHPluginCore class which can be used to access instances of
		/// other classes like the GreyOSProgramManager.
		/// </summary>
		public static readonly GHPluginCore instance = new GHPluginCore();

		/// <summary>
		/// An instance of the GreyOSProgramManager, which allows you to register custom
		/// programs that can be executed and used in GreyOS, the operating system which
		/// a player's in-game computer runs on.
		/// </summary>
		public readonly GreyOSProgramManager greyOSProgramManager = new GreyOSProgramManager();

        private GHPluginCore()
        {

        }

		private void ApplyPatchWithPatchType(MethodInfo targetMethodInfo, MethodInfo patchMethodInfo, PatchTypes patchType)
		{
			this.logger.LogInfo("Patching " + targetMethodInfo.Name);

			switch (patchType)
			{
				case PatchTypes.Prefix:
					harmony.Patch(
						original: targetMethodInfo,
						prefix: new HarmonyMethod(patchMethodInfo)
					);

					break;

				case PatchTypes.Postfix:
					harmony.Patch(
						original: targetMethodInfo,
						postfix: new HarmonyMethod(patchMethodInfo)
					);

					break;

				case PatchTypes.Transpiler:
					harmony.Patch(
						original: targetMethodInfo,
						transpiler: new HarmonyMethod(patchMethodInfo)
					);

					break;

				case PatchTypes.Finalizer:
					harmony.Patch(
						original: targetMethodInfo,
						finalizer: new HarmonyMethod(patchMethodInfo)
					);

					break;

				default:
					break;
			}

			this.logger.LogInfo("Patched " + targetMethodInfo.Name);
		}

        private void ApplyPatch(MethodInfo targetMethodInfo, MethodInfo patchMethodInfo, PatchTypes patchType)
        {
			ReadOnlyCollection<Patch> targetMethodPatches = null;

			if (Harmony.GetPatchInfo(targetMethodInfo) == null)
			{
				this.ApplyPatchWithPatchType(
					targetMethodInfo,
					patchMethodInfo,
					patchType
				);

				return;
			}

			switch (patchType)
			{
				case PatchTypes.Prefix:
					targetMethodPatches = Harmony.GetPatchInfo(
						targetMethodInfo
					).Prefixes;

					break;

				case PatchTypes.Postfix:
					targetMethodPatches = Harmony.GetPatchInfo(
						targetMethodInfo
					).Postfixes;

					break;

				case PatchTypes.Transpiler:
					targetMethodPatches = Harmony.GetPatchInfo(
						targetMethodInfo
					).Transpilers;

					break;

				case PatchTypes.Finalizer:
					targetMethodPatches = Harmony.GetPatchInfo(
						targetMethodInfo
					).Finalizers;

					break;

				default:
					break;
			}

			foreach (Patch targetMethodPatch in targetMethodPatches)
			{
				if (targetMethodPatch.owner == harmonyID)
				{
					return;
				}
			}

			this.ApplyPatchWithPatchType(
				targetMethodInfo,
				patchMethodInfo,
				patchType
			);
		}


		private void ApplyCorePatches()
        {
			this.ApplyPatch(
				AccessTools.Method(
					typeof(Ventana),
					nameof(Ventana.Awake)
				),
				AccessTools.Method(
					typeof(VentanaPatcher),
					nameof(VentanaPatcher.Awake_PrefixPatch),
					new Type[]
					{
						typeof(Ventana)
					}
				),
				PatchTypes.Prefix
			);

			this.ApplyPatch(
				AccessTools.Method(
					typeof(Terminal),
					nameof(Terminal.InicializarComandos)
				),
				AccessTools.Method(
					typeof(TerminalPatcher),
					nameof(TerminalPatcher.InicializarComandos_PostfixPatch),
					new Type[]
					{
						typeof(Terminal)
					}
				),
				PatchTypes.Postfix
			);

			this.ApplyPatch(
				AccessTools.Method(
					typeof(PlayerServerMethods),
					nameof(PlayerServerMethods.PrepareCommandServerRpc),
					new Type[]
					{
						typeof(string),
						typeof(byte[]),
						typeof(string),
						typeof(string),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(bool)
					}
				),
				AccessTools.Method(
					typeof(PlayerServerMethodsPatcher),
					nameof(PlayerServerMethodsPatcher.PrepareCommandServerRpc_PrefixPatch),
					new Type[]
					{
						typeof(string),
						typeof(byte[]),
						typeof(string),
						typeof(string),
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(bool),
						typeof(PlayerServerMethods)
					}
				),
				PatchTypes.Prefix
			);

			this.ApplyPatch(
				AccessTools.Method(
					typeof(PlayerServerMethods),
					nameof(PlayerServerMethods.UserLogin),
					new Type[]
					{
						typeof(ulong),
						typeof(bool)
					}
				),
				AccessTools.Method(
					typeof(PlayerServerMethodsPatcher),
					nameof(PlayerServerMethodsPatcher.UserLogin_PostfixPatch),
					new Type[]
					{
						typeof(ulong),
						typeof(bool),
						typeof(PlayerServerMethods)
					}
				),
				PatchTypes.Postfix
			);
		}

		/// <summary>
		/// This method must be called in the Awake method of your BepInEx plugin,
		/// preferably at the start of it if possible. It is responsible for executing crucial
		/// initialization procedures to ensure that the library works correctly.
		/// </summary>
		public void Init()
		{
			this.ApplyCorePatches();

			this.logger.LogInfo("All methods patched successfully.");
		}

		/*
		For future reference (Do not remove):

        Programs are stored as files on the player's computer's FileSystem, all files
        (including these program files) are represented as instances of the
        FileSystem.Archivo class, which inherits from the FileSystem.Fichero class.

        Note: PlayerClient.Singleton.pc can be used to retrieve the player's computer
        (represented as an instance of the PlayerComputer class).

        Key classes/fields/methods of interest found during previous analysis:
        -> PlayerServerMethods.PrepareCommandServerRpc
        -> Computer.AddProcess
        -> FileSystem.AddFileRef (Used for adding files to a Computer's FileSystem)
        -> PlayerClientMethods.SendCommandClientRpc
        -> PlayerServerMethods.StartProgram
        -> Database.RestorePlayerComputer
        -> PlayerServerMethods.UserLogin
        -> PlayerComputer.ConfigureStartupProcess
        -> PlayerUtils.CrearArchivo
        -> Database.RestoreComputer
        -> Database.RestorePlayerComputer
        -> FileSystem.Deserializar
        -> Terminal.InicializarComandos

        Note: In singleplayer mode, the actual filesystem of the player's computer is stored
        in the "fileSystem" field of a PlayerComputer instance stored in a PlayerServerMethods
        instance (which can be accessed through a PlayerServer instance).

        Note: The PlayerServerMethods.UserLogin method is called once during the start of the
        boot sequence of the player's computer.

        Note: The Terminal.InicializarComandos method is called once during the boot sequence
        of the player's computer as well as once every time the player opens a new Terminal.

        The steps to adding custom programs to the GreyOS which can be run from the Terminal
        in the player's computer are as follows:
        -> Add the program file associated with the custom program to the player's computer's
        filesystem (represented as instance of the FileSystem class) (this should be done
        just after the "UserLogin" method of the PlayerServerMethods class is called, this
        can be done using a postfix patch or through other means if there are any).
        -> Add an instance of the custom program (for the regular programs that are already
        a part of GreyOS, such as the File Explorer and Browser programs, they are represented
        as instances of the ProgramWindow class, for custom programs, they are represented
        as an instance of the GreyOSProgram class which inherits from the ProgramWindow class)
        to the "programs" field of the Terminal instance (this should be done after the
        "InicializarComandos" method of the Terminal class is called, this method adds
        the programs that come with GreyOS by default to the "programs" field of the Terminal
        instance).
        */
	}
}
