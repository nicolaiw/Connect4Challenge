namespace Connect4Challenge.Interface

type IConnectFour =
    abstract member Move: pitch:int[,] -> int

