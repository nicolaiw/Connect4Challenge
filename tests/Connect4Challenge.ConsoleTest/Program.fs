open Connect4


let ``invert pitch (sould return 0)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|x|_|_|_|
    3 |_|_|_|x|_|_|_|
    2 |_|_|_|x|_|_|_|
    1 |_|_|_|x|_|_|_| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[3,4] <- 1
    array.[3,3] <- 1
    array.[3,2] <- 1
    array.[3,1] <- 1
    
    printfn "BEFORE: %i" (array.[3..3, 1..4] |> Seq.cast<int> |> Seq.reduce (fun acc elem -> acc + elem))

    let invertetPitch = invertPitch array
    let slots = invertetPitch.[3..3, 1..4]

    let res = slots |> Seq.cast<int> |> Seq.reduce (fun acc elem -> acc + elem)
    
    printfn "AFTER: %i" res

[<EntryPoint>]
let main argv = 

    let res = ``invert pitch (sould return 0)`` ()

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
