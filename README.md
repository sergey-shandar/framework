﻿Portable .NET Framework 

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

    ```C#
    static int ApiMain(Options options)
    {
        //
    }

    static int main(string args[])
    {
        return CommandLineParser.Run(args, ApiMain);
    }
    ```
    
- using commands

    ```C#
    sealed class Commands
    {
        int Download(string file, Optional<int> x) { ... }
        int Upload(string file) { ... }
    } 
    
    static int main(string args[])
    {
        return Cli.Run(new Commands(), args);
    }
    ```
    
- using static classes

    ```C#
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
    ```
    
See [CLAP](http://adrianaisemberg.github.io/CLAP/)
    
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
    
    public sealed class Empty: Stack<T>
    {
        public override TR Apply<TR>(Switch<TR> s)
        {
            return s.Case(this);
        }
    }
    
    public sealed class One: Stack<T>
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
    
```
- A(null, null)
    - A < B => B(A, null)
        - A < B < C => B`(A, C), C(null, null)
            - A < B < C < D => B`(A, D), D(C, null)
                - A < B < C < D < E =>
            - A < B < D < C => B`(A, D), D(null, C)
            - A < D < B < C => B`(D, C), D(A, null)
            - D < A < B < C => B`(D, C), D(null, A)
        - A < C < B => C(A, B`), B`(null, null)
        - C < A < B => C(null, null), A`(C, B`), B`(null, null)
    - B < A => B(null, A)
```
