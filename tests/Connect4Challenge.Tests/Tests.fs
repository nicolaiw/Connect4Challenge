module Connect4Challenge.Tests

open NUnit.Framework
open Connect4Challenge.RunTime
open Connect4Challenge.Interface

[<Test>]
let ``should return 0 (getLine)`` () =
    let array = Array2D.create 7 6 0
    let line = getLine 2 array
    printfn "ACTUAL %i" line.Value
    Assert.AreEqual(line.Value, 0)

[<Test>]
let ``should return 2 (getLine)`` () =
    let array = Array2D.create 7 6 0
    array.[2, 0] <- 1
    array.[2, 1] <- -1

    let line = getLine 2 array
    printfn "ACTUAL %i" line.Value
    Assert.AreEqual(line.Value, 2)

[<Test>]
let ``should return Won (checkLeftWard)`` () =
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
let ``should return Won (checkRightWard)`` () =
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
let ``should return Won (checkDownWard)`` () =
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
let ``should return Won (checkDownLeftBounds)`` () =
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
let ``should return Won (checkUpRightWard)`` () =
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
let ``should return Won (checkDownRightWard)`` () =
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
let ``should return Won (checkUpLeftWard)`` () =
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
let ``should return Won (downLeft, downRight, right)`` () =
    let array = Array2D.create 7 6 0

    (*
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|_|_|_|_|
    3 |_|_|_|x|x|x|x|
    2 |_|_|x|_|x|_|_|
    1 |_|x|_|_|_|x|_| 
    0 |x|_|_|_|_|_|x| 
       0 1 2 3 4 5 6  
    *)

    array.[0,0] <- 1
    array.[1,1] <- 1
    array.[2,2] <- 1
    array.[3,3] <- 1
    array.[4,3] <- 1
    array.[5,3] <- 1
    array.[6,3] <- 1
    array.[4,2] <- 1
    array.[5,1] <- 1
    array.[6,0] <- 1


    let res = won (3,3) 4 array



    let rec result r acc = match r with
                            |h::t-> h 
                                    |> List.map (fun (x,y) -> printfn "x:%i y:%i" x y)
                                    |> ignore
                                    result t true
                            |[] -> acc

    let detectWon = result res false


    Assert.AreEqual(true, detectWon)



[<Test>]
let ``invert pitch (should return -4)`` () =
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


type P1() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_1"
            override this.Move pitch =  0
type P2() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_2"
            override this.Move pitch =  3
[<Test>]
let ``Test game (Player 1 should win)`` () =
    let pitch = Array2D.create 7 6 0

    (*
       p1    p2
       _____________
    5 |_|_|_|_|_|_|_|
    4 |_|_|_|_|_|_|_|
    3 |x|_|_|x|_|_|_|
    2 |x|_|_|x|_|_|_|
    1 |x|_|_|x|_|_|_| 
    0 |x|_|_|x|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    let p1 = new P1()
    let p2 = new P2()

    let gameLog = game p1 p2 4 pitch

    let printLog = for i in gameLog do printfn "%A" i

    printLog

    let player = match gameLog |> Seq.last with
                 | WonMove(player, _) -> player
                 | _ -> "NOT EXPECTED"

    printfn "ACTUAL: %s" player

    Assert.AreEqual("P_1", player)




type P3() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_3"
            override this.Move pitch =  0

type P4() =
   inherit ConnectFour() with 
            override this.Name with get() = "P_4"
            override this.Move pitch =  3
//_________________________________________________________________________

[<Test>]
let ``Test game (invalid move p1)`` () =
    let pitch = Array2D.create 7 6 0

    (*
       p3
       p4
       _____________
    5 |4|_|_|_|_|_|_|
    4 |3|_|_|_|_|_|_|
    3 |3|_|_|_|_|_|_|
    2 |2|_|_|_|_|_|_|
    1 |4|_|_|_|_|_|_| 
    0 |3|_|_|_|_|_|_| 
       0 1 2 3 4 5 6  
    *)

    let p3 = new P3()
    let p4 = new P4()

    let gameLog = game p3 p4 4 pitch

    let printLog = for i in gameLog do printfn "%A" i

    printLog

    let (x,y) = match gameLog |> Seq.last with
                 |FailMove(_,_,(x,y)) -> (x,y)
                 |_ -> failwith "unexpected result"

    let p = match gameLog |> Seq.last with
                 |FailMove(p,_,_) -> p
                 |_ -> failwith "unexpected result"

    Assert.AreEqual(0, x)
    Assert.AreEqual(5, y)
    Assert.AreEqual("P_3", p)


//    match gameLog[gameLog.Length -1] with
//    | WonMove(player, wonCoords) -> printfn "PLAYER: %s" player
//                                    match wonCoords with
//                                    |

