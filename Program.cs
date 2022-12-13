using System;
using System.Threading;

namespace Car
{
    class Program
    {
        static int firstPlayerPadSize = 2;
        static int secondPlayerPadSize = 4;
        static int ballPositionX = 0;
        static int ballPositionY = 0;
        static bool ballDirectionUp = true;
        static bool ballDirectionRight = false;
        static int firstPlayerPosition = 0;
        static int secondPlayerPosition = 0;
        static int firstPlayerResult = 0;
        static int secondPlayerResult = 0;
        static Random randomGenerator = new Random();

        static void RemoveScrollBars()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        static void DrawFirstPlayer()
        {
            for (int y = firstPlayerPosition; y < firstPlayerPosition + firstPlayerPadSize; y++)
            {
                PrintAtPosition(0, y, '|');
                PrintAtPosition(1, y, '|');

            }
        }

         static void PrintAtPosition(int x, int y, char simbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(simbol);
        }

        static void DrawSecondPlayer()
        {
            for (int y = secondPlayerPosition; y < secondPlayerPosition + secondPlayerPadSize; y++)
            {
                PrintAtPosition(Console.WindowWidth - 1, y, '|');
                PrintAtPosition(Console.WindowWidth - 2, y, '|');
            }
        }

        static void SetInitialPositions()
        {
            firstPlayerPosition = Console.WindowHeight / 2 - firstPlayerPadSize / 2;
            secondPlayerPosition = Console.WindowHeight / 2 - secondPlayerPadSize / 2;
            SetBallAtTheMiddleOfTheGameFieled();

        }

        static void SetBallAtTheMiddleOfTheGameFieled()
        {
            ballPositionX = Console.WindowWidth / 2;
            ballPositionY = Console.WindowHeight / 2;
        }

        static void DrawBall()
        {
            PrintAtPosition(ballPositionX, ballPositionY, '@');
        }
        static void PrintResult()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
            Console.Write("{0}-{1}", firstPlayerResult, secondPlayerResult);
        }

        static void MoveFirstPlayerDown()
        {
            if (firstPlayerPosition < Console.WindowHeight - firstPlayerPadSize)
            {
                firstPlayerPosition++;
            }
        }

        static void MoveFirstPlayerUp()
        {
            if (firstPlayerPosition > 0)
            {
                firstPlayerPosition--;
            }
        }

        static void MoveSecondPlayerDown()
        {
            if (secondPlayerPosition < Console.WindowHeight - secondPlayerPadSize)
            {
                secondPlayerPosition++;
            }
        }

        static void MoveSecondPlayerUp()
        {
            if (secondPlayerPosition > 0)
            {
                secondPlayerPosition--;
            }
        }

        static void SecondPlayerAIMove()
        {
            int randomNumber = randomGenerator.Next(0, 2);
            //if (randomNumber == 0)
            //{
            //    MoveSecondPlayerUp();
            //}
            //if (randomNumber == 1)
            //{
            //    MoveSecondPlayerDown();
            //}
            if (randomNumber == 0)
            {

                if (ballDirectionUp == true)
                {
                    MoveSecondPlayerUp();
                }
                else
                {
                    MoveSecondPlayerDown();
                }
            }
        }

        private static void MoveBall()
        {
            if (ballPositionY == 0)
            {
                ballDirectionUp = false;
            }
            if (ballPositionY == Console.WindowHeight - 1)
            {
                ballDirectionUp = true;
            }
            if (ballPositionX == Console.WindowWidth - 1)
            {
                SetBallAtTheMiddleOfTheGameFieled();
                ballDirectionRight = false;
                ballDirectionUp = true;
                firstPlayerResult++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("First player wins!");
                Console.ReadKey();
            }
            if (ballPositionX == 0)
            {
                SetBallAtTheMiddleOfTheGameFieled();
                ballDirectionRight = true;
                ballDirectionUp = true;
                secondPlayerResult++;
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                Console.WriteLine("Second player wins!");
                Console.ReadKey();
            }
            if (ballPositionX < 3)
            {
                if (ballPositionY >= firstPlayerPosition && ballPositionY < firstPlayerPosition + firstPlayerPadSize)
                {
                    ballDirectionRight = true;
                }
            }
            if (ballPositionX >= Console.WindowWidth - 3 - 1)
            {
                if (ballPositionY >= secondPlayerPosition && ballPositionY < secondPlayerPosition + secondPlayerPadSize)
                {
                    ballDirectionRight = false;
                }
            }
            if (ballDirectionUp)
            {
                ballPositionY--;
            }
            else
            {
                ballPositionY++;
            }
            if (ballDirectionRight)
            {
                ballPositionX++;
            }
            else
            {
                ballPositionX--;
            }
        }

        static void Main(string[] args)
        {
            RemoveScrollBars();
            SetInitialPositions();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        MoveFirstPlayerUp();
                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        MoveFirstPlayerDown();
                    }
                }
                SecondPlayerAIMove();
                MoveBall();
                Console.Clear();
                DrawFirstPlayer();
                DrawSecondPlayer();
                DrawBall();
                PrintResult();
                Thread.Sleep(60);
            }
        }
    }
}
