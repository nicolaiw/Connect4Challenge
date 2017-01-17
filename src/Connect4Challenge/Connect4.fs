module Connect4Challenge.RunTime

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
        2. x + connect4 > xMax              --> don't test rightward
        3. y+1 < connect4                   --> don't test downward
        4.                                  --> NEVER test upward (because just the current move will be tested)
        5. ..                               
*)

open Connect4Challenge.Interface
open System.Collections.Generic

type Move =       
   | Invalid of (int*int)*string  // return column, row and an error message        
   | Valid of int*int // return column and row
   
type MoveResult =
    | Won of (int*int) list // return the coordinates
    | None

type Line =
     | ValidLine of int
     | InvalidLine of int

type MoveLog =
    | WonMoveInterOp of string*(int*int)*IEnumerable<IEnumerable<(int*int)>>  // Player, List of Lists with Coordinates 
    | WonMove of string*(int*int)*((int*int) list) list // Player, wonMove, List of Lists with Coordinates 
    | FailMove of string*string*(int*int) // Player, ErrorMessage, Coordinates
    | UsualMove of string*(int*int) // Player, 
    | Tie of string*(int*int)

let createPitch (log: System.Collections.Generic.IEnumerable<MoveLog>) pitchMaxX pitchMaxY =

    //InterOp
    let gameLog = log |> List.ofSeq

    let slotValues = 
        let rec loop l acc = 
            match l with
            | [] -> acc// |> List.rev
            | hd::tl -> match hd with
                        | Tie(player,(x,y))| FailMove(player, _, (x,y)) | UsualMove(player,(x,y))| WonMove(player, (x,y), _) | WonMoveInterOp(player, (x,y), _) -> loop tl acc @ [(player,x,y)]
        loop gameLog []
    
    let playerNames = slotValues 
                      |> Seq.distinctBy (fun (p, _, _) -> p)
                      |> Seq.map(fun (p,_,_) -> p)
                      |> Seq.toList 

    let playerSigns = seq{
                          if playerNames.Length > 0 then yield (playerNames.[0], "x")
                          if playerNames.Length > 1 then yield (playerNames.[1], "o")
                         }
                        

    let getPlayerSign player = match Seq.exists (fun (p,_) -> p=player) playerSigns with
                               | false -> "_"
                               | _ -> playerSigns
                                      |> Seq.find (fun (p,_) -> p = player) 
                                      |> snd

    let checkSlot (x, y) = match slotValues |> List.tryFind(fun (_, xv, yv) -> x=xv && y=yv) with
                           | Some((player,_,_)) -> let sign = getPlayerSign player
                                                   "|" +  sign + "|"
                           | _ -> "|_|"

    let p = new System.Text.StringBuilder()
    
    p.AppendLine().Append("Players: ").AppendLine().AppendLine() |> ignore
    [for pl in playerSigns do 
        p.Append(fst pl + " = " + snd pl).AppendLine() |> ignore] |>ignore

    p.AppendLine().Append("y").AppendLine() |>ignore
    p.Append("   ") |> ignore

    [for _ in 0 ..pitchMaxX do
        p.Append("_  ") |> ignore
        ] |> ignore

    [for y in 0..pitchMaxY do
        p.AppendLine().Append(pitchMaxY-y).Append(" ") |> ignore
        for x in 0..pitchMaxX do
            let slotValue = checkSlot (x,pitchMaxY-y)
            p.Append(slotValue) |> ignore] |> ignore

    p.AppendLine().Append("   ") |>ignore

    [for x in 0 ..pitchMaxX do
        p.Append(x).Append("  ") |> ignore] |> ignore

    p.Append(" x") |> ignore

    p.AppendLine().AppendLine().Append("Game result:").AppendLine().AppendLine() |> ignore

    match Seq.length log with
    |l when l = 0 -> ()
    | _ -> match log |> Seq.last with
           | Tie(x,y) -> p.Append("Tie: last move (" + x.ToString() + "," + y.ToString() + ")") |>ignore
           | WonMove(player, (x,y), _) | WonMoveInterOp(player, (x,y), _) -> p.Append(player +  " won (" +  x.ToString() + "," + y.ToString() + ")" ) |>ignore
           | FailMove(player, ex, _) -> p.Append(player + ": " + ex) |> ignore
           | UsualMove(_,(_,_)) -> ()
    p


