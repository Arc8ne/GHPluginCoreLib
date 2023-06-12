using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GHPluginCoreLib
{
    public class GreyOSProgramManager
    {
        public readonly List<GreyOSProgram> registeredGreyOSPrograms = new List<GreyOSProgram>();

        public GreyOSProgramManager()
        {

        }

        public void RegisterProgram(GreyOSProgram greyOSProgram)
        {
            this.registeredGreyOSPrograms.Add(greyOSProgram);
        }

        public void UnregisterProgram(GreyOSProgram greyOSProgram)
        {
            this.registeredGreyOSPrograms.Remove(greyOSProgram);
        }
    }
}
