using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GHPluginCoreLib
{
	/// <summary>
	/// The GHPluginUtilities class exposes several helper methods for BepInEx plugin
	/// developers to use.
	/// </summary>
	public class GHPluginUtilities
	{
		/// <summary>
		/// Returns the Component of one of the children of the GameObject specified in the
		/// first parameter with the same name as the one specified in the second parameter.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <param name="componentName"></param>
		/// <returns></returns>
		public static T GetComponentInGameObjectByName<T>(GameObject gameObject, string componentName)
		{
			foreach (Component component in gameObject.GetComponentsInChildren<Component>())
			{
				if (component is T && component.name == componentName)
				{
					return (T)Convert.ChangeType(component, typeof(T));
				}
			}

			return default;
		}
	}
}
