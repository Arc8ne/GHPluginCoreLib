using HarmonyLib;

namespace GHPluginCoreLib
{
    class TerminalPatcher
    {
        public static void InicializarComandos_PostfixPatch(Terminal __instance)
        {
			GHPluginCore.instance.logger.LogInfo("Initiating GreyOSProgram command registration procedure...");

			foreach (GreyOSProgram registeredGreyOSProgram in GHPluginCore.instance.greyOSProgramManager.registeredGreyOSPrograms)
			{
				__instance.programs.Add(
					registeredGreyOSProgram
				);

				GHPluginCore.instance.logger.LogInfo("Registered command for GreyOSProgram with name: " + registeredGreyOSProgram.programName + " successfully.");
			}

			GHPluginCore.instance.logger.LogInfo("GreyOSProgram command registration procedure completed successfully.");
		}
	}
}