// returns the next free line for the given column (x)
let getLine x pitch = 
    let rec lineLoop lineIndex slotValue =
        match slotValue with
        | v when lineIndex = (Array2D.length2 pitch)-1 -> match v with
                                                          | 0 -> ValidLine(lineIndex)
                                                          | _ -> InvalidLine(lineIndex+1) // now free slot in columnd x --> return the invalid line
        | 0 -> ValidLine(lineIndex)
        | _ -> lineLoop (lineIndex+1) pitch.[x, lineIndex+1]
    lineLoop 0 pitch.[x, 0]

let isValidMove column pitch =
    match column with
    | i when i < 0 || i >= Array2D.length1 pitch  -> Invalid((column,-999), "Just columns between 0 and " + ((Array2D.length1 pitch)-1).ToString() + " are Valid.\n Given: " + i.ToString()) // between 0 and 6 (on a 7x6 pitch)
    | _ -> match getLine column pitch with
           | ValidLine(row) -> Valid (column, row)
           | InvalidLine(row) -> Invalid((column, row),"Column " + column.ToString() + " is Full.")
//    | i when pitch.[i, (Array2D.length2 pitch) - 1] <> 0 -> let row = getLine column pitch
//                                                            Invalid((column,row),"Column " + i.ToString() + " is Full.") // check if column is full
//    | _ -> Valid (column, getLine column pitch)
   
let makeMove column (pitch: int[,]) = 
    let rec testLoop currentLineIndex=
        match currentLineIndex with
        | i when pitch.[column, i] = 0 -> pitch.[column, i] <- 1 // 1 = yours, 0 = empty, -1 = theirs
                                          pitch
        | i -> testLoop (i+1)
    testLoop 0

let getValuesWithIndices startX nextX startY nextY howManyInARow (pitch: int[,]) =
            let rec loop x y currentIteration acc =
                match currentIteration with
                | i when i = howManyInARow -> acc |> List.rev
                | i -> let valueWithIndices = (pitch.[x, y],(x,y))
                       loop (nextX x) (nextY y) (i + 1) (valueWithIndices::acc)
            loop startX startY 0 []

let getMoveResult howManyInARow valuesAndIndices = let sum = valuesAndIndices 
                                                                |> List.map (fun (value,_) -> value)
                                                                |> List.reduce (+)
                                                   match sum with
                                                   | s when s = howManyInARow -> Won(valuesAndIndices |> List.map (fun (_,indices) -> indices))
                                                   | _ -> None

let getResult (x, y) howManyInARow getNextX getNextY pitch = getValuesWithIndices x getNextX y getNextY howManyInARow pitch
                                                                |> getMoveResult howManyInARow
    
let check (x,y) howManyInARow checkBounds calcResult pitch= match checkBounds (x,y) howManyInARow pitch with
                                                            | true -> None
                                                            | _ -> calcResult (x,y) howManyInARow pitch

let checkLeftBounds (x,_) howManyInARow _ = x+1 < howManyInARow
let checkRightBounds (x,_) howManyInARow pitch = x + howManyInARow > Array2D.length1 pitch
let checkDownBounds (_,y) howManyInARow _ = y+1 < howManyInARow
let checkUpBounds (_,y) howManyInARow pitch = y + howManyInARow > Array2D.length2 pitch
let checkDownLeftBounds (x,y) howManyInARow pitch = checkLeftBounds::[checkDownBounds] 
                                                    |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)
let checkDownRightBounds (x,y) howManyInARow pitch = checkRightBounds::[checkDownBounds]
                                                     |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)
let checkUpLeftBounds (x,y) howManyInARow pitch = checkLeftBounds::[checkUpBounds]
                                                  |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)
let checkUpRightBounds (x,y) howManyInARow pitch = checkRightBounds::[checkUpBounds]
                                                   |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)

let leftResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x-1) (fun _ -> y) pitch
let rightResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x+1) (fun _ -> y) pitch
let downResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun _ -> x) (fun y -> y-1) pitch
let downLeftResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x-1) (fun y -> y-1) pitch
let downRigthResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x+1) (fun y -> y-1) pitch
let upLeftResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x-1) (fun y -> y+1) pitch
let upRightResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x+1) (fun y -> y+1) pitch


