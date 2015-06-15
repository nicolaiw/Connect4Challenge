module Connect4

open Connect4Challenge
//open Array2D

type Move =       
   | Invalid of string          
   | Valid     

let isValidMove column pitch = 
    match column with
    | i when i < 0 || i > Array2D.length1 pitch  -> Invalid("Just columns between 0 and " + ((Array2D.length1 pitch)-1).ToString() + " are Valid.\n Given: " + i.ToString()) // between 0 and 6 (on an 7x6 pitch)
    | i when pitch.[i, Array2D.length2 pitch] <> 0 -> Invalid("Column " + i.ToString() + " is Full.") // check if column is full
    | _ -> Valid

let makeMove column (pitch: int[,]) = 
    let rec testLoop currentLineIndex=
        match currentLineIndex with
        | i when pitch.[column, currentLineIndex] = 0 -> pitch.[column, i] <- 1 // 1 = yours, 0 = empty, -1 = theirs
                                                         pitch
        | i -> testLoop (i+1)
    testLoop 0

(*
    1 = yours
    0 = empty
    -1 = theirs 
     
    y 
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|_|_|_|_|
    3 |_|_|_|_|_|_|_|
    2 |_|_|_|_|_|_|_|
    1 |_|_|_|_|_|_|_| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6   x 
       
    connect4 = 4
    xMax = 7 // Array2D.lenght1 <array>
    yMax = 6 // Array2D.length2 <array>

    in --> (x,y)

    *------------------------------------------------------------*
    *   TESTS: (just test if the current move results in a win)  *
    *------------------------------------------------------------*
    
        1. x+1 < connect4                   --> don't test leftward
                                            --> also don't test diagonal left-up-ward
                                            --> also don't test diagonal left-down-ward
        2. x+1 + connect4 > xMax            --> don't test rightward
                                            --> also don't test diagonal right-up-ward 
                                            --> also don't test diagonal left-down-ward
        3. else                             
           4. y+1 < connect4                  --> don't test downward
                                              --> also don't test left-down-ward
                                              --> also don't test right-down-ward
           5. y+1 + connect4 > yMax           --> NEVER test upward (because just the current move will be tested)
                                              --> also don't test left-up-ward
                                              --> also don't test right-up-ward
*)

let won x y pitch = false

let getLine x pitch = 
    let rec lineLoop index slotValue =
        match slotValue with
        | _ when index >= Array2D.length2 pitch -> failwith "NO FREE SLOT FOUND"
        | 0 -> index-1
        | _ -> lineLoop (index+1) pitch.[x, index]
    lineLoop 0 999

let game (p1: IConnectFour) (p2: IConnectFour) =
    let players = [|p1;p2|]
    let mutable playerIndex = 0

    let getNextPlayer = 
        match playerIndex with
        | 0 -> playerIndex <- playerIndex+1
        | 1 -> playerIndex <- playerIndex-1
        |_ -> failwith "INVALID PLAYERINDEX"
        players.[playerIndex]

    let rec move (player: IConnectFour) (pitchSoFar:int[,]) =
        let column = player.Move(pitchSoFar)
        match isValidMove column pitchSoFar with
        | Valid -> 
                  match won column (getLine column pitchSoFar) (makeMove column pitchSoFar) with
                  | true -> player
                  | _ -> move getNextPlayer pitchSoFar
        | Invalid(txt) -> failwith ("INVALID MOVE: " + txt)
    move players.[playerIndex] (Array2D.create 7 6 0)

    