using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;
using UI.Dialogs;
using Util;
using Object = UnityEngine.GameObject;
using UnityEngine.UI;

namespace GHPluginCoreLib
{
    class VentanaPatcher
    {
		/*
		Temporary patch which should be removed only once underlying issues have been
		completely resolved.
		*/
        public static bool Awake_PrefixPatch(Ventana __instance)
        {
			MonoBehaviour baseInstance = __instance;

			__instance.dialogo = baseInstance.GetComponentInParent<uDialog>();

			__instance.clipboard = Object.FindObjectOfType<Clipboard>();

			__instance.mouseCursor = Object.FindObjectOfType<MouseCursor>();

			PlayerComputer pc = PlayerClient.Singleton.player.pc;

			__instance.remoteNetID = pc.GetID();

			__instance.activeUser = (__instance.prevActiveUser = pc.GetUsers(true)[1].nombreUsuario);

			__instance.PID = pc.AddLocalProcess();

			Transform iconMaximizeImageTransform = OS.FindTransform(__instance.dialogo.transform, "Maximize Button");

			if (iconMaximizeImageTransform != null)
			{
				Image[] iconMaximizeImageTransformImageComponents = iconMaximizeImageTransform.GetComponentsInChildren<Image>();

				if (iconMaximizeImageTransformImageComponents.Length >= 2)
				{
					__instance.iconMaximizeImage = iconMaximizeImageTransformImageComponents[1];

					__instance.maximizeIcon = __instance.iconMaximizeImage.sprite;
				}
			}

			__instance.restoreIcon = Object.FindObjectOfType<Icons>().GetIcon("restorewindow", true);

			/*
			These 4 fields must be set as their default values are different and
			can cause issues with animations not playing due to incorrect animation names.
			*/
			__instance.dialogo.ShowAnimation = eShowAnimation.Grow;

			__instance.dialogo.RestoreAnimation = eShowAnimation.Grow;

			__instance.dialogo.CloseAnimation = eCloseAnimation.Shrink;

			__instance.dialogo.MinimizeAnimation = eCloseAnimation.Shrink;

			return false;
		}
    }
}
