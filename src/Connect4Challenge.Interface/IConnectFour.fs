namespace Connect4Challenge.Interface

type IConnectFour =
    abstract member Name : string with get
    abstract member Move: pitch:int[,] -> int

