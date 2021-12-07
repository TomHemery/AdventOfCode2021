using System.Collections.Generic;
using System;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day3: Problem 
    {
        public Day3(string inputPath): base(inputPath)
        {

        }

        public override void Part1()
        {
            int[] gamma = new int[puzzleInput[0].Length];
            int[] epsilon = new int[puzzleInput[0].Length];
            foreach (string line in puzzleInput) {
                for (int i = 0; i < line.Length; i++) {
                    gamma[i] += line[i] == '1' ? 1 : -1;
                }
            }
            for (int i = 0; i < gamma.Length; i++) {
                gamma[i] = gamma[i] >= 0 ? 1 : 0; 
                epsilon[i] = gamma[i] == 0 ? 1 : 0;
            }
            long gammaVal = binaryStringToInt(string.Join("", gamma));
            long epsilonVal = binaryStringToInt(string.Join("", epsilon));
            long powerConsumption = gammaVal * epsilonVal;

            Console.WriteLine("Power consumption: " + powerConsumption);
        }

        public override void Part2()
        {
            List<string> filteredList = new List<string>(puzzleInput);
            int i = 0;
            Console.WriteLine(filteredList.Count());
            while(filteredList.Count() > 1) {
                char mcb = mostCommonBit(filteredList, i);
                filteredList = filteredList.Where(x => x[i] == mcb).ToList();
                i++;
                Console.WriteLine("Most common bit: " + mcb + " new list count: " + filteredList.Count());
            }
            string oxygenRating = filteredList.First();

            filteredList = new List<string>(puzzleInput);
            Console.WriteLine(filteredList.Count());
            i = 0;
            while(filteredList.Count() > 1) {
                char mcb = mostCommonBit(filteredList, i);
                filteredList = filteredList.Where(x => x[i] != mcb).ToList();
                i++;
                Console.WriteLine("Most common bit: " + mcb + " new list count: " + filteredList.Count());
            }
            string co2Rating = filteredList.First();

            Console.WriteLine("Oxygen rating: " + oxygenRating + " CO2 Rating: " + co2Rating);
            Console.WriteLine("Life support: " + (binaryStringToInt(oxygenRating) * binaryStringToInt(co2Rating)));
        }

        protected char mostCommonBit(IEnumerable<string> input, int bitPosition) 
        {
            int count = 0;
            foreach (string line in input) {
                if (line[bitPosition] == '1') count++;
                else count--;
            }

            Console.Write("Count: " + count + " ");

            return count >= 0 ? '1' : '0';
        }

        protected int binaryStringToInt(string value) {
            return Convert.ToInt32(value, 2);
        }
    }
}