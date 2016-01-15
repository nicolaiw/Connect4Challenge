namespace Connect4Challenge.TestImplementation

open Connect4Challenge.Interface
open System


type ConFour() =
   inherit ConnectFour() with 
            override this.Name with get() = "Bar"
            override this.Move pitch =  //let stream = System.IO.File.OpenText(@"C:\Temp\tst.txt")
                                        1


