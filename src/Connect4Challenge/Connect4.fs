module Connect4

open Connect4Challenge
//open Array2D

let isValidMove (move: int) (pitch: int[,]) = 
    match move with
    | _ when move < 0 || move > Array2D.length1 pitch  -> false // between 0 and 5
    | _ when pitch.[move, Array2D.length2 pitch] <> 0 -> false  // check if column is not full
    | _ -> true

let makeMove column (pitch: int[,]) = 
    let rec testLoop currentLineIndex=
        match currentLineIndex with
        | i when pitch.[column, currentLineIndex] = 0 -> pitch.[column, i] <- 1
                                                         pitch
        | i -> testLoop (i+1)
    testLoop 0

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
        | true -> 
                  match won column (getLine column pitchSoFar) (makeMove column pitchSoFar) with
                  | true -> player
                  | _ -> move getNextPlayer pitchSoFar
        | _ -> failwith "INVALID MOVE"
    move players.[playerIndex] (Array2D.create 7 6 0)

    