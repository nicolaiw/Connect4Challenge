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

let won x y pitch = true

let game (p1: IConnectFour) (p2: IConnectFour) =
    let rec move (player: IConnectFour) (pitchSoFar:int[,]) =
        let move = player.Move(pitchSoFar)
        match isValidMove move pitchSoFar with
        | true -> makeMove move pitchSoFar
        | _ -> failwith "Invalid move"
    move p1 (Array2D.create 7 6 0)

    