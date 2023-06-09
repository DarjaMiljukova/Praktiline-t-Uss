//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Uss2
//{
//    public class FoodManager
//    {
//        private List<Food> foodItems;
//        private Random random;

//        public FoodManager()
//        {
//            foodItems = new List<Food>();
//            random = new Random();
//        }

//        public void GenerateFood(int count)
//        {
//            for (int i = 0; i < count; i++)
//            {
//                Position position = GenerateRandomPosition();
//                char symbol = GetRandomSymbol();
//                int points = GetRandomPoints();
//                FoodCreator food = new FoodCreator(position, symbol, Point);
//                foodItems.Add(food);
//            }
//        }

//        private Position GenerateRandomPosition()
//        {
//            int row = random.Next(1, VerticalLine + 1);
//            int col = random.Next(1, HorizontalLine + 1);
//            return new Position(row, col);
//        }

//        private char GetRandomSymbol()
//        {
//            char[] symbols = { '@', '$', '*', '%' };
//            int index = random.Next(symbols.Length);
//            return symbols[index];
//        }

//        private int GetRandomPoints()
//        {
//            int[] points = { 10, 20, 30, 40 };
//            int index = random.Next(points.Length);
//            return points[index];
//        }

//        public List<Food> GetFoodItems()
//        {
//            return foodItems;
//        }
//    }
//}
