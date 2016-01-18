namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Connect4Challenge.Interface")>]
[<assembly: AssemblyProductAttribute("Connect4Challenge")>]
[<assembly: AssemblyDescriptionAttribute("Connect4 programming challenge.")>]
[<assembly: AssemblyVersionAttribute("0.0.2")>]
[<assembly: AssemblyFileVersionAttribute("0.0.2")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.2"
