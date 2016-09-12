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

    public struct reactionEntry
    {
        public string x;
        public reactionEntry(string _x)
        {
            x = _x;
        }
    }

    class reactionTable
    {
        static Dictionary<reactionKey, reactionEntry> table = new Dictionary<reactionKey, reactionEntry>();
        public void loadTable()
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
                    reactionEntry entry = new reactionEntry(entries[i]);
                    table.Add(key, entry);
                }
            }

            // Should be testing out here, my bad
            reactionKey _key = new reactionKey("hydrochloricAcid", "lead");
            // Test retrieval
            Console.WriteLine(getEntry(_key).x);

        }

        public static reactionEntry getEntry(reactionKey key)
        {
            reactionEntry ret;
            table.TryGetValue(key, out ret);
            return ret;
        }
    }
}