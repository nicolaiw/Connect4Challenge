namespace Connect4Challenge

type IConnectFour =
    abstract member Move: pitch:int[,] -> int

