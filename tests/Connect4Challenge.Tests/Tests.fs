module Connect4Challenge.Tests

open Connect4Challenge
open NUnit.Framework
open Connect4

[<Test>]
let ``should return 0`` () =
    let array = Array2D.create 7 6 0
    let line = getLine 2 array
    printfn "ACTUAL %i" line
    Assert.AreEqual(line, 0)

[<Test>]
let ``should return 2`` () =
    let array = Array2D.create 7 6 0
    array.[2, 0] <- 1
    array.[2, 1] <- -1

    let line = getLine 2 array
    printfn "ACTUAL %i" line
    Assert.AreEqual(line, 2)
