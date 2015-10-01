using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship2
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleShipBoard board = new BattleShipBoard(10);
            board.InitShips();


            board.PrintBoard();

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Console.WriteLine();

                    board.Fire(j, i);
                    board.PrintBoard();
                    if (board.IsGameDone())
                        break;

                }
                if (board.IsGameDone())
                    break;
            }

            Console.WriteLine("HitCount: " +board.hitCount);
            Console.ReadLine();
        }
    }

    public class BattleShipBoard
    {
        private int size;
        private string[,] board;
        public int hitCount = 0;
        // ships
        int[] ships =  {5,4,3,3,2};
        Random random = new Random();


        public BattleShipBoard(int size)
        {
            this.size = size;
            board = new string[size,size];
        }

        public void InitShips() {
            foreach(int shipSize in ships) {
                while (true)
                {
                    var isVertical = random.Next(0, 2) == 0;
                    var isDone = pasteShip(isVertical, shipSize);
                    if (isDone)
                        break;
                }
                
            }
        }

        private bool pasteShip(bool isVertical, int shipSize)
        {
                var startRowIndex = random.Next(0, isVertical ? size - shipSize  : size );
                var startColumnIndex = random.Next(0, !isVertical ? size - shipSize  : size );
                var canPaste = true;
                for (int i = 0; i < shipSize; i++)
                {
                    var column = getNextColumnIndex(isVertical, startColumnIndex, i);
                    var row = getNextRowIndex(isVertical, startRowIndex, i);
                    if (board[column, row] == "X")
                    {
                        canPaste = false;
                        break;
                    }
                }


                if (canPaste)
                {
                    for (int i = 0; i < shipSize; i++)
                    {
                        var column = getNextColumnIndex(isVertical, startColumnIndex, i);
                        var row = getNextRowIndex(isVertical, startRowIndex, i);
                        board[column, row] = "X";
                    }
                }

                return canPaste;
        }

        private static int getNextRowIndex(bool isVertical, int startRowIndex, int i)
        {
            return isVertical ? startRowIndex + i : startRowIndex;
        }

        private static int getNextColumnIndex(bool isVertical, int startColumnIndex, int i)
        {
            return !isVertical ? startColumnIndex + i : startColumnIndex;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(board[j,i] ?? ".");
                }
                Console.WriteLine();
            }
        }


        internal void Fire(int column, int row)
        {
            column--;
            row--;
            hitCount++;
            board[column, row] = "*";
        }

        public bool IsGameDone()
        {
            foreach (var value in board)
            {
                if (value == "X")
                    return false;
            }
            
            return true;
        }
    }

}
