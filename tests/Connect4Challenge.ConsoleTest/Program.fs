open Connect4


let ``shuld return Won (checkRightWard)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|_|_|_|_|
    3 |_|_|_|_|_|_|_|
    2 |_|_|_|_|_|_|_|
    1 |_|_|_|x|x|x|x| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[3,1] <- 1
    array.[4,1] <- 1
    array.[5,1] <- 1
    array.[6,1] <- 1

    let res = checkRightWard (3,1) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false
    moveResult

[<EntryPoint>]
let main argv = 

    let res = ``shuld return Won (checkRightWard)`` () 

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
