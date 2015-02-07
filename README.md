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

## JSON Parser

### Untyped

```C#
JS.Object["name", JS.Array[5][6][7]["string"]]["Date", 5] => { "name": [ 5, 6, 7, "string"], "Date": 5 }
    
JS
    .Object
    .p("name", JS.Array.i(5).i(6).i(7).i("string"))
    .p("Date", 5)
    
// with implicit type conversion to JS.ValueType    
JS
    .Object
    .p("name", JS.Array(5, 6, 7, "string"))
    .p("Date", 5)
```    
    
# Immutable Containers

## Stack

```C#
public abstract class Stack<T> 
{
    public abstract class Switch<TR>
    {
        public abstract TR Case(Empty empty);
        public abstract TR Case(One one);
    }
    
    public abstract TR Apply<TR>(Switch<TR> s);
    
    public sealed class Empty
    {
        public override TR Apply<TR>(Switch<TR> s)
        {
            return s.Case(this);
        }
    }
    
    public sealed class One
    {
        public readonly T Value;
        public readonly Stack<T> Next;
    
        public override TR Apply<TR>(Switch<TR> s)
        {
            return s.Case(this);
        }
        
        public One(T value, Stack<T> next)
        {
            this.Value = value;
            this.Next = next;
        }
    }
    
    private Stack()
    {
    }
} 
``` 
    
