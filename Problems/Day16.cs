using System.Collections.Generic;
using System;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day16: Problem
    {
        string hexInput;
        string binaryInput;

        protected Dictionary<int, Func<List<ulong>, ulong>> operationDictionary = new Dictionary<int, Func<List<ulong>, ulong>>()
        {
            {0, inputs => inputs.Aggregate((x, y) => x + y)},
            {1, inputs => inputs.Aggregate((x, y) => x * y)},
            {2, inputs => inputs.Min()},
            {3, inputs => inputs.Max()},
            {5, inputs => (ulong)(inputs[0] > inputs[1] ? 1 : 0)},
            {6, inputs => (ulong)(inputs[0] < inputs[1] ? 1 : 0)},
            {7, inputs => (ulong)(inputs[0] == inputs[1] ? 1 : 0)}
        };
        protected const int LITERAL_PACKET = 4;
        protected const char LENGTH_TYPE_TOTAL_LENGTH = '0';
        protected const char LENGTH_TYPE_NUMBER_PACKETS = '1';


        public Day16(string inputPath): base(inputPath)
        {
            hexInput = puzzleInput[0];
            binaryInput = String.Join(
                String.Empty, 
                hexInput.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
            );
        }

        public override void Part1()
        {
            int versionSum = 0;
            ParsePacket(binaryInput, out versionSum);
            Console.WriteLine("Version sum: " + versionSum);
        }

        public override void Part2()
        {
            Console.WriteLine("Evaluation: " + ParsePacket(binaryInput, out _));
        }

        protected ulong ParsePacket(string input, out int versionSum)
        {
            int startIndex = 0;
            versionSum = 0;
            return ParsePacket(input, out versionSum, ref startIndex);
        }

        // Retuns the evaluation of the input string, also returns version sum as an output variable
        protected ulong ParsePacket(string input, out int versionSum, ref int currIndex, int packetCount = 1, bool isCount = true, Func<List<ulong>, ulong> operation = null) {

            versionSum = 0;
            int endOfPacket = currIndex + packetCount;
            List<ulong> evaluations = new List<ulong>();

            for(int counter = 0; isCount ? counter < packetCount : currIndex < endOfPacket; counter ++)
            {
                int packetVersion = Convert.ToInt32(input.Substring(currIndex, 3), 2);
                int packetTypeID = Convert.ToInt32(input.Substring(currIndex + 3, 3), 2);
                currIndex += 6; // Every packet consumes at least 6 characters
                if (packetTypeID != LITERAL_PACKET) { // Operator Packet
                    char lengthTypeID = input[currIndex];
                    int subPacketVersionSum = 0;
                    currIndex++;
                    if (lengthTypeID == LENGTH_TYPE_TOTAL_LENGTH) { // Total Length Specified
                        int totalLength = Convert.ToInt32(input.Substring(currIndex, 15), 2);
                        currIndex += 15;             
                        evaluations.Add(
                            ParsePacket(
                                input, 
                                out subPacketVersionSum, 
                                ref currIndex, 
                                totalLength, 
                                false, 
                                operationDictionary[packetTypeID]
                            )
                        );
                    } else { // Packet count specified
                        int subPacketCount = Convert.ToInt32(input.Substring(currIndex, 11), 2);
                        currIndex += 11; 
                        evaluations.Add(
                            ParsePacket(
                                input, 
                                out subPacketVersionSum, 
                                ref currIndex, 
                                subPacketCount, 
                                true, 
                                operationDictionary[packetTypeID]
                            )
                        );
                    }
                    versionSum += subPacketVersionSum + packetVersion;
                } else { // Literal Packet
                    versionSum += packetVersion;
                    int pos = currIndex;
                    string value = "";
                    while(true)
                    {
                        value += input.Substring(pos + 1, 4);
                        currIndex += 5;
                        if(input[pos] == '0') {
                            break;
                        } else {
                            pos += 5;
                        }
                    }
                    evaluations.Add(Convert.ToUInt64(value, 2));
                }
            }

            if (operation != null) {
                return operation.Invoke(evaluations);
            }
            return evaluations[0];
        }
    }
}