using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // File reading

namespace QualScheme
{
    public struct reactionKey
    {
        public readonly string reagent;
        public readonly string solutionElement;
        public reactionKey(string _reagent, string _solutionElement)
        {
            reagent = _reagent;
            solutionElement = _solutionElement;
        }
    }

    class reactionTable
    {
        static Dictionary<reactionKey, string> table = new Dictionary<reactionKey, string>();
        public reactionTable()
        {
            Constants c = new Constants();
            StreamReader reader = new StreamReader(c.getTxtPath() + "group1reactions.csv");

            if (reader.Peek() == -1) // file doesnt exist
                return;

            string rawXAxis = reader.ReadLine();
            string[] xAxis = rawXAxis.Split(',');
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine(); 

                string[] entries = line.Split(',');

                for (int i = 1; i < entries.Length; i++)
                {
                    reactionKey key = new reactionKey(xAxis[i], entries[0]);
                    table.Add(key, entries[i]);
                }
            }
        }

        public string getEntry(reactionKey key)
        {
            string ret;
            if (table.TryGetValue(key, out ret))
                return ret;
            else
                return "INVALID KEY";
        }
    }
}