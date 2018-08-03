using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Andeart.JsonButler.CodeSerialization
{
    public class ButlerSerializerSettings
    {
        public Type[] PreferredConstructorTypes { get; set; }

        public Assembly RootCallingAssembly { get; set; }

        public ButlerSerializerSettings (Assembly rootCallingAssembly)
        {
            RootCallingAssembly = rootCallingAssembly;
        }
    }
}
