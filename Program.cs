using System;
using System.Collections.Generic;
using System.Linq;

namespace TopNBuzzwords
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputToys = new List<string>
            {
                "elmo",
                "elsa",
                "elsa dolls",
                "legos",
                "drone",
                "tablet",
                "warcraft"
            };

            var inputQuotes = new List<string>
            {
                "Elmo is the hottest of the season! Elmo will be on every kid's wishlist!",
                "The new Elmo dolls are super high quality",
                "Expect the Elsa dolls to be very popular this year, Elsa!",
                "Elsa and Elmo are the toys I'll be buying for my kids, Elsa is good",
                "For parents of older kids, look into buying them a drone",
                "Warcraft is slowly rising in popularity ahead of the holiday season"
            };

            var results = FindBuzzWords(
                7,
                7,
                inputToys,
                6,
                inputQuotes);

            foreach (var toy in results)
            {
                Console.WriteLine(toy);
            }

            Console.ReadKey();
        }

        private static List<string> FindBuzzWords(
            int numToys,
            int topToys,
            List<string> Toys,
            int numQuotes,
            List<string> Quotes)
        {
            var amountOfBuzzwords = topToys;

            if (amountOfBuzzwords > numToys)
            {
                amountOfBuzzwords = numToys;
            }

            var toysAndOccurrences = new Dictionary<string, int>();

            foreach (var toy in Toys)
            {
                foreach (var quote in Quotes)
                {
                    var occurrencesForThisQuoteAndToy = CountToyOccurrences(toy, quote);

                    if (toysAndOccurrences.ContainsKey(toy))
                    {
                        toysAndOccurrences[toy] = toysAndOccurrences[toy] + occurrencesForThisQuoteAndToy;
                    }
                    else
                    {
                        toysAndOccurrences.Add(toy, occurrencesForThisQuoteAndToy);
                    }
                }
            }

            var fullToysList = toysAndOccurrences
                .OrderByDescending(x => x.Value)
                .ThenByDescending(y => y.Key)
                .Select(y => y.Key)
                .ToList();

            fullToysList.RemoveRange(amountOfBuzzwords, fullToysList.Count - amountOfBuzzwords);

            return fullToysList;
        }

        public static int CountToyOccurrences(string toyName, string quote)
        {
            var amountOfOccurrences = 0;
            var i = 0;
            while ((i = quote.IndexOf(toyName, i, StringComparison.InvariantCultureIgnoreCase)) != -1)
            {
                i += toyName.Length;
                amountOfOccurrences++;
            }

            return amountOfOccurrences;
        }
    }
}