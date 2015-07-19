open Connect4Challenge.Bootstrapper
open Connect4Challenge.Interface
open Connect4Challenge.RunTime



[<EntryPoint>]
let main argv = 

//    let pitch = Array2D.create 7 6 0 
//    let l = Array2D.length1 pitch
//    let moveResult = isValidMove -1 (Array2D.create 7 6 0)
//    let moveResult2 = isValidMove 0 (Array2D.create 7 6 1)

//    let sb = new Sandboxer()
//    let arr = Array2D.create 7 6 0
//    let args = [|arr|]
//    let appDom = sb.setup(@"C:\Users\Nicki\Desktop\Projects\Connect4Challenge\tests\Connect4Challenge.TestImplementation\bin\Debug")
//    
//    let name = appDom.invokeProperty<ConnectFour,string>("Connect4Challenge.TestImplementation","Name")
//    let move = appDom.invokeMethod<ConnectFour,int>("Connect4Challenge.TestImplementation","Move", arr)
//
//    printfn "name: %s; move: %i" name move

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
