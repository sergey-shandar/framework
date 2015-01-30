Portable .NET Framework 

# For Developers

## Change Version Number

Change versions in two files

- Framework.G1\Framework.G1\AssemblyInfo.cs
- Framework.G1\Framework.G1\_CreateNewNuGetPackages\Config.ps1 

[https://nuget.codeplex.com/workitem/4013](https://nuget.codeplex.com/workitem/4013)

# Ideas

## Assembly Version from Git

Major.Minor.GitDeep.GitHash

## Command Line Parser

Principals:

- Using Conventions Over Configuration
- Using Immutable Structires
- Using Optional


    static int ApiMain(Options options)
    {
        //
    }

    static int main(string args[])
    {
        return CommandLineParser.Run(args, ApiMain);
    }
    
- using commands


    sealed class Commands
    {
        int Download(string file, Optional<int> x) { ... }
        int Upload(string file) { ... }
    } 
    
    static int main(string args[])
    {
        return Cli.Run(new Commands(), args);
    }
    
- using static classes


    static class Commands
    {
        [Help("Download file.")]
        static int Download([Help("A file name.")] string file, [Help("Version)] Optional<int> version) { ... }
        
        [Help("Upload file.")]
        static int Upload([Help("A file name.")]string file) { ... }
    } 
    
    static int main(string args[])
    {
        return Cli.Run<Commands>(args);
    }
    
### Settings

- case-sensetivity.
- if case-sensetive, should we transform a function name to lower case?
- non-latin symbols in function/parameter names.
- argument start options "-", "--", "/".
- argument name/value separator " ", ":", "=".
- argument values with no name.

### Accepted Types

- one value
    - string
    - char
    - byte, sbyte, ... , ulong, long
    - float,double
    - decimal
    - enum
    - DateTime, TimeSpan
- multiple values
    - IEnumerable
    
### Optional Types

- with default value:
    - string value = "something"
    - int value = 54
- using optional types:
    - Optional.ByRef<string> value = default(Optional.ByRef<string>)
    - Optional.ByValue<int> value = default(Optional.ByValue<int>)
    
- collections and booleans can't have optional types or default values. 
