using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.G1
{
    public static class Cli
    {
        public static int Run<T>(string[] args)
        {
            var type = typeof(T);
            var methods = type.GetMethods();
            foreach(var method in methods)
            {
                
            }
            return 0;
        }
    }
}
