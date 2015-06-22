module Connect4

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

open Connect4Challenge

type Move =       
   | Invalid of string          
   | Valid
   
type MoveResult =
    | Won of (int*int) list //return the coordinates
    | None

let isValidMove column pitch = 
    match column with
    | i when i < 0 || i > Array2D.length1 pitch  -> Invalid("Just columns between 0 and " + ((Array2D.length1 pitch)-1).ToString() + " are Valid.\n Given: " + i.ToString()) // between 0 and 6 (on a 7x6 pitch)
    | i when pitch.[i, Array2D.length2 pitch] <> 0 -> Invalid("Column " + i.ToString() + " is Full.") // check if column is full
    | _ -> Valid

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
                                                                |> List.reduce (fun acc elem -> acc + elem)
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
let checkDownLeftBounds (x,y) howManyInARow pitch = checkLeftBounds::checkDownBounds::[] 
                                                    |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)
let checkDownRightBounds (x,y) howManyInARow pitch = checkRightBounds::checkDownBounds::[]
                                                     |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)
let checkUpLeftBounds (x,y) howManyInARow pitch = checkLeftBounds::checkUpBounds::[]
                                                  |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)
let checkUpRightBounds (x,y) howManyInARow pitch = checkRightBounds::checkUpBounds::[]
                                                   |> List.exists (fun checkFun -> checkFun (x,y) howManyInARow pitch)


let leftResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x-1) (fun y -> y) pitch
let rightResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x+1) (fun y -> y) pitch
let downResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x) (fun y -> y-1) pitch
let downLeftResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x-1) (fun y -> y-1) pitch
let downRigthResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x+1) (fun y -> y-1) pitch
let upLeftResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x-1) (fun y -> y+1) pitch
let upRightResult (x,y) howManyInARow pitch = getResult (x,y) howManyInARow (fun x -> x+1) (fun y -> y+1) pitch


//Highlevel API checks
let leftCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkLeftBounds leftResult pitch
let rightCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkRightBounds rightResult pitch
let downCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkDownBounds downResult pitch
let downLeftCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkDownLeftBounds downLeftResult pitch
let downRightCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkDownRightBounds downRigthResult pitch
let upLeftCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkUpLeftBounds upLeftResult pitch
let upRightCheck (x,y) howManyInARow pitch = check (x,y) howManyInARow checkUpRightBounds upRightResult pitch

let invertPitch pitch = Array2D.map (fun elem -> elem * (-1)) pitch

let won (x,y) howManyInARow pitch = false

let getLine x pitch = 
    let rec lineLoop lineIndex slotValue =
        match slotValue with
        | _ when lineIndex >= Array2D.length2 pitch -> failwith "No free slot found on column " + x
        | 0 -> lineIndex-1
        | _ -> lineLoop (lineIndex+1) pitch.[x, lineIndex]
    lineLoop 0 999

let game (p1: IConnectFour) (p2: IConnectFour) howManyinARow (startPitch: int[,]) =
    let players = [|p1;p2|]
    let mutable playerIndex = 0

    let getNextPlayer = 
        match playerIndex with
        | 0 -> playerIndex <- playerIndex+1
        | 1 -> playerIndex <- playerIndex-1
        |_ -> failwith "Invalid player index" // should never appear
        players.[playerIndex]

    let rec move (player: IConnectFour) pitchSoFar =
        let column = player.Move(pitchSoFar)
        match isValidMove column pitchSoFar with
        | Valid ->        // x        y                           usualy 4       pitch with modified state
                  match won (column, (getLine column pitchSoFar)) howManyinARow (makeMove column pitchSoFar) with
                  | true -> player
                  | _ -> move getNextPlayer pitchSoFar
        | Invalid(txt) -> failwith ("INVALID MOVE: " + txt)
    move players.[playerIndex] startPitch //(Array2D.create 7 6 0)

    