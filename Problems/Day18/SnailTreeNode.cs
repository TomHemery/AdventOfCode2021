using System;
using System.Collections.Generic;
namespace AdventOfCode2021
{
    public class SnailTreeNode
    {
        public int value = -1;
        
        public SnailTreeNode left = null;
        public SnailTreeNode right = null;
        public SnailTreeNode parent = null;

        public SnailTreeNode() {}

        public SnailTreeNode(SnailTreeNode parent)
        {
            this.parent = parent;
        }

        public SnailTreeNode(SnailTreeNode parent, int value)
        {
            this.parent = parent;
            this.value = value;
        }

        public SnailTreeNode(SnailTreeNode left, SnailTreeNode right)
        {
            this.left = left;
            this.right = right;
        }

        public SnailTreeNode(string line)
        {
            SnailTreeNode curr = this;

            foreach(char c in line) 
            {
                if (c == '[') { // Descend
                    SnailTreeNode child = new SnailTreeNode(curr);
                    curr.AddChildLeftFirst(child);
                    curr = child;
                } else if (c == ']') { // Ascend
                    curr = curr.parent;
                } else if (c == ',') { // Right child
                    SnailTreeNode child = new SnailTreeNode(curr.parent);
                    curr.parent.AddChildLeftFirst(child);
                    curr = child;
                } else { // Value
                    curr.value = (int)char.GetNumericValue(c);
                }
            }
        }

        public void AddChildLeftFirst(SnailTreeNode child)
        {
            if (left == null) left = child;
            else if(right == null) right = child;
            else throw new System.Exception("Tree node assigned more than 2 children");
        }

        public bool IsLeaf() {
            return left == null && right == null;
        }

        public override string ToString() {
            if (IsLeaf()) {
                return "" + value;
            } else {
                return "[" + left.ToString() + "," + right.ToString() + "]";
            }
        }

        public int GetMagnitude() {
            if(IsLeaf()) return value;
            else return left.GetMagnitude() * 3 + right.GetMagnitude() * 2;
        }

        public static SnailTreeNode Add(SnailTreeNode a, SnailTreeNode b)
        {
            SnailTreeNode parent = new SnailTreeNode(a, b);
            a.parent = parent;
            b.parent = parent;
            Reduce(parent);
            return parent;
        }

        public static void Reduce(SnailTreeNode node)
        {
            while (true) {
                ExplodeAll(node);
                if (!SplitFirst(node)) break;
            }
        }

        protected static void ExplodeAll(SnailTreeNode start)
        {
            while(ExplodeFirst(start));
        }   

        protected static bool ExplodeFirst(SnailTreeNode curr, int depth = 0)
        {
            if (curr.IsLeaf()) return false;
            else if (depth == 4) {
                SnailTreeNode leftNeighbour = GetFirstLeft(curr.left);
                SnailTreeNode rightNeighbour = GetFirstRight(curr.right);

                if (leftNeighbour != null) leftNeighbour.value += curr.left.value;
                if (rightNeighbour != null) rightNeighbour.value += curr.right.value;

                curr.left = null;
                curr.right = null;
                curr.value = 0;
                return true;
            } else {
                if (ExplodeFirst(curr.left, depth + 1)) return true;
                if (ExplodeFirst(curr.right, depth + 1)) return true;
            }
            return false;
        }

        protected static bool SplitFirst(SnailTreeNode node)
        {
            if (node.IsLeaf()) {
                if(node.value >= 10) {
                    node.left = new SnailTreeNode(node, node.value / 2);
                    node.right = new SnailTreeNode(node, (node.value / 2) + node.value % 2);
                    node.value = -1;
                    return true;
                }
            } else {
                if (SplitFirst(node.left)) return true;
                else if (SplitFirst(node.right)) return true;
            }
            return false;
        }

        protected static SnailTreeNode GetFirstLeft(SnailTreeNode start)
        {
            SnailTreeNode curr = start;
            while (curr.parent.left == curr) {
                curr = curr.parent;
                if (curr.parent == null) return null;
            }
            curr = curr.parent.left;
            while (!curr.IsLeaf()) curr = curr.right;
            return curr;
        } 

        protected static SnailTreeNode GetFirstRight(SnailTreeNode start)
        {
            SnailTreeNode curr = start;
            while (curr.parent.right == curr) {
                curr = curr.parent;
                if (curr.parent == null) return null;
            }
            curr = curr.parent.right;
            while (!curr.IsLeaf()) curr = curr.left;
            return curr;
        } 
    }
}