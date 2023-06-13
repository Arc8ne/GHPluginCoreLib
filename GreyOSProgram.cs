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
    public class GreyOSProgram : ProgramWindow
    {
        protected GameObject programPrefab = null;

		public string programName = "DefaultGreyOSProgram";

        public FileSystem.Archivo associatedFile = null;

		private void InitGreyOSProgramPrefabFromAssetBundle(string associatedAssetBundleFilePath, string programPrefabName)
		{
			AssetBundle associatedAssetBundle = AssetBundle.LoadFromFile(associatedAssetBundleFilePath);

			programPrefab = associatedAssetBundle.LoadAsset<GameObject>(programPrefabName);

			Object.DontDestroyOnLoad(programPrefab);

			associatedAssetBundle.Unload(false);
		}

		public GreyOSProgram(string programName, string associatedAssetBundleFilePath, string programPrefabName) : base(programName.Replace(" ", "") + ".exe", programName)
        {
            this.programName = programName;

            InitGreyOSProgramPrefabFromAssetBundle(
                associatedAssetBundleFilePath,
                programPrefabName
            );
        }

		public GreyOSProgram(string programName, GameObject programPrefab) : base(programName.Replace(" ", "") + ".exe", programName)
		{
			this.programName = programName;

			this.programPrefab = programPrefab;
		}

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
