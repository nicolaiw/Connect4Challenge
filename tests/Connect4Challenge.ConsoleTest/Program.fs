open Connect4Challenge.Bootstrapper
open Connect4Challenge.Interface

type ConFour() =
    interface IConnectFour with
        member this.Name = "Foo"
        member this.Move pitch = 1



[<EntryPoint>]
let main argv = 

    // returns null
    //let o1 = getInterfaceFromAssembly<System.IFormatProvider> @"C:\Users\Nicki\Desktop\Projects\Connect4Challenge\tests\Connect4Challenge.ConsoleTest\bin\Debug\Connect4Challenge.ConsoleTest.exe"
    // returns instance of IConnectFour
    let o2 = getInterfaceFromAssembly<IConnectFour> @"C:\Users\Nicki\Desktop\Projects\Connect4Challenge\tests\Connect4Challenge.ConsoleTest\bin\Debug\Connect4Challenge.ConsoleTest.exe"

    printfn "%s" (o2.Name)

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
