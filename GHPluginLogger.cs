using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using BepInEx.Logging;

namespace GHPluginCoreLib
{
	public class GHPluginLogger
	{
		private ManualLogSource logger = null;

		public GHPluginLogger(string sourceName = "GH Plugin Logger")
		{
			this.logger = new ManualLogSource(sourceName);
		}

		public GHPluginLogger(ManualLogSource logger)
		{
			this.logger = logger;
		}

		public void LogInfo(string msg)
		{
			this.logger.LogInfo(msg);
		}

		public void LogAllGameObjectData(GameObject gameObject, bool logDataOfAllChildGameObjects, List<string> propertyNamesFilter = null)
		{
			this.logger.LogInfo("Current GameObject: " + gameObject);

			if (gameObject.transform.parent != null)
			{
				this.logger.LogInfo("Current GameObject parent: " + gameObject.transform.parent.gameObject);
			}
			else
			{
				this.logger.LogInfo("This GameObject does not have a parent.");
			}

			this.logger.LogInfo("Properties of current GameObject:");

			if (propertyNamesFilter == null)
			{
				foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(gameObject))
				{
					this.logger.LogInfo(propertyDescriptor.Name + " = " + propertyDescriptor.GetValue(gameObject));
				}
			}
			else
			{
				foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(gameObject))
				{
					if (propertyNamesFilter.Contains(propertyDescriptor.Name) == true)
					{
						this.logger.LogInfo(propertyDescriptor.Name + " = " + propertyDescriptor.GetValue(gameObject));
					}
				}
			}

			this.logger.LogInfo("GameObject transform position: " + gameObject.transform.position.ToString());

			if (gameObject.GetComponent<Canvas>() != null)
			{
				this.logger.LogInfo("GameObject canvas sort-order: " + gameObject.GetComponent<Canvas>().sortingOrder);
			}

			this.logger.LogInfo("");

			if (logDataOfAllChildGameObjects == true)
			{
				for (int currentChildIndex = 0; currentChildIndex < gameObject.transform.childCount; currentChildIndex++)
				{
					this.LogAllGameObjectData(gameObject.transform.GetChild(currentChildIndex).gameObject, logDataOfAllChildGameObjects);
				}
			}
		}

		public void LogSceneData(Scene scene, bool logAllSceneObjectsData)
		{
			foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(scene))
			{
				this.logger.LogInfo(propertyDescriptor.Name + " = " + propertyDescriptor.GetValue(scene));
			}

			this.logger.LogInfo("IsValid = " + scene.IsValid());

			if (logAllSceneObjectsData == true)
			{
				foreach (GameObject rootGameObject in scene.GetRootGameObjects())
				{
					this.LogAllGameObjectData(rootGameObject, true);
				}
			}
		}

		public void LogActiveSceneData(bool logAllSceneObjectsData)
		{
			this.LogSceneData(SceneManager.GetActiveScene(), logAllSceneObjectsData);
		}

		public void LogGameObjectNameAndParent(GameObject gameObject, bool logChildObjects)
		{
			this.logger.LogInfo("Current GameObject: " + gameObject);

			this.logger.LogInfo("Current GameObject name: " + gameObject.name);

			if (gameObject.transform.parent != null)
			{
				this.logger.LogInfo("Current GameObject parent: " + gameObject.transform.parent.gameObject);
			}
			else
			{
				this.logger.LogInfo("This GameObject does not have a parent.");
			}

			if (logChildObjects == true)
			{
				for (int currentChildIndex = 0; currentChildIndex < gameObject.transform.childCount; currentChildIndex++)
				{
					this.LogGameObjectNameAndParent(gameObject.transform.GetChild(currentChildIndex).gameObject, logChildObjects);
				}
			}
		}

		public void LogSceneGameObjectsNameAndParent(Scene scene)
		{
			foreach (GameObject rootGameObject in scene.GetRootGameObjects())
			{
				this.LogGameObjectNameAndParent(rootGameObject, true);
			}
		}

		public void LogActiveSceneGameObjectsNameAndParent()
		{
			this.LogSceneGameObjectsNameAndParent(SceneManager.GetActiveScene());
		}

		public void LogFileSystem(FileSystem fileSystem)
		{
			FileSystem.Carpeta rootFolder = fileSystem.GetCarpetaRaiz();

			this.LogInfo("Folder name: " + rootFolder.nombre);

			foreach (FileSystem.Carpeta rootSubFolder in rootFolder.GetCarpetas())
			{
				this.LogFolder(rootSubFolder);
			}
		}

		public void LogFolder(FileSystem.Carpeta folder)
		{
			this.LogInfo("Folder name: " + folder.nombre);

			foreach (FileSystem.Archivo file in folder.GetArchivos())
			{
				this.LogInfo("File name: " + file.nombre);
			}

			foreach (FileSystem.Carpeta subFolder in folder.GetCarpetas())
			{
				this.LogFolder(subFolder);
			}
		}

		public void LogException(Exception exception)
		{
			this.LogInfo("Exception occurred.");

			this.LogInfo("Exception message: " + exception.Message);

			this.LogInfo("Exception stack trace:");

			this.LogInfo(StackTraceUtility.ExtractStackTrace());
		}
	}
}
