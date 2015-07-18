namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Connect4Challenge.Runtime")>]
[<assembly: AssemblyProductAttribute("Connect4Challenge")>]
[<assembly: AssemblyDescriptionAttribute("Connect4Challenge is a programming challenge. You can compete against our algorithm by implementing the ConnectFour Interface. You can wirte you Code in F# or C#.")>]
[<assembly: AssemblyVersionAttribute("0.0.2")>]
[<assembly: AssemblyFileVersionAttribute("0.0.2")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.2"
