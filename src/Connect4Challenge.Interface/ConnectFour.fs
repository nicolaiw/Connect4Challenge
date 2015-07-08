namespace Connect4Challenge.Interface


//Becaus in F# interfaces will be implemented explicitly, there are some issues with the Sandboxer i can't resolve yet.
//But ist not realy a problem to use an AbstractClass instead so .. use it :)
[<AbstractClass>]
type ConnectFour() =
    //inherit System.MarshalByRefObject()
    abstract member Name : string with get
    abstract member Move: pitch:int[,] -> int

