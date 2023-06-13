using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using UnityEngine.UI;
using UI.Dialogs;
using UnityEngine;
using ProgramVisual;
using Object = UnityEngine.Object;
using HarmonyLib;

namespace GHPluginCoreLib
{
    /// <summary>
    /// The GreyOSProgram class is the class from which custom programs that are meant
    /// to be run on a player's in-game computer must inherit from.
    /// </summary>
    public class GreyOSProgram : ProgramWindow
    {
        /// <summary>
        /// The prefab associated with the program.
        /// </summary>
        protected GameObject programPrefab = null;

        /// <summary>
        /// The name of the program.
        /// </summary>
		public string programName = "DefaultGreyOSProgram";

        /// <summary>
        /// The file associated with the program, this field will automatically be assigned
        /// to at runtime provided that the program has been registered with the
        /// GreyOSProgramManager.
        /// </summary>
        public FileSystem.Archivo associatedFile = null;

		private void InitGreyOSProgramPrefabFromAssetBundle(string associatedAssetBundleFilePath, string programPrefabName)
		{
			AssetBundle associatedAssetBundle = AssetBundle.LoadFromFile(associatedAssetBundleFilePath);

			programPrefab = associatedAssetBundle.LoadAsset<GameObject>(programPrefabName);

			Object.DontDestroyOnLoad(programPrefab);

			associatedAssetBundle.Unload(false);
		}

        /// <summary>
        /// The default constructor method for the GreyOSProgram class.
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="associatedAssetBundleFilePath"></param>
        /// <param name="programPrefabName"></param>
		public GreyOSProgram(string programName, string associatedAssetBundleFilePath, string programPrefabName) : base(programName.Replace(" ", "") + ".exe", programName)
        {
            this.programName = programName;

            InitGreyOSProgramPrefabFromAssetBundle(
                associatedAssetBundleFilePath,
                programPrefabName
            );
        }

        /// <summary>
        /// A constructor for the GreyOSProgram class.
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="programPrefab"></param>
		public GreyOSProgram(string programName, GameObject programPrefab) : base(programName.Replace(" ", "") + ".exe", programName)
		{
			this.programName = programName;

			this.programPrefab = programPrefab;
		}

        /// <summary>
        /// This method can be overridden by the program's class provided that it
        /// inherits from the GreyOSProgram class. This method is called when a player
        /// opens the program either through the File Explorer or the Terminal.
        /// </summary>
        /// <param name="comando"></param>
        /// <param name="currentFile"></param>
        /// <param name="PID"></param>
        /// <param name="terminal"></param>
        /// <returns></returns>
		public override string Procesar(string[] comando, FileSystem.Archivo currentFile, int PID, Terminal terminal)
        {
			if (PID == -1)
            {
                return "";
            }
            
            this.taskBar = Object.FindObjectOfType<uDialog_TaskBar>();
            
            this.dialog = uDialog.NewDialog(programPrefab, this.taskBar.transform.parent.GetComponent<RectTransform>());

            this.dialog.gameObject.AddComponent<Ventana>();

            this.ventana = this.dialog.GetComponentInChildren<Ventana>();
            
            if (this.ventana != null)
            {
                this.ventana.SetPathProgram(currentFile.GetRuta(true));
                
                this.dialog.SetVisibleOnStart(false);
                
                int mainTerminalPID = terminal.IsSubterminal() ? terminal.GetMainTerminalID() : terminal.GetPID();
                
                if (terminal.IsInternalBash() && terminal.GetParentID() > 0)
                {
                    mainTerminalPID = terminal.GetParentID();
                }
                
                this.ventana.SetPID(PID, mainTerminalPID);
            }

            this.title = this.programName;
            
            this.dialog.SetTitleText(this.title, false);
            
            this.ConnectToRemoteComputer(comando, terminal, this.title);
            
            return "";
        }
    }
}
