using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uss2
{
    class Score
    {
        private int score;
        private List<int> scoreList;

        public void AddScore(int p)
        {
            score += p;
        }

        public void SaveScore()
        {
            scoreList.Add(score);
            scoreList.Sort();
            scoreList.Reverse();
            using (StreamWriter to_file = new StreamWriter("Scores.txt")) /*запись символов в заданной кодировке*/
            {
                foreach (int score in scoreList)
                {
                    to_file.WriteLine(score);
                }
            }
        }

        private void ReadFile()
        {
            using (StreamReader from_file = new StreamReader("Scores.txt"))
            {
                string line = from_file.ReadLine();
                if (int.TryParse(line, out int score))
                {
                    scoreList.Add(score);
                }
            }
        }
    }
}