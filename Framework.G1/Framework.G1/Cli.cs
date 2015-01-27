using System;
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
            var dictionary = typeof (T)
                .GetMethods()
                .Where(m => m.Attributes.HasFlag(
                    MethodAttributes.Public | MethodAttributes.Static))
                .ToDictionary(m => m.Name.ToLower());

            if (args.Length == 0)
            {
                foreach (var m in dictionary)
                {
                    writer.WriteLine(m.Key);
                }
                return Ok;
            }

            var command = args[0];

            try
            {
                var c = dictionary
                    .Get(args[0])
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
