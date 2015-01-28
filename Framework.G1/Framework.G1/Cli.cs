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
                // print help.
                // TODO: it should also print an error.
                foreach (var m in dictionary)
                {
                    writer.WriteLine(m.Key);
                }
                return Error;
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
                if (args[1].StartsWith('-'))
                {
                }
            }
            catch (Exception e)
            {
                writer.WriteLine("Error: " + e.Message);
                return Error;
            }
            return Ok;
        }

        public static void Parse(
            string parameter, 
            Action<string, string> parameterAction, 
            Action<string> multiparameterAction)
        {
            if (!parameter.StartsWith('-'))
            {
                throw new Exception("unrecognized parameter: " + parameter);
            }
            if (!parameter.StartsWith('-', 1))
            {
                multiparameterAction(parameter.Substring(1));
            }
            else
            {
                var nameValue = parameter.Substring(2);
                var split = nameValue.Split(nameValue.IndexOf('='));
                parameterAction(split.Before, split.After);
            }
        }
    }
}
