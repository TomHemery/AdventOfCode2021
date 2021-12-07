using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day4: Problem
    {
        protected List<BingoBoard> boards = new List<BingoBoard>();
        protected string[] numbers;

        public Day4(string inputPath): base(inputPath)
        {
            numbers = puzzleInput[0].Split(',');
            List<string> boardContents = new List<string>();

            foreach(string line in puzzleInput.Skip(2)) {
                if(line == "") {
                    boards.Add(new BingoBoard(boardContents));
                    boardContents.Clear();
                } else {
                    boardContents.Add(line);
                }
            }

            foreach(string number in numbers) {
                foreach(BingoBoard board in boards) {

                }

                for(int i = boards.Count - 1; i >= 0; i--) {
                    BingoBoard board = boards[i];
                    board.MarkOffNumber(number);
                    if (board.HasWon()) {
                        Console.WriteLine("Score: " + int.Parse(number) * board.GetScore());
                        boards.Remove(board);
                    }
                }
            }
        }

        public override void Part1()
        {
            
        }

        public override void Part2()
        {
            
        }
    }
}