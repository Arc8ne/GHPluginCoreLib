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
	/// <summary>
	/// The GHPluginLogger class allows you to log messages to the BepInEx console. It
	/// contains various helper methods to make logging much more convenient.
	/// </summary>
	public class GHPluginLogger
	{
		private ManualLogSource logger = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sourceName"></param>
		public GHPluginLogger(string sourceName = "GH Plugin Logger")
		{
			this.logger = BepInEx.Logging.Logger.CreateLogSource(sourceName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		public GHPluginLogger(ManualLogSource logger)
		{
			this.logger = logger;
		}

		/// <summary>
		/// Logs a message to the BepInEx debug console.
		/// </summary>
		/// <param name="msg"></param>
		public void LogInfo(string msg)
		{
			this.logger.LogInfo(msg);
		}

		/// <summary>
		/// Logs all properties of a GameObject by default. It can also be used to log
		/// all properties of the GameObject and its children. The third parameter can be
		/// used to specify which properties of the GameObjects being logged should not
		/// be logged to the BepInEx debug console.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="logDataOfAllChildGameObjects"></param>
		/// <param name="propertyNamesFilter"></param>
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

		/// <summary>
		/// Logs all properties for a Scene in the game, the second parameter specifies whether
		/// the properties of all GameObjects in the Scene should be logged as well.
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="logAllSceneObjectsData"></param>
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

		/// <summary>
		/// Logs all properties for the currently active Scene in the game, the second
		/// parameter specifies whether the properties of all GameObjects in the Scene
		/// should be logged as well.
		/// </summary>
		/// <param name="logAllSceneObjectsData"></param>
		public void LogActiveSceneData(bool logAllSceneObjectsData)
		{
			this.LogSceneData(SceneManager.GetActiveScene(), logAllSceneObjectsData);
		}

		/// <summary>
		/// Logs the name of a GameObject as well as the name of its parent, the second
		/// parameter specifies whether the same should be done for the children of the
		/// GameObject specified in the first parameter.
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="logChildObjects"></param>
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

		/// <summary>
		/// Logs the names of all the GameObjects in a Scene in the game as well as the names
		/// of their parents.
		/// </summary>
		/// <param name="scene"></param>
		public void LogSceneGameObjectsNameAndParent(Scene scene)
		{
			foreach (GameObject rootGameObject in scene.GetRootGameObjects())
			{
				this.LogGameObjectNameAndParent(rootGameObject, true);
			}
		}

		/// <summary>
		/// Logs the names of all the GameObjects in the currently active Scene in the game
		/// as well as the names of their parents.
		/// </summary>
		public void LogActiveSceneGameObjectsNameAndParent()
		{
			this.LogSceneGameObjectsNameAndParent(SceneManager.GetActiveScene());
		}

		/// <summary>
		/// Logs the names of all the folders and files in a FileSystem instance.
		/// </summary>
		/// <param name="fileSystem"></param>
		public void LogFileSystem(FileSystem fileSystem)
		{
			FileSystem.Carpeta rootFolder = fileSystem.GetCarpetaRaiz();

			this.LogInfo("Folder name: " + rootFolder.nombre);

			foreach (FileSystem.Carpeta rootSubFolder in rootFolder.GetCarpetas())
			{
				this.LogFolder(rootSubFolder);
			}
		}

		/// <summary>
		/// Logs the name of the FileSystem.Carpeta instance specified in the first parameter
		/// as well as the name of all its subfolders and files.
		/// </summary>
		/// <param name="folder"></param>
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

		/// <summary>
		/// Logs information about an exception, including its message and its stack trace.
		/// </summary>
		/// <param name="exception"></param>
		public void LogException(Exception exception)
		{
			this.LogInfo("Exception occurred.");

			this.LogInfo("Exception message: " + exception.Message);

			this.LogInfo("Exception stack trace:");

			this.LogInfo(StackTraceUtility.ExtractStackTrace());
		}
	}
}
