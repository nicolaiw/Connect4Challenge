module Connect4Challenge.Tests

open Connect4Challenge
open NUnit.Framework
open Connect4

[<Test>]
let ``should return 0 (getLine)`` () =
    let array = Array2D.create 7 6 0
    let line = getLine 2 array
    printfn "ACTUAL %i" line
    Assert.AreEqual(line, 0)

[<Test>]
let ``should return 2 (getLine)`` () =
    let array = Array2D.create 7 6 0
    array.[2, 0] <- 1
    array.[2, 1] <- -1

    let line = getLine 2 array
    printfn "ACTUAL %i" line
    Assert.AreEqual(line, 2)

[<Test>]
let ``shuld return Won (checkLeftWard)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|_|_|_|_|
    3 |_|_|_|_|_|_|_|
    2 |_|_|_|_|_|_|_|
    1 |_|_|_|_|_|_|_| 
    0 |x|x|x|x|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[0,0] <- 1
    array.[1,0] <- 1
    array.[2,0] <- 1
    array.[3,0] <- 1

    let res = leftCheck (3,0) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)

[<Test>]
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

    let res = rightCheck (3,1) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)

[<Test>]
let ``shuld return Won (checkDownWard)`` () =
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

    let res = downCheck (3,4) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)


[<Test>]
let ``shuld return Won (checkDownLeftBounds)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|x|_|_|_|
    3 |_|_|x|_|_|_|_|
    2 |_|x|_|_|_|_|_|
    1 |x|_|_|_|_|_|_| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[0,1] <- 1
    array.[1,2] <- 1
    array.[2,3] <- 1
    array.[3,4] <- 1

    let res = downLeftCheck (3,4) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)

[<Test>]
let ``shuld return Won (checkUpRightWard)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|x|_|_|_|
    3 |_|_|x|_|_|_|_|
    2 |_|x|_|_|_|_|_|
    1 |x|_|_|_|_|_|_| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[0,1] <- 1
    array.[1,2] <- 1
    array.[2,3] <- 1
    array.[3,4] <- 1

    let res = upRightCheck (0,1) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)

[<Test>]
let ``shuld return Won (checkDownRightWard)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|x|_|_|_|
    3 |_|_|_|_|x|_|_|
    2 |_|_|_|_|_|x|_|
    1 |_|_|_|_|_|_|x| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[3,4] <- 1
    array.[4,3] <- 1
    array.[5,2] <- 1
    array.[6,1] <- 1

    let res = downRightCheck (3,4) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)

[<Test>]
let ``shuld return Won (checkUpLeftWard)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|x|_|_|_|
    3 |_|_|_|_|x|_|_|
    2 |_|_|_|_|_|x|_|
    1 |_|_|_|_|_|_|x| 
    0 |_|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    array.[3,4] <- 1
    array.[4,3] <- 1
    array.[5,2] <- 1
    array.[6,1] <- 1

    let res = upLeftCheck (6,1) 4 array

    let moveResult = match res with
                        |Won(list) -> list 
                                        |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                        |> ignore
                                      true
                        |None -> false

    Assert.AreEqual(true, moveResult)



[<Test>]
let ``invert pitch (sould return -4)`` () =
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

    let res = invertetPitch.[3..3, 1..4] |> Seq.cast<int> |> Seq.reduce (fun acc elem -> acc + elem)
    
    printfn "AFTER: %i" res

    Assert.AreEqual(-4, res)

