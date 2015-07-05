open Connect4Challenge.Bootstrapper
open Connect4Challenge.Interface


[<EntryPoint>]
let main argv = 

    let sb = new Sandboxer()
    let arr = Array2D.create 7 6 0
    let args = [|arr|]
    let appDom = sb.setup(@"C:\Users\Nicki\Desktop\Connect4Challenge\tests\Connect4Challenge.TestImplementation\bin\Debug")
    let name = appDom.invokeProperty<ConnectFour,string>("Connect4Challenge.TestImplementation","Name")
    let move = appDom.invokeMethod<ConnectFour,int>("Connect4Challenge.TestImplementation","Move", arr)


    //let move = sb.invoke<ConnectFour, string>(@"C:\Users\Nicki\Desktop\Connect4Challenge\tests\Connect4Challenge.TestImplementation\bin\Debug", "Connect4Challenge.TestImplementation", MemberType.Method("Move"), arr)
    //let res = newSb.getInterfaceType<ConnectFour>(@"Connect4Challenge.TestImplementation")
//    let i = match tryCast<ConnectFour> newSb with
//            | Some(o) -> o
//            | None -> failwith "fail"
//
//    let name = i.Name
//    let arr = Array2D.create 7 6 0
//    let res = i.Move(arr)

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
