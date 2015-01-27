using System;
using Framework.G1;

namespace CliTest
{
    class Program
    {
        class Interface
        {
            static int Run()
            {
                return 0;
            }
        }

        static int Main(string[] args)
        {
            return Cli.Run<Interface>(Console.Out, args);
        }
    }
}
