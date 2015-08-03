using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Bot
{
    public class Connect4Random : Connect4Challenge.Interface.ConnectFour
    {
        /* Note that it's pretty important NOT to create a new instance each time you need a random number.
         * You should reuse the old instance to achieve uniformity in the generated numbers
         * http://stackoverflow.com/a/2019432/3782012 
         * http://stackoverflow.com/a/768001/3782012 */
        static Random rnd = new Random();

        public override string Name
        {
            get { return "Connect4Random"; }
        }

        public override int Move(int[,] pitch)
        {
            /* 0 = not set
             * 1 = own token
             * -1 = opposing token 
             */

            /* Example
              Y  _____________
              5 |_|_|_|_|_|_|_|
              4 |_|_|_|_|_|_|_|
              3 |_|_|_|_|_|_|_|
              2 |_|_|_|_|x|_|_|
              1 |_|x|_|_|x|x|_| 
              0 |_|x|x|_|x|x|_| 
                 0 1 2 3 4 5 6  X
             */


            /* Example
             Y  _____________
             5 | 0| 0| 0| 0| 0| 0| 0|
             4 | 0| 0| 0| 0| 0| 0| 0|
             3 | 0| 0| 0| 0| 0| 0| 0|
             2 | 0| 0| 0| 0| 1| 0| 0|
             1 | 0|-1| 0| 0|-1|-1| 0| 
             0 | 0| 1|-1| 0| 1| 1| 0| 
                0 1 2 3 4 5 6  X
            */

            /* Default 7x6 field */
            int pitchSizeX = pitch.GetLength(0);
            int pitchSizeY = pitch.GetLength(1);
            //List<int[]> possibleMoves = new List<int[]>();

            //for (int x = 0; x < pitchSizeX; x++)
            //{
            //    for (int y = 0; y < pitchSizeY; y++)
            //    {
            //        if (pitch[x, y] == 0)
            //        {
            //            possibleMoves.Add(new int[2] { x, y });
            //            break;
            //        }
            //    }
            //}

            //int rndNumber = rnd.Next(possibleMoves.Count);
            //int[] rndMove = possibleMoves[rndNumber];

            //return rndMove[0];

            List<int> possibleMoves = new List<int>();

            for (int x = 0; x < pitchSizeX; x++)
            {
                for (int y = 0; y < pitchSizeY; y++)
                {
                    if (pitch[x, y] == 0)
                    {
                        possibleMoves.Add(x);
                        break;
                    }
                }
            }

            int rndNumber = rnd.Next(possibleMoves.Count);
            int rndMoveX = possibleMoves[rndNumber];

            return rndMoveX;
        }


    }
}
