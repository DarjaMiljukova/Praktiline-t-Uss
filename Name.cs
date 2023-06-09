using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uss2
{
    class Name
    {
        private int name;
        private List<int> nameList;


        public void AddScore(int p)
        {
            name += p;
        }

        public void SaveName()
        {
            nameList.Add(name);
            nameList.Sort();
            nameList.Reverse();
            using (StreamWriter to_file = new StreamWriter("Names.txt"))
            {
                foreach (int name in nameList)
                {
                    to_file.WriteLine(name);
                }
            }
        }

        private void ReadFile()
        {
            using (StreamReader from_file = new StreamReader("Names.txt"))
            {
                string line = from_file.ReadLine();
                if (int.TryParse(line, out int name))
                {
                    nameList.Add(name);
                }
            }
        }
    }
}
