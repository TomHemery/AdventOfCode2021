using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2021 
{
    public class BingoBoard 
    {
        protected string[,] board;

        public BingoBoard (List<string> numbers) {
            Console.WriteLine("Making new board with size: " + numbers.Count);
            board = new string[numbers.Count, numbers.Count];
            for (int i = 0; i < numbers.Count; i++) {
                string[] row = Regex.Split(numbers[i], @"\s+").Where(s => s != string.Empty).ToArray();
                for(int j = 0; j < row.Length; j++) {
                    Console.Write(row[j] + ", ");
                    board[i, j] = row[j]; 
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void MarkOffNumber(string number) {
            for (int i = 0; i < board.GetLength(0); i++) {
                for (int j = 0; j < board.GetLength(1); j++) {
                    if (board[i, j] == number) {
                        board[i, j] = "*";
                    }
                }
            }
        }

        public bool HasWon() {
            for(int x = 0; x < board.GetLength(0); x++) {
                bool won = true;
                for(int y = 0; y < board.GetLength(1); y++) {
                    if(board[x, y] != "*") {
                        won = false;
                        break;
                    }
                }
                if (won) return true;
            }
            for(int y = 0; y < board.GetLength(1); y++) {
                bool won = true;
                for(int x = 0; x < board.GetLength(1); x++) {
                    if(board[x, y] != "*") {
                        won = false;
                        break;
                    }
                }
                if (won) return true;
            }
            return false;
        }

        public int GetScore() {
            int score = 0;
            foreach(string value in board) {
                if (value != "*") score += int.Parse(value);
            }
            return score;
        }
    }
}