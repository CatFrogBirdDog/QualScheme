using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // File reading

namespace QualScheme
{
    class Solution
    {
        private List<string> ions = new List<string>();
        public void generateSolution(bool group1, bool group2, bool group3, bool group4)
        {
            if (group1)
            {
                ions.AddRange(generateFromList("group1.csv"));
            }

            if (group2)
            {
                ions.AddRange(generateFromList("group2.csv"));
            }

            if (group3)
            {
                ions.AddRange(generateFromList("group3.csv"));
            }
            
            if (group4)
            {
                ions.AddRange(generateFromList("group4.csv"));
            }
        }

        public void printSolution()
        {
            // Lambda function to print to the screen
            ions.ForEach(el => Console.WriteLine(el));
        }

        public bool saveSolution()
        {
            // Write this solution to file

            return true;
        }

        private List<string> generateFromList(string file)
        {
            // Random Number Generator
            Random rand = new Random();

            List<string> returnList = new List<string>();

            Constants c = new Constants();
            StreamReader reader = new StreamReader(c.getTxtPath() + file);

            string rawIons = reader.ReadLine();
            string[] parsedIons = rawIons.Split(',');

            // While the list is empty (To ensure that at least 1 item is generated)
            while (returnList.Count == 0)
            {
                for (int i = 0; i < parsedIons.Length; i++)
                {
                    // If a random number between 1 and 100 is even, add the ions
                    if (rand.Next(1, 100) % 2 == 0)
                    {
                        returnList.Add(parsedIons[i]);
                    }
                }
            }

            return returnList;
        } 

    }
}
