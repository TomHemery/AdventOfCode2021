using System;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day8: Problem 
    {
        public Day8(string inputPath): base(inputPath)
        {

        }

        public override void Part1()
        {
            int[] uniqueCases = {7, 3, 4, 2};
            int total = 0;
            foreach(string line in puzzleInput) {
                string outputValues = line.Split("|")[1].Trim();
                string [] outputs = outputValues.Split(" ");

                foreach(string output in outputs) {
                    if(uniqueCases.Contains(output.Length)) {
                        total++;
                    }
                }
            }

            Console.WriteLine("Total part 1: " + total);
        }

        public override void Part2()
        {
            long total = 0;
            foreach(string line in puzzleInput) {
                string [] outputs = line.Split("|")[1].Trim().Split(" ");
                string [] signalPatterns = line.Split("1")[0].Trim().Split(" ");
                string[] reps = new string[10];

                string [] fiveCharPatterns = signalPatterns.Where(x => x.Length == 5).ToArray();
                string [] sixCharPatterns = signalPatterns.Where(x => x.Length == 6).ToArray();
                reps[7] = signalPatterns.Where(x => x.Length == 3).First();
                reps[1] = signalPatterns.Where(x => x.Length == 2).First();
                reps[8] = signalPatterns.Where(x => x.Length == 7).First();
                reps[4] = signalPatterns.Where(x => x.Length == 4).First();
                reps[3] = fiveCharPatterns.Where(x => x.Contains(reps[1][0]) && x.Contains(reps[1][1])).First();
                reps[9] = sixCharPatterns.Where(x => {
                    foreach(char c in reps[3]) {
                        if(!x.Contains(c)) return false;
                    }
                    return true;
                }).First();

                char topLeft = reps[9].Except(reps[3]).First(); // TOP LEFT
                reps[5] = fiveCharPatterns.Where(x => x.Contains(topLeft)).First();
                reps[2] = fiveCharPatterns.Where(x => x != reps[5] && x != reps[3]).First();

                char middle = reps[4].Except(reps[1]).Except("" + topLeft).First(); // MIDDLE
                reps[0] = sixCharPatterns.Where(x => !x.Contains(middle)).First();
                reps[6] = sixCharPatterns.Where(x => x != reps[0] && x != reps[9]).First();
        
                for(int i = 0; i < reps.Length; i++) {
                    reps[i] = String.Concat(reps[i].OrderBy(c => c));
                }

                string val = "";
                foreach(string output in outputs)
                {
                    String ordered = String.Concat(output.OrderBy(c => c));
                    for(int i = 0; i < reps.Length; i++) {
                        if(reps[i] == ordered) val += i;
                    }
                }

                total += int.Parse(val);

            }

            Console.WriteLine("Total part 2: " + total);
        }
    }
}