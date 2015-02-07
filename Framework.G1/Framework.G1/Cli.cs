using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Framework.G1
{
    public static class Cli
    {
        public const int Ok = 0;
        public const int Error = -1;

        public static int Run<T>(TextWriter writer, string[] args)
        {
            var commandMap = typeof (T)
                .GetMethods()
                .Where(m => m.Attributes.HasFlag(
                    MethodAttributes.Public | MethodAttributes.Static))
                .ToDictionary(m => m.Name.ToLower());

            var nonameList = new List<string>();

            var nameMap = new Dictionary<string, List<string>>();

            var symbolMap = new Dictionary<char, List<string>>();

            var list = nonameList;

            foreach (var arg in args)
            {
                // -?
                if (arg.StartsWith('-'))
                {
                    // --
                    if (arg.StartsWith('-', 1))
                    {
                        list = nameMap.GetOrNew(arg.Substring(2));
                    }
                    // -
                    else
                    {
                        foreach (var c in arg.AsEnumerable().Skip(1))
                        {
                            list = symbolMap.GetOrNew(c);
                        }                       
                    }
                }
                else
                {
                    list.Add(arg);
                }
            }

            if (nonameList.Count == 0)
            {
                // print help.
                // TODO: it should also print an error.
                foreach (var m in commandMap)
                {
                    writer.WriteLine(m.Key);
                }
                return Error;
            }

            var command = nonameList[0];

            try
            {
                var c = commandMap
                    .Get(command)
                    .Default(() =>
                    {
                        throw new Exception("unknown command: " + command); 
                    });
            }
            catch (Exception e)
            {
                writer.WriteLine("Error: " + e.Message);
                return Error;
            }
            return Ok;
        } 
    }
}
