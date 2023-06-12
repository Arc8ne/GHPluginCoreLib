using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GHPluginCoreLib
{
	public class GHPluginUtilities
	{
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
