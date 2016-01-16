# Connect4Challenge

## Description
Connect4Challenge is a programming challenge.

## Usage
You can Select two *.dll files. The two implemented algorithms will cempete aganist each other. You just have to implement the following abstract class.

``` fsharp
[<AbstractClass>]
type ConnectFour() =
    abstract member Name : string with get
    abstract member Move: pitch:int[,] -> int
```

``` csharp
public abstract class ConnectFour
{
	public override abstract string Name { get; }
	public abstract override int Move(int[,] pitch);
}
```
[See](https://github.com/Jallah/Connect4Challenge/blob/master/src/Connect4Challenge.Interface/ConnectFour.fs)
You can wirte your Code in every .NET Language (even in VB.Net :P)

## Explaination
The ``Name`` property is just your name or alias. The Move method gets a 2D Array. This param contains the current Pitch.

* 0 values = free slots
* -1 values = your opponent's turns
* 1 values = your turns

Your algorithm has to be return the a value between 0 and 6 (on an e.g. 7x6 pitch). This Value is the Columnd you want to insert.

1. Clone this repo
2. Open an new Project
3. On Windows run build.cmd on Linux run build.sh
4. Add a reference to your project to /bin/Connect4Challenge.Interface/Connect4Challenge.Interface.dll
5. Write your algorithm
6. Find an enmy
7. Upload the algorithms
8. Get the Result

Note: the first class in your assembly which implements the ConnectFour abstract class will be used. So it is suggested just to have one
class implementing this class.

## Build status

**Master Branch**

[![Build Status](https://travis-ci.org/Jallah/Connect4Challenge.svg?branch=master)](https://travis-ci.org/Jallah/Connect4Challenge)
[![Build status](https://ci.appveyor.com/api/projects/status/tngsbj4u54o90fit/branch/master?svg=true)](https://ci.appveyor.com/project/Jallah/connect4challenge/branch/master)

