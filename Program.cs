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

            Console.Write("Sisesta nimi: ");
            string n = Console.ReadLine();

            Sound gameOverSound = new Sound("../../../inecraft_death.mp3");
            Sound BGSound = new Sound("../../../bg.mp3");
            BGSound.SetVolume(0.2f);
            BGSound.Play();

            List<Point> poisons = new List<Point>();

            while (n.Length < 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Nimi peab olema pikem kui 5 märki: ");
                n = Console.ReadLine();
            }
            Player player = new Player { Name = n, Score = 0 };

            Console.Clear();

            Console.SetWindowSize(80, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            int speed = 100;


            Point p = new Point(4, 5, '¤');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            Console.ForegroundColor = snakeColor;
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, 'x');
            Console.ForegroundColor = ConsoleColor.Red;
            Point food = foodCreator.CreateFood();
            food.Draw();

            FoodCreator scorefoodCreator = new FoodCreator(80, 25, '/');
            Console.ForegroundColor = ConsoleColor.Yellow;
            Point scor = scorefoodCreator.CreateFood();
            scor.Draw();



            //подставная еда
            FoodCreator poisonCreator = new FoodCreator(80, 25, '@');
            Console.ForegroundColor = ConsoleColor.White;
            Point poison = poisonCreator.CreateFood();
            poison.Draw();

            FoodCreator poison2Creator = new FoodCreator(80, 25, '@');
            Console.ForegroundColor = ConsoleColor.White;
            Point poison2 = poison2Creator.CreateFood();
            poison2.Draw();


            poisons.Add(poison);

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    using (StreamWriter writer = new StreamWriter("../../../Scores.txt", true))
                    {
                        writer.WriteLine(player.Name + ": " + player.Score);
                    }
                    break;
                }
                else if (snake.Poisoned(poisons))
                {
                    break;
                }
                else if (snake.Eat(food))
                {
                    Sound hrust = new Sound("../../../z_uk-kushaet.mp3");
                    hrust.Play();

                    Console.ForegroundColor = ConsoleColor.Red;
                    food = foodCreator.CreateFood();
                    food.Draw();

                    Console.ForegroundColor = ConsoleColor.White;
                    poison2 = poison2Creator.CreateFood();
                    poison2.Draw();

                    Console.ForegroundColor = ConsoleColor.White;
                    poisonCreator = new FoodCreator(80, 25, '@');
                    poison = poisonCreator.CreateFood();
                    poison.Draw();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    scor = scorefoodCreator.CreateFood();
                    scor.Draw();

                    poisons.Add(poison);

                    player.Score++;

                    speed = Speed(player.Score, speed);
                }
                else if (snake.Scored(scor))
                {
                    Sound hrust = new Sound("../../../z_uk-kushaet.mp3");
                    hrust.Play();
                    player.Score++;
                    player.Score++;
                    player.Score++;
                }
                else
                {
                    Console.ForegroundColor = snakeColor;
                    snake.Move();
                }
                Thread.Sleep(speed);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }

            BGSound.Stop();
            gameOverSound.Play();
            gameOverSound.SetVolume(0.2f);
            WriteGameOver();
            Console.ReadLine();
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("Autor see mäng Darja", xOffset, yOffset++);
        }

        static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

        static ConsoleColor Menu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.WriteLine(" 1 - Mäng \n 2 - Top mängijad \n 3 - Uss värv \n \n");
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

        public static ConsoleColor ChooseSnakeColor()
        {
            int varv;
            ConsoleColor snakeColor;

            while (true)
            {
                Console.WriteLine("Valige uss värv (vaikevärv punane): \n 1 - Sinine \n 2 - Valge \n 3 - Roheline \n 4 - Tsüaan \n 5 - Tumekollane \n 6 - Roosa \n 7 - Punane");
                varv = int.Parse(Console.ReadLine());

                if (varv >= 1 && varv <= 7)
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
                    snakeColor = ConsoleColor.Blue;
                    break;
                case 2:
                    snakeColor = ConsoleColor.White;
                    break;
                case 3:
                    snakeColor = ConsoleColor.Green;
                    break;
                case 4:
                    snakeColor = ConsoleColor.Cyan;
                    break;
                case 5:
                    snakeColor = ConsoleColor.DarkYellow;
                    break;
                case 6:
                    snakeColor = ConsoleColor.Magenta;
                    break;
                case 7:
                    snakeColor = ConsoleColor.Red;
                    break;
                default:
                    snakeColor = ConsoleColor.Red;
                    break;
            }

            return snakeColor;
        }

        public static int Speed(int score, int speed)
        {
            if (score == 3)
            {
                speed -= 10;
            }
            else if (score == 6)
            {
                speed -= 10;
            }
            else if (score == 9)
            {
                speed -= 10;
            }
            else if (score == 12)
            {
                speed -= 10;
            }
            else if (score == 15)
            {
                speed -= 10;
            }
            else if (score == 18)
            {
                speed -= 10;
            }
            return speed;
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
