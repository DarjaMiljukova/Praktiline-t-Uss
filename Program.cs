using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Uss2;

namespace Uss2
{
    class Program
    {
        static ConsoleColor snakeColor = ConsoleColor.Red;
        static void Main(string[] args)
        {
            snakeColor = Menu();
            Console.Write("Sisestage oma nimi: ");
            string n = Console.ReadLine();
            Console.SetWindowSize(80, 25);
            Player player = new() { Name = n, Score = 0 };

            Console.Clear();




            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Отрисовка точек			
            Point p = new Point(4, 5, '¤');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '▲');
            Point food = foodCreator.CreateFood();
            food.Draw();

            FoodCreator food2Creator = new FoodCreator(80, 25, '/');
            Point food2 = food2Creator.CreateFood();
            food2.Draw();

            //{
            //    FoodManager foodManager = new FoodManager();
            //    foodManager.GenerateFood(3);

            //    List<Food> foodItems = foodManager.GetFoodItems();
            //    foreach (Food food in foodItems)
            //    {
            //        Console.WriteLine($"Position: {food.Position.Row}, {food.Position.Column}");
            //        Console.WriteLine($"Symbol: {food.Symbol}");
            //        Console.WriteLine($"Points: {food.Points}");
            //        Console.WriteLine();
            //    }
            //}

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver();
            Console.ReadLine();
        }



        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("FINISH", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("============================", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

        public static ConsoleColor ChooseSnakeColor()
        {
            int varv;
            ConsoleColor snakeColor;

            while (true)
            {
                Console.WriteLine("Valige madu värvus:\n1 - Lilla:\n2 - Kollane:\n3 - Punane:\n4 - Sinine:\n5 - Roheline");
                varv = int.Parse(Console.ReadLine());

                if (varv >= 1 && varv <= 5)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vigane sisend. Palun proovi uuesti.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            switch (varv)
            {
                case 1:
                    snakeColor = ConsoleColor.Magenta;
                    break;
                case 2:
                    snakeColor = ConsoleColor.Yellow;
                    break;
                case 3:
                    snakeColor = ConsoleColor.Red;
                    break;
                case 4:
                    snakeColor = ConsoleColor.Blue;
                    break;
                case 5:
                    snakeColor = ConsoleColor.Green;
                    break;
                default:
                    snakeColor = ConsoleColor.DarkRed;
                    break;
            }

            return snakeColor;
        }






        static ConsoleColor Menu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.WriteLine("----------\n1 - Igra\n2 - Top igrokov\n3 - Vibor cvetov\n----------");
                int v = int.Parse(Console.ReadLine());
                if (v == 1)
                {
                    break;
                }
                else if (v == 2)
                {
                    string[] lines = File.ReadAllLines("../../../Scores.txt");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else if (v == 3)
                {
                    snakeColor = ChooseSnakeColor();
                }
            }
            return snakeColor;
        }
    }
}
//static void Draw(int x, int y, char sym)
//{
//    Console.SetCursorPosition(x, y);
//    Console.Write(sym);

//Music_mäng = new music();
//ConsoleKeyInfo nupp = neew ConsoleKeyInfo();
//_ = mäng.Tagaplaanis_Mangida("");