<a name='assembly'></a>
# GHPluginCoreLib

## Contents

- [GHPluginCore](#T-GHPluginCoreLib-GHPluginCore 'GHPluginCoreLib.GHPluginCore')
  - [greyOSProgramManager](#F-GHPluginCoreLib-GHPluginCore-greyOSProgramManager 'GHPluginCoreLib.GHPluginCore.greyOSProgramManager')
  - [instance](#F-GHPluginCoreLib-GHPluginCore-instance 'GHPluginCoreLib.GHPluginCore.instance')
  - [Init()](#M-GHPluginCoreLib-GHPluginCore-Init 'GHPluginCoreLib.GHPluginCore.Init')
- [GHPluginLogger](#T-GHPluginCoreLib-GHPluginLogger 'GHPluginCoreLib.GHPluginLogger')
  - [#ctor(sourceName)](#M-GHPluginCoreLib-GHPluginLogger-#ctor-System-String- 'GHPluginCoreLib.GHPluginLogger.#ctor(System.String)')
  - [#ctor(logger)](#M-GHPluginCoreLib-GHPluginLogger-#ctor-BepInEx-Logging-ManualLogSource- 'GHPluginCoreLib.GHPluginLogger.#ctor(BepInEx.Logging.ManualLogSource)')
  - [LogActiveSceneData(logAllSceneObjectsData)](#M-GHPluginCoreLib-GHPluginLogger-LogActiveSceneData-System-Boolean- 'GHPluginCoreLib.GHPluginLogger.LogActiveSceneData(System.Boolean)')
  - [LogActiveSceneGameObjectsNameAndParent()](#M-GHPluginCoreLib-GHPluginLogger-LogActiveSceneGameObjectsNameAndParent 'GHPluginCoreLib.GHPluginLogger.LogActiveSceneGameObjectsNameAndParent')
  - [LogAllGameObjectData(gameObject,logDataOfAllChildGameObjects,propertyNamesFilter)](#M-GHPluginCoreLib-GHPluginLogger-LogAllGameObjectData-UnityEngine-GameObject,System-Boolean,System-Collections-Generic-List{System-String}- 'GHPluginCoreLib.GHPluginLogger.LogAllGameObjectData(UnityEngine.GameObject,System.Boolean,System.Collections.Generic.List{System.String})')
  - [LogException(exception)](#M-GHPluginCoreLib-GHPluginLogger-LogException-System-Exception- 'GHPluginCoreLib.GHPluginLogger.LogException(System.Exception)')
  - [LogFileSystem(fileSystem)](#M-GHPluginCoreLib-GHPluginLogger-LogFileSystem-FileSystem- 'GHPluginCoreLib.GHPluginLogger.LogFileSystem(FileSystem)')
  - [LogFolder(folder)](#M-GHPluginCoreLib-GHPluginLogger-LogFolder-FileSystem-Carpeta- 'GHPluginCoreLib.GHPluginLogger.LogFolder(FileSystem.Carpeta)')
  - [LogGameObjectNameAndParent(gameObject,logChildObjects)](#M-GHPluginCoreLib-GHPluginLogger-LogGameObjectNameAndParent-UnityEngine-GameObject,System-Boolean- 'GHPluginCoreLib.GHPluginLogger.LogGameObjectNameAndParent(UnityEngine.GameObject,System.Boolean)')
  - [LogInfo(msg)](#M-GHPluginCoreLib-GHPluginLogger-LogInfo-System-String- 'GHPluginCoreLib.GHPluginLogger.LogInfo(System.String)')
  - [LogSceneData(scene,logAllSceneObjectsData)](#M-GHPluginCoreLib-GHPluginLogger-LogSceneData-UnityEngine-SceneManagement-Scene,System-Boolean- 'GHPluginCoreLib.GHPluginLogger.LogSceneData(UnityEngine.SceneManagement.Scene,System.Boolean)')
  - [LogSceneGameObjectsNameAndParent(scene)](#M-GHPluginCoreLib-GHPluginLogger-LogSceneGameObjectsNameAndParent-UnityEngine-SceneManagement-Scene- 'GHPluginCoreLib.GHPluginLogger.LogSceneGameObjectsNameAndParent(UnityEngine.SceneManagement.Scene)')
- [GHPluginUnityUtilityLogger](#T-GHPluginCoreLib-GHPluginUnityUtilityLogger 'GHPluginCoreLib.GHPluginUnityUtilityLogger')
  - [#ctor()](#M-GHPluginCoreLib-GHPluginUnityUtilityLogger-#ctor 'GHPluginCoreLib.GHPluginUnityUtilityLogger.#ctor')
  - [GenerateUnitySceneLogFileForActiveScene(outputPath,onlyLogActiveGameObjects,logAllComponentsData,logAllFieldsData,logAllPropertiesData,logAllMethodsData)](#M-GHPluginCoreLib-GHPluginUnityUtilityLogger-GenerateUnitySceneLogFileForActiveScene-System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Boolean- 'GHPluginCoreLib.GHPluginUnityUtilityLogger.GenerateUnitySceneLogFileForActiveScene(System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)')
  - [GenerateUnitySceneLogFileForGameObject(gameObject,outputPath,onlyLogActiveGameObjects,logAllComponentsData,logAllFieldsData,logAllPropertiesData,logAllMethodsData)](#M-GHPluginCoreLib-GHPluginUnityUtilityLogger-GenerateUnitySceneLogFileForGameObject-UnityEngine-GameObject,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Boolean- 'GHPluginCoreLib.GHPluginUnityUtilityLogger.GenerateUnitySceneLogFileForGameObject(UnityEngine.GameObject,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)')
  - [GenerateUnitySceneLogFileForScene(scene,outputPath,onlyLogActiveGameObjects,logAllComponentsData,logAllFieldsData,logAllPropertiesData,logAllMethodsData)](#M-GHPluginCoreLib-GHPluginUnityUtilityLogger-GenerateUnitySceneLogFileForScene-UnityEngine-SceneManagement-Scene,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Boolean- 'GHPluginCoreLib.GHPluginUnityUtilityLogger.GenerateUnitySceneLogFileForScene(UnityEngine.SceneManagement.Scene,System.String,System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)')
- [GHPluginUtilities](#T-GHPluginCoreLib-GHPluginUtilities 'GHPluginCoreLib.GHPluginUtilities')
  - [GetComponentInGameObjectByName\`\`1(gameObject,componentName)](#M-GHPluginCoreLib-GHPluginUtilities-GetComponentInGameObjectByName``1-UnityEngine-GameObject,System-String- 'GHPluginCoreLib.GHPluginUtilities.GetComponentInGameObjectByName``1(UnityEngine.GameObject,System.String)')
- [GreyOSProgram](#T-GHPluginCoreLib-GreyOSProgram 'GHPluginCoreLib.GreyOSProgram')
  - [#ctor(programName,associatedAssetBundleFilePath,programPrefabName)](#M-GHPluginCoreLib-GreyOSProgram-#ctor-System-String,System-String,System-String- 'GHPluginCoreLib.GreyOSProgram.#ctor(System.String,System.String,System.String)')
  - [#ctor(programName,programPrefab)](#M-GHPluginCoreLib-GreyOSProgram-#ctor-System-String,UnityEngine-GameObject- 'GHPluginCoreLib.GreyOSProgram.#ctor(System.String,UnityEngine.GameObject)')
  - [associatedFile](#F-GHPluginCoreLib-GreyOSProgram-associatedFile 'GHPluginCoreLib.GreyOSProgram.associatedFile')
  - [programName](#F-GHPluginCoreLib-GreyOSProgram-programName 'GHPluginCoreLib.GreyOSProgram.programName')
  - [programPrefab](#F-GHPluginCoreLib-GreyOSProgram-programPrefab 'GHPluginCoreLib.GreyOSProgram.programPrefab')
  - [Procesar(comando,currentFile,PID,terminal)](#M-GHPluginCoreLib-GreyOSProgram-Procesar-System-String[],FileSystem-Archivo,System-Int32,Terminal- 'GHPluginCoreLib.GreyOSProgram.Procesar(System.String[],FileSystem.Archivo,System.Int32,Terminal)')
- [GreyOSProgramManager](#T-GHPluginCoreLib-GreyOSProgramManager 'GHPluginCoreLib.GreyOSProgramManager')
  - [#ctor()](#M-GHPluginCoreLib-GreyOSProgramManager-#ctor 'GHPluginCoreLib.GreyOSProgramManager.#ctor')
  - [registeredGreyOSPrograms](#F-GHPluginCoreLib-GreyOSProgramManager-registeredGreyOSPrograms 'GHPluginCoreLib.GreyOSProgramManager.registeredGreyOSPrograms')
  - [RegisterProgram(greyOSProgram)](#M-GHPluginCoreLib-GreyOSProgramManager-RegisterProgram-GHPluginCoreLib-GreyOSProgram- 'GHPluginCoreLib.GreyOSProgramManager.RegisterProgram(GHPluginCoreLib.GreyOSProgram)')
  - [UnregisterProgram(greyOSProgram)](#M-GHPluginCoreLib-GreyOSProgramManager-UnregisterProgram-GHPluginCoreLib-GreyOSProgram- 'GHPluginCoreLib.GreyOSProgramManager.UnregisterProgram(GHPluginCoreLib.GreyOSProgram)')

<a name='T-GHPluginCoreLib-GHPluginCore'></a>
## GHPluginCore `type`

##### Namespace

GHPluginCoreLib

##### Summary

The GHPluginCore class can be inherited from by your BepInEx plugin, however it is
not mandatory for your BepInEx plugin to inherit from this class.
The GHPluginCore class is responsible for implementing and exposing the functionality
that this library offers (e.g. the ability to create and implement custom
programs that can be used in GreyOS). It can be accessed through its "instance" field.

<a name='F-GHPluginCoreLib-GHPluginCore-greyOSProgramManager'></a>
### greyOSProgramManager `constants`

##### Summary

An instance of the GreyOSProgramManager, which allows you to register custom
programs that can be executed and used in GreyOS, the operating system which
a player's in-game computer runs on.

<a name='F-GHPluginCoreLib-GHPluginCore-instance'></a>
### instance `constants`

##### Summary

An instance of the GHPluginCore class which can be used to access instances of
other classes like the GreyOSProgramManager.

<a name='M-GHPluginCoreLib-GHPluginCore-Init'></a>
### Init() `method`

##### Summary

This method must be called in the Awake method of your BepInEx plugin,
preferably at the start of it if possible. It is responsible for executing crucial
initialization procedures to ensure that the library works correctly.

##### Parameters

This method has no parameters.

<a name='T-GHPluginCoreLib-GHPluginLogger'></a>
## GHPluginLogger `type`

##### Namespace

GHPluginCoreLib

##### Summary

The GHPluginLogger class allows you to log messages to the BepInEx console. It
contains various helper methods to make logging much more convenient.

<a name='M-GHPluginCoreLib-GHPluginLogger-#ctor-System-String-'></a>
### #ctor(sourceName) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| sourceName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-#ctor-BepInEx-Logging-ManualLogSource-'></a>
### #ctor(logger) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logger | [BepInEx.Logging.ManualLogSource](#T-BepInEx-Logging-ManualLogSource 'BepInEx.Logging.ManualLogSource') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogActiveSceneData-System-Boolean-'></a>
### LogActiveSceneData(logAllSceneObjectsData) `method`

##### Summary

Logs all properties for the currently active Scene in the game, the second
parameter specifies whether the properties of all GameObjects in the Scene
should be logged as well.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logAllSceneObjectsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogActiveSceneGameObjectsNameAndParent'></a>
### LogActiveSceneGameObjectsNameAndParent() `method`

##### Summary

Logs the names of all the GameObjects in the currently active Scene in the game
as well as the names of their parents.

##### Parameters

This method has no parameters.

<a name='M-GHPluginCoreLib-GHPluginLogger-LogAllGameObjectData-UnityEngine-GameObject,System-Boolean,System-Collections-Generic-List{System-String}-'></a>
### LogAllGameObjectData(gameObject,logDataOfAllChildGameObjects,propertyNamesFilter) `method`

##### Summary

Logs all properties of a GameObject by default. It can also be used to log
all properties of the GameObject and its children. The third parameter can be
used to specify which properties of the GameObjects being logged should not
be logged to the BepInEx debug console.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameObject | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') |  |
| logDataOfAllChildGameObjects | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| propertyNamesFilter | [System.Collections.Generic.List{System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{System.String}') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogException-System-Exception-'></a>
### LogException(exception) `method`

##### Summary

Logs information about an exception, including its message and its stack trace.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogFileSystem-FileSystem-'></a>
### LogFileSystem(fileSystem) `method`

##### Summary

Logs the names of all the folders and files in a FileSystem instance.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| fileSystem | [FileSystem](#T-FileSystem 'FileSystem') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogFolder-FileSystem-Carpeta-'></a>
### LogFolder(folder) `method`

##### Summary

Logs the name of the FileSystem.Carpeta instance specified in the first parameter
as well as the name of all its subfolders and files.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| folder | [FileSystem.Carpeta](#T-FileSystem-Carpeta 'FileSystem.Carpeta') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogGameObjectNameAndParent-UnityEngine-GameObject,System-Boolean-'></a>
### LogGameObjectNameAndParent(gameObject,logChildObjects) `method`

##### Summary

Logs the name of a GameObject as well as the name of its parent, the second
parameter specifies whether the same should be done for the children of the
GameObject specified in the first parameter.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameObject | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') |  |
| logChildObjects | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogInfo-System-String-'></a>
### LogInfo(msg) `method`

##### Summary

Logs a message to the BepInEx debug console.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| msg | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogSceneData-UnityEngine-SceneManagement-Scene,System-Boolean-'></a>
### LogSceneData(scene,logAllSceneObjectsData) `method`

##### Summary

Logs all properties for a Scene in the game, the second parameter specifies whether
the properties of all GameObjects in the Scene should be logged as well.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scene | [UnityEngine.SceneManagement.Scene](#T-UnityEngine-SceneManagement-Scene 'UnityEngine.SceneManagement.Scene') |  |
| logAllSceneObjectsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-GHPluginCoreLib-GHPluginLogger-LogSceneGameObjectsNameAndParent-UnityEngine-SceneManagement-Scene-'></a>
### LogSceneGameObjectsNameAndParent(scene) `method`

##### Summary

Logs the names of all the GameObjects in a Scene in the game as well as the names
of their parents.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scene | [UnityEngine.SceneManagement.Scene](#T-UnityEngine-SceneManagement-Scene 'UnityEngine.SceneManagement.Scene') |  |

<a name='T-GHPluginCoreLib-GHPluginUnityUtilityLogger'></a>
## GHPluginUnityUtilityLogger `type`

##### Namespace

GHPluginCoreLib

##### Summary

The GHPluginUnityUtilityLogger class can be used to generate log files containing
comprehensive and detailed information about a Unity scene.

<a name='M-GHPluginCoreLib-GHPluginUnityUtilityLogger-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor method for the GHPluginUnityUtilityLogger class.

##### Parameters

This constructor has no parameters.

<a name='M-GHPluginCoreLib-GHPluginUnityUtilityLogger-GenerateUnitySceneLogFileForActiveScene-System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Boolean-'></a>
### GenerateUnitySceneLogFileForActiveScene(outputPath,onlyLogActiveGameObjects,logAllComponentsData,logAllFieldsData,logAllPropertiesData,logAllMethodsData) `method`

##### Summary

Generates a log file for the currently active Unity scene in the game.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| outputPath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| onlyLogActiveGameObjects | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllComponentsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllFieldsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllPropertiesData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllMethodsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-GHPluginCoreLib-GHPluginUnityUtilityLogger-GenerateUnitySceneLogFileForGameObject-UnityEngine-GameObject,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Boolean-'></a>
### GenerateUnitySceneLogFileForGameObject(gameObject,outputPath,onlyLogActiveGameObjects,logAllComponentsData,logAllFieldsData,logAllPropertiesData,logAllMethodsData) `method`

##### Summary

Generates a log file for the GameObject specified in the first parameter.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameObject | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') |  |
| outputPath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| onlyLogActiveGameObjects | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllComponentsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllFieldsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllPropertiesData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllMethodsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-GHPluginCoreLib-GHPluginUnityUtilityLogger-GenerateUnitySceneLogFileForScene-UnityEngine-SceneManagement-Scene,System-String,System-Boolean,System-Boolean,System-Boolean,System-Boolean,System-Boolean-'></a>
### GenerateUnitySceneLogFileForScene(scene,outputPath,onlyLogActiveGameObjects,logAllComponentsData,logAllFieldsData,logAllPropertiesData,logAllMethodsData) `method`

##### Summary

Generates a log file for the Unity scene specified in the first parameter.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scene | [UnityEngine.SceneManagement.Scene](#T-UnityEngine-SceneManagement-Scene 'UnityEngine.SceneManagement.Scene') |  |
| outputPath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| onlyLogActiveGameObjects | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllComponentsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllFieldsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllPropertiesData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| logAllMethodsData | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='T-GHPluginCoreLib-GHPluginUtilities'></a>
## GHPluginUtilities `type`

##### Namespace

GHPluginCoreLib

##### Summary

The GHPluginUtilities class exposes several helper methods for BepInEx plugin
developers to use.

<a name='M-GHPluginCoreLib-GHPluginUtilities-GetComponentInGameObjectByName``1-UnityEngine-GameObject,System-String-'></a>
### GetComponentInGameObjectByName\`\`1(gameObject,componentName) `method`

##### Summary

Returns the Component of one of the children of the GameObject specified in the
first parameter with the same name as the one specified in the second parameter.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| gameObject | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') |  |
| componentName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-GHPluginCoreLib-GreyOSProgram'></a>
## GreyOSProgram `type`

##### Namespace

GHPluginCoreLib

##### Summary

The GreyOSProgram class is the class from which custom programs that are meant
to be run on a player's in-game computer must inherit from.

<a name='M-GHPluginCoreLib-GreyOSProgram-#ctor-System-String,System-String,System-String-'></a>
### #ctor(programName,associatedAssetBundleFilePath,programPrefabName) `constructor`

##### Summary

The default constructor method for the GreyOSProgram class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| programName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| associatedAssetBundleFilePath | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| programPrefabName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-GHPluginCoreLib-GreyOSProgram-#ctor-System-String,UnityEngine-GameObject-'></a>
### #ctor(programName,programPrefab) `constructor`

##### Summary

A constructor for the GreyOSProgram class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| programName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| programPrefab | [UnityEngine.GameObject](#T-UnityEngine-GameObject 'UnityEngine.GameObject') |  |

<a name='F-GHPluginCoreLib-GreyOSProgram-associatedFile'></a>
### associatedFile `constants`

##### Summary

The file associated with the program, this field will automatically be assigned
to at runtime provided that the program has been registered with the
GreyOSProgramManager.

<a name='F-GHPluginCoreLib-GreyOSProgram-programName'></a>
### programName `constants`

##### Summary

The name of the program.

<a name='F-GHPluginCoreLib-GreyOSProgram-programPrefab'></a>
### programPrefab `constants`

##### Summary

The prefab associated with the program.

<a name='M-GHPluginCoreLib-GreyOSProgram-Procesar-System-String[],FileSystem-Archivo,System-Int32,Terminal-'></a>
### Procesar(comando,currentFile,PID,terminal) `method`

##### Summary

This method can be overridden by the program's class provided that it
inherits from the GreyOSProgram class. This method is called when a player
opens the program either through the File Explorer or the Terminal.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| comando | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |
| currentFile | [FileSystem.Archivo](#T-FileSystem-Archivo 'FileSystem.Archivo') |  |
| PID | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| terminal | [Terminal](#T-Terminal 'Terminal') |  |

<a name='T-GHPluginCoreLib-GreyOSProgramManager'></a>
## GreyOSProgramManager `type`

##### Namespace

GHPluginCoreLib

##### Summary

The GreyOSProgramManager class is responsible for registering custom programs
with GreyOS, the operating system that a player's in-game computer runs on.
Programs registered with this class can be used through the in-game
computer.

<a name='M-GHPluginCoreLib-GreyOSProgramManager-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor for the GreyOSProgramManager class.

##### Parameters

This constructor has no parameters.

<a name='F-GHPluginCoreLib-GreyOSProgramManager-registeredGreyOSPrograms'></a>
### registeredGreyOSPrograms `constants`

##### Summary

Contains a list of all the programs registered with the GreyOSProgramManager
so far.

<a name='M-GHPluginCoreLib-GreyOSProgramManager-RegisterProgram-GHPluginCoreLib-GreyOSProgram-'></a>
### RegisterProgram(greyOSProgram) `method`

##### Summary

Registers a program with the GreyOSProgramManager.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| greyOSProgram | [GHPluginCoreLib.GreyOSProgram](#T-GHPluginCoreLib-GreyOSProgram 'GHPluginCoreLib.GreyOSProgram') |  |

<a name='M-GHPluginCoreLib-GreyOSProgramManager-UnregisterProgram-GHPluginCoreLib-GreyOSProgram-'></a>
### UnregisterProgram(greyOSProgram) `method`

##### Summary

Unregisters a program from the GreyOSProgramManager.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| greyOSProgram | [GHPluginCoreLib.GreyOSProgram](#T-GHPluginCoreLib-GreyOSProgram 'GHPluginCoreLib.GreyOSProgram') |  |
