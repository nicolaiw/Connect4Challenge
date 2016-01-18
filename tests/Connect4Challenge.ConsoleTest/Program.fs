open Connect4Challenge.Bootstrapper
open Connect4Challenge.Interface
open Connect4Challenge.RunTime
open System.Text
//open Connect4Challenge.Bot



type P3() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_3"
            override this.Move pitch =  0

type P4() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_4"
            override this.Move pitch =  0

let pitch = Array2D.create 7 6 0

let p3 = new P3()
let p4 = new P4()

let gameLog = game p3 p4 4 pitch (fun _ ->())


[<EntryPoint>]
let main argv = 

    let res = createPitch gameLog (Array2D.length1 pitch-1) (Array2D.length2 pitch-1);

    printf "%s" (res.ToString())
    
//    let pitch = Array2D.create 7 6 0 
//    let l = Array2D.length1 pitch
//    let moveResult = isValidMove -1 (Array2D.create 7 6 0)
//    let moveResult2 = isValidMove 0 (Array2D.create 7 6 1)
//    let o = getSubClassFromAssembly<ConnectFour> @"C:\Users\Nicki\Desktop\Projects\Connect4Challenge\tests\Connect4Challenge.TestImplementation\bin\Debug\Connect4Challenge.TestImplementation.dll"
//    let sb = new Sandboxer()
//    let arr = Array2D.create 7 6 0
//    let args = [|arr|]
//    let appDom = sb.setup(@"C:\Users\Nicki\Desktop\Projects\Connect4Challenge\tests\Connect4Challenge.TestImplementation\bin\Debug")
//    
//    let name = appDom.invokeProperty<ConnectFour,string>("Connect4Challenge.TestImplementation","Name")
//    let move = appDom.invokeMethod<ConnectFour,int>("Connect4Challenge.TestImplementation","Move", arr)
//
//    printfn "name: %s; move: %i" (o.Name) (o.Move arr)

    System.Console.ReadLine() |> ignore
    System.Console.ReadKey() |> ignore

    0 // return an integer exit code