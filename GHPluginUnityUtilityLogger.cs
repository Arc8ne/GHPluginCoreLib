using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using Component = UnityEngine.Component;
using System.ComponentModel;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using Jint.Parser;

namespace GHPluginCoreLib
{
    public class GHPluginUnityUtilityLogger
    {
        private string currentLog = "";

        public GHPluginUnityUtilityLogger()
        {

        }

        private void WriteLine(string msg = "")
        {
            currentLog += msg + "\n";
        }

        private void ClearLog()
        {
            currentLog = "";
        }

        private string GetUnityMatrix4x4ElementsAsString(Matrix4x4 matrix4x4)
        {
            string matrix4x4ElementsString = "";

            for (int currentRowIndex = 0; currentRowIndex < 4; currentRowIndex++)
            {
                matrix4x4ElementsString += matrix4x4.GetRow(currentRowIndex).ToString() + " ";
            }

            return matrix4x4ElementsString;
        }

        private void WriteUnityMatrix4x4Data(Matrix4x4 matrix4x4, string matrix4x4VariableName = "UnityEngine.Matrix4x4")
        {
            this.WriteLine(
                matrix4x4VariableName + ":" + this.GetUnityMatrix4x4ElementsAsString(matrix4x4)
            );
        }

		private void WriteFieldsData(FieldInfo[] fieldInfoArray, object selectedObject)
		{
			object currentPropertyInfoValue = null;

			foreach (FieldInfo fieldInfo in fieldInfoArray)
			{
				try
				{
					currentPropertyInfoValue = fieldInfo.GetValue(selectedObject);

					if (currentPropertyInfoValue is Matrix4x4 matrix4x4)
					{
						this.WriteUnityMatrix4x4Data(matrix4x4, fieldInfo.Name);
					}
					else
					{
						this.WriteLine(Regex.Replace(fieldInfo.Name + ":" + currentPropertyInfoValue.ToString(), @"\r\n?|\n", ""));
					}
				}
				catch
				{
					this.WriteLine(fieldInfo.Name + ":null");
				}
			}
		}

		private void WriteAllFieldDataOfObject(object selectedObject)
		{
			this.WriteFieldsData(
				selectedObject.GetType().GetFields(
					BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
				),
				selectedObject
			);
		}

		private void WriteAllPropertyDataOfObject(object selectedObject)
        {
			object currentPropertyInfoValue = null;

			foreach (PropertyInfo propertyInfo in selectedObject.GetType().GetProperties())
			{
				try
				{
					currentPropertyInfoValue = propertyInfo.GetValue(selectedObject);

					if (currentPropertyInfoValue is Matrix4x4 matrix4x4)
					{
						this.WriteUnityMatrix4x4Data(matrix4x4, propertyInfo.Name);
					}
                    else
                    {
						this.WriteLine(Regex.Replace(propertyInfo.Name + ":" + currentPropertyInfoValue.ToString(), @"\r\n?|\n", ""));
					}
				}
				catch
				{
					this.WriteLine(propertyInfo.Name + ":null");
				}
			}
		}

		private void WriteAllMethodDataOfObject(object selectedObject)
		{
            string currentMethodSignature = "";

			foreach (MethodInfo methodInfo in selectedObject.GetType().GetMethods())
			{
                currentMethodSignature = "";

				if (methodInfo.IsPrivate == true)
				{
					currentMethodSignature += "private ";
				}
				else if (methodInfo.IsPublic == true)
				{
					currentMethodSignature += "public ";
				}

				if (methodInfo.IsConstructor == false)
                {
					if (methodInfo.IsAbstract == true)
					{
						currentMethodSignature += "abstract ";
					}
					else if (methodInfo.IsStatic == true)
					{
						currentMethodSignature += "static ";
					}
					else if (methodInfo.IsVirtual == true)
					{
						currentMethodSignature += "virtual ";
					}

					currentMethodSignature += methodInfo.ReturnType.FullName + " " + methodInfo.Name + "(";
				}
				else
				{
					currentMethodSignature += methodInfo.Name + "(";
				}

				ParameterInfo[] methodParameters = methodInfo.GetParameters();

				for (int currentParameterIndex = 0; currentParameterIndex < methodParameters.Length; currentParameterIndex++)
				{
					currentMethodSignature += methodParameters[currentParameterIndex].ParameterType.FullName;

					if (currentParameterIndex < methodParameters.Length - 1)
					{
						currentMethodSignature += ", ";
					}
				}

				currentMethodSignature += ")";

				this.WriteLine("Method:" + currentMethodSignature);
			}
		}

