open Connect4Challenge.Interface
open Connect4Challenge.RunTime

let rec getConsoleInput(playerName) = 
        System.Console.Write("ColumnIndex " + playerName + ": ");
        match  System.Int32.TryParse(System.Console.ReadLine()) with
        |(true, v) -> v
        |(false,_) -> System.Console.WriteLine("INVALID. TRY AGAIN!")
                      getConsoleInput(playerName)

type P1() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_1"
            override this.Move _ =  getConsoleInput(this.Name)

type P2() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_2"
            override this.Move _ =  getConsoleInput(this.Name)

let pitch = Array2D.create 7 6 0

let p1 = new P1()
let p2 = new P2()

let players = seq { 
                    yield (p1.Name, "x") 
                    yield (p2.Name, "o")
                  }

let printLog log = let res = createPitch log (Array2D.length1 pitch-1) (Array2D.length2 pitch-1) players
                   System.Console.Clear()                                                         
                                                                                                  
                   printf "%s" (res.ToString())


[<EntryPoint>]
let main argv = 

    printLog []
    let res = game p1 p2 4 pitch printLog

    printLog res
    //let res = gameLog (Array2D.length1 pitch-1) (Array2D.length2 pitch-1) printLog

    
    System.Console.ReadLine() |> ignore
    0