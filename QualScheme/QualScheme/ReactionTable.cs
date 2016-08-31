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
        public readonly int funcNumber;
        public readonly int funcArg1;
        public readonly int funcArg2;
        public readonly int funcArg3;
        public reactionEntry(int _funcNumber, int _funcArg1, int _funcArg2, int _funcArg3)
        {
            funcNumber = _funcNumber;
            funcArg1 = _funcArg1;
            funcArg2 = _funcArg2;
            funcArg3 = _funcArg3;
        }
    }

    class reactionTable
    {
        static Dictionary<reactionKey, reactionEntry> table;
        public void loadTable()
        {
            // Windows Directory
            string winDir = System.Environment.GetEnvironmentVariable("windir");
            StreamReader reader = new StreamReader("..//..//test.txt");

            while (reader.Peek() != -1)
            {
                string s = reader.ReadLine(); // Store in a hashtable?
                Console.WriteLine(s);
            }
            // Github error test?
            // Parse and store as reactionEntrys

        }

        public static reactionEntry getEntry(reactionKey key)
        {
            reactionEntry ret;
            table.TryGetValue(key, out ret);
            return ret;
        }
    }
}