		private void WriteComponentData(Component component, bool logAllFieldsData, bool logAllPropertiesData, bool logAllMethodsData)
        {
			this.WriteLine("GameObject:" + component.name);

			this.WriteLine("Type:" + component.GetType().ToString());

			if (component.gameObject != null)
			{
				this.WriteLine("Parent:" + component.gameObject.name);
			}
			else
			{
				this.WriteLine("Parent:<None>");
			}

            if (logAllFieldsData == true)
			{
				this.WriteLine("<FieldList>");

				this.WriteAllFieldDataOfObject(component);
			}

            if (logAllPropertiesData == true)
			{
				this.WriteLine("<PropertyList>");

				this.WriteAllPropertyDataOfObject(component);
			}

            if (logAllMethodsData == true)
			{
				this.WriteLine("<MethodList>");

				this.WriteAllMethodDataOfObject(component);
			}

			this.WriteLine();
		}

        private void WriteGameObjectData(GameObject gameObject, bool logAllChildGameObjectsData, bool onlyLogActiveGameObjects, bool logAllComponentsData, bool logAllFieldsData, bool logAllPropertiesData, bool logAllMethodsData)
        {
            if (onlyLogActiveGameObjects == true && gameObject.activeInHierarchy == false)
            {
                return;
            }

            this.WriteLine("GameObject:" + gameObject.name);

            this.WriteLine("Type:" + gameObject.GetType().ToString());

            if (gameObject.transform.parent != null)
            {
                this.WriteLine("Parent:" + gameObject.transform.parent.gameObject.name);
            }
            else
            {
                this.WriteLine("Parent:<None>");
            }

			if (logAllFieldsData == true)
			{
				this.WriteLine("<FieldList>");

				this.WriteAllFieldDataOfObject(gameObject);
			}

			if (logAllPropertiesData == true)
			{
				this.WriteLine("<PropertyList>");

				this.WriteAllPropertyDataOfObject(gameObject);
			}

			if (logAllMethodsData == true)
			{
				this.WriteLine("<MethodList>");

				this.WriteAllMethodDataOfObject(gameObject);
			}

			this.WriteLine();

			if (logAllComponentsData == true)
            {
				foreach (Component component in gameObject.GetComponents(typeof(Component)))
				{
					this.WriteComponentData(
						component,
						logAllFieldsData,
						logAllPropertiesData,
						logAllMethodsData
					);
				}
			}

            if (logAllChildGameObjectsData == true)
            {
                for (int currentIndex = 0; currentIndex < gameObject.transform.childCount; currentIndex++)
                {
                    this.WriteGameObjectData(
						gameObject.transform.GetChild(currentIndex).gameObject,
						logAllChildGameObjectsData,
						onlyLogActiveGameObjects,
						logAllComponentsData,
						logAllFieldsData,
						logAllPropertiesData,
						logAllMethodsData
					);
                }
            }
        }

        public void GenerateUnitySceneLogFileForScene(Scene scene, string outputPath, bool onlyLogActiveGameObjects, bool logAllComponentsData, bool logAllFieldsData, bool logAllPropertiesData, bool logAllMethodsData)
        {
            if (scene == null)
            {
                return;
            }

            this.ClearLog();

            GameObject[] rootGameObjects = scene.GetRootGameObjects();

            foreach (GameObject rootGameObject in rootGameObjects)
            {
                this.WriteGameObjectData(
					rootGameObject,
					true,
					onlyLogActiveGameObjects,
					logAllComponentsData,
					logAllFieldsData,
					logAllPropertiesData,
					logAllMethodsData
				);
            }

            File.WriteAllText(outputPath, this.currentLog);
        }

        public void GenerateUnitySceneLogFileForActiveScene(string outputPath, bool onlyLogActiveGameObjects, bool logAllComponentsData, bool logAllFieldsData, bool logAllPropertiesData, bool logAllMethodsData)
        {
            this.GenerateUnitySceneLogFileForScene(
				SceneManager.GetActiveScene(),
				outputPath,
				onlyLogActiveGameObjects,
				logAllComponentsData,
				logAllFieldsData,
				logAllPropertiesData,
				logAllMethodsData
			);
        }

        public void GenerateUnitySceneLogFileForGameObject(GameObject gameObject, string outputPath, bool onlyLogActiveGameObjects, bool logAllComponentsData, bool logAllFieldsData, bool logAllPropertiesData, bool logAllMethodsData)
        {
            this.ClearLog();

            this.WriteGameObjectData(
				gameObject,
				true,
				onlyLogActiveGameObjects,
				logAllComponentsData,
				logAllFieldsData,
				logAllPropertiesData,
				logAllMethodsData
			);

            File.WriteAllText(outputPath, this.currentLog);
        }
    }
}
