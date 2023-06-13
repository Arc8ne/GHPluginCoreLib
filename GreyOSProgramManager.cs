using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GHPluginCoreLib
{
    /// <summary>
    /// The GreyOSProgramManager class is responsible for registering custom programs
    /// with GreyOS, the operating system that a player's in-game computer runs on.
    /// Programs registered with this class can be used through the in-game
    /// computer.
    /// </summary>
    public class GreyOSProgramManager
    {
        /// <summary>
        /// Contains a list of all the programs registered with the GreyOSProgramManager
        /// so far.
        /// </summary>
        public readonly List<GreyOSProgram> registeredGreyOSPrograms = new List<GreyOSProgram>();

        /// <summary>
        /// Default constructor for the GreyOSProgramManager class.
        /// </summary>
        public GreyOSProgramManager()
        {

        }

        /// <summary>
        /// Registers a program with the GreyOSProgramManager.
        /// </summary>
        /// <param name="greyOSProgram"></param>
        public void RegisterProgram(GreyOSProgram greyOSProgram)
        {
            this.registeredGreyOSPrograms.Add(greyOSProgram);
        }

        /// <summary>
        /// Unregisters a program from the GreyOSProgramManager.
        /// </summary>
        /// <param name="greyOSProgram"></param>
        public void UnregisterProgram(GreyOSProgram greyOSProgram)
        {
            this.registeredGreyOSPrograms.Remove(greyOSProgram);
        }
    }
}
