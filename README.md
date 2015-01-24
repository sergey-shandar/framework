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
  }

  static int main(string args[])
  {
      return CommandLineParser.Run(args, ApiMain);
  } 