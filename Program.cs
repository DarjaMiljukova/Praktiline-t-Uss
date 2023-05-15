using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Uss2;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);

            HorizontalLine Upline = new HorizontalLine(0, 78, 0, '*');
            HorizontalLine Downline = new HorizontalLine(0, 78, 24, '*');
            VerticalLine Leftline = new VerticalLine(0, 24, 0, '*');
            VerticalLine Rightline = new VerticalLine(0, 24, 78, '*');
            Upline.Drow();
            Downline.Drow();
            Leftline.Drow();
            Rightline.Drow();

            Point p = new Point(4, 5, '*');
            p.Draw();
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

