using System.Collections.Generic;
using System;
using System.Linq;
namespace AdventOfCode2021
{
    class Day10: Problem
    {
        protected char[] openingBrackets = new char[] {'(', '[', '{', '<'};

        protected Dictionary<char, char> bracketDict = new Dictionary<char, char>()
            {
                {'(', ')'},
                {'[', ']'},
                {'{', '}'},
                {'<', '>'}
            };

        public Day10(string inputPath): base(inputPath)
        {

        }

        public override void Part1()
        {
            Dictionary<char, int> scoreTable = new Dictionary<char, int>()
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };
            long score = 0;
            foreach(string line in puzzleInput)
            {
                char corrupted;
                if(IsCorrupted(line, out corrupted))
                {
                    score += scoreTable[corrupted];
                }
            }

            Console.WriteLine("Total score: " + score);
        }

        protected bool IsCorrupted(string line, out char mismatch)
        {
            Stack<char> stack = new Stack<char>();
            foreach(char c in line)
            {
                if(openingBrackets.Contains(c)) stack.Push(c);
                else {
                    if(
                        c == ')' && stack.Peek() == '(' ||
                        c == ']' && stack.Peek() == '[' ||
                        c == '}' && stack.Peek() == '{' ||
                        c == '>' && stack.Peek() == '<'
                    ) {
                        stack.Pop();
                    } else { // Corrupted
                        mismatch = c;
                        return true;
                    }
                }
            }
            mismatch = ' ';
            return false;
        }

        public override void Part2()
        {
            string[] incomplete = puzzleInput.Where(x => !IsCorrupted(x, out _)).ToArray();

            Dictionary<char, int> scoreTable = new Dictionary<char, int>()
            {
                {')', 1},
                {']', 2},
                {'}', 3},
                {'>', 4}
            };

            List<long> scores = new List<long>();
            foreach(string line in incomplete)
            {
                long score = 0;
                Stack<char> stack = new Stack<char>();
                foreach(char c in line)
                {
                    if(openingBrackets.Contains(c)) stack.Push(c);
                    else stack.Pop();
                }

                while(stack.Count > 0)
                {
                    score = score * 5 + scoreTable[bracketDict[stack.Pop()]];
                }

                scores.Add(score);
            }
            
            scores.Sort();

            Console.WriteLine("Middle score: " + scores[scores.Count / 2]);
        }
    }
}