// Highlevel API checks
let leftCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkLeftBounds leftResult pitch
let rightCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkRightBounds rightResult pitch
let downCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkDownBounds downResult pitch
let downLeftCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkDownLeftBounds downLeftResult pitch
let downRightCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkDownRightBounds downRigthResult pitch
let upLeftCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkUpLeftBounds upLeftResult pitch
let upRightCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkUpRightBounds upRightResult pitch

let invertPitch pitch = Array2D.map (fun elem -> elem * (-1)) pitch

let won (x,y) howManyInARow pitch = 
    let addToCheckList f list = list @ [f]
    let checkList = []
                    |> addToCheckList leftCheck 
                    |> addToCheckList rightCheck
                    |> addToCheckList downCheck
                    |> addToCheckList downLeftCheck
                    |> addToCheckList downRightCheck
                    |> addToCheckList upLeftCheck
                    |> addToCheckList upRightCheck
                    |> List.map (fun checkFun -> checkFun (x,y) howManyInARow pitch)
    let rec getResults results acc =
            match results with
            | [] -> acc
            | h::t -> match h with
                      | Won(cords) -> getResults t (cords::acc)
                      | None -> getResults t acc
    getResults checkList []

// Here the magic happens
let game (p1: ConnectFour) (p2: ConnectFour) howManyInARow (startPitch: int[,]) afterEachMove=
    
    //TODO: check if howManyInARow is possible to the given pitch

    // check for max moves
    // check wether x*y % 2 == 0 --> otherwise --> invalid pitch 
    // example: 3*3 = 9; 9/2 = 4,5 --> not valid because on player could do more moves than the other
    let maxMoves = match (Array2D.length1 startPitch, Array2D.length2 startPitch) with
                   | (maxX,maxY) when (maxX * maxY) % 2 = 0 -> maxX*maxY
                   | _ -> failwith ("Invalid Pitch. The number of Slots modulo two has to be zero.")
    
    let players = [|p1;p2|]
    
    let rec move (player: ConnectFour) pitchSoFar moveCount log nextPlayerIndex=
        if Seq.length log <> 0 then afterEachMove(log)
        let column = player.Move(pitchSoFar)
        match isValidMove column pitchSoFar with
        | Valid(col,row) -> match won (col ,row) howManyInARow (makeMove col pitchSoFar) with
                            | [] -> match moveCount with 
                                    | count when count < maxMoves -> let playerMove = UsualMove(player.Name,(col,row))
                                                                     let player = players.[nextPlayerIndex]
                                                                     let playerIndex = match nextPlayerIndex with
                                                                                       | 0 -> 1
                                                                                       | 1 -> 0
                                                                                       | _ -> failwith "Invalid player index" // should never occour
                                                                     let invertedPitch = invertPitch pitchSoFar
                                                                     move player invertedPitch (count+1) ([playerMove] @ log ) playerIndex
                                    | _ ->  [Tie(player.Name,(col,row))] @ log
                                            |> List.rev
                                            //(UsualMove(player.Name,(col,row))::log) // Tie
                            | wonResults -> let wonMove = WonMove(player.Name,(col,row), wonResults)
                                            [wonMove] @ log
                                            |> List.rev 
        | Invalid((col,row),errorMessage) -> //let usualMove = UsualMove(player.Name,(col,row))
                                             let message = "(" + col.ToString() + "," + row.ToString() + ") INVALID MOVE: " + errorMessage
                                             let failMove = FailMove(player.Name, message, (col,row))
                                             [failMove] @ log
                                             |> List.rev 
    
    move players.[0] startPitch 1 [] 1 //(Array2D.create 7 6 0)

//TODO: to extension ()
let gameInterOp 
        (p1: ConnectFour)
        (p2: ConnectFour)
        howManyInARow
        (startPitch: int[,]) 
        =
          game p1 p2 howManyInARow startPitch (fun _ ->()) //TODO: interop
          |> Seq.map (fun log ->
                        match log with
                        | WonMove (player,wonCoords, coordinates) ->
                             WonMoveInterOp(player,wonCoords, coordinates 
                                                              |> Seq.map (fun i -> Seq.cast<System.Collections.Generic.IEnumerable<_>> i)
                                                              |> Seq.cast<System.Collections.Generic.IEnumerable<_>>)
                        | _ -> log )
                                                                                            
                                                                                         