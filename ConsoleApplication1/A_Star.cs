using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class A_Star
    {
        int[,] currentElements;
        public PriorityQueue OpenStats = null;
        HashSet<string> ClosedStates;
        Dictionary<int, KeyValuePair<int, int>> goalIndex = new Dictionary<int, KeyValuePair<int, int>>();
        bool isTheGoal;
        bool isInClosed;
        int[,] goal = null;
        int index = 0;
        List<State> ALL;
        int size;
        int cost;
        bool ishamming;
        public A_Star(int[,] start, int[,] goal, int size,bool ishamming)
        {
            this.size = size;
            this.goal = goal;
            this.ishamming = ishamming;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    goalIndex.Add(this.goal[i, j], new KeyValuePair<int, int>(i, j));
                }
            }
            currentElements = start;
            OpenStats = new PriorityQueue(start.Length * 10);
            ClosedStates = new HashSet<string>();
            ALL = new List<State>();
            StartNode(currentElements);
        }
        public void StartNode(int[,] arr)
        {

            int x = 0;
            int y = 0;
            x = Environment.TickCount;
            isTheGoal = false;
            State start = new State(null, arr, 0, ref isTheGoal, size);

            if (!start.IsSolvable(size, arr))
            {
                Console.WriteLine("Not Solvable!!!");
                return;
            }
            Console.WriteLine("Solvable");

            if (ishamming)
            {
                start.cost = HammingCost(start.integers);
            }
            else
            {
                start.cost = ManhhatenCost(goalIndex, start.integers);
            }
            ALL.Add(start);
            OpenStats.insert(new KeyValuePair<int, int>(index, start.cost));
            index++;
            if (isTheGoal)
            {
                Console.WriteLine("Number of Movements = " + start.CostInDepth);
                return;
            }
            Go();
            y = Environment.TickCount;
            Console.WriteLine("Time Taken In MS : " + (y - x));
        }
        public void Go()
        {

            KeyValuePair<int, int> INC;
            int emptyRowIndex = -1;
            int emptyColIndex = -1;
            int Hcost;

            while (OpenStats.NumOfNodes != 0)
            {

                INC = OpenStats.extractMin();
                Hcost = 0;
                State current = ALL[INC.Key];
                findIndex(current.integers, ref emptyRowIndex, ref emptyColIndex);
                findMov(ref current.mov, emptyRowIndex, emptyColIndex, size, size);

                if (current.mov.down)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex + 1, emptyColIndex, current.CostInDepth, ref isInClosed, ref ClosedStates, size);
                    if (ishamming)
                    {
                        Hcost = HammingCost(s.integers);
                    }
                    else
                    {
                        Hcost = ManhhatenCost(goalIndex, s.integers);
                    }
                    if (Hcost == 0)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        PrintSolution(s);

                        //Console.WriteLine(s1);
                        //Console.WriteLine(s2);
                        //Console.WriteLine(s3);

                        return;
                    }
                    else
                    {
                        s.cost = s.CostInDepth + Hcost;
                    }

                    if (!isInClosed)
                    {

                        ALL.Add(s);
                        OpenStats.insert(new KeyValuePair<int, int>(index, s.cost));
                        index++;

                    }
                }
                if (current.mov.up)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex - 1, emptyColIndex, current.CostInDepth, ref isInClosed, ref ClosedStates, size);

                    if (ishamming)
                    {
                        Hcost = HammingCost(s.integers);
                    }
                    else
                    {
                        Hcost = ManhhatenCost(goalIndex, s.integers);
                    }
                    if (Hcost == 0)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        PrintSolution(s);
                        return;
                    }
                    else
                    {
                        s.cost = s.CostInDepth + Hcost;
                    }

                    if (!isInClosed)
                    {
                        ALL.Add(s);
                        OpenStats.insert(new KeyValuePair<int, int>(index, s.cost));
                        index++;
                    }
                }
                if (current.mov.right)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex, emptyColIndex + 1, current.CostInDepth, ref isInClosed, ref ClosedStates, size);
                    if (ishamming)
                    {
                        Hcost = HammingCost(s.integers);
                    }
                    else
                    {
                        Hcost = ManhhatenCost(goalIndex, s.integers);
                    }
                    if (Hcost == 0)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        PrintSolution(s);
                        return;
                    }
                    else
                    {
                        s.cost = s.CostInDepth + Hcost;
                    }

                    if (!isInClosed)
                    {
                        ALL.Add(s);
                        OpenStats.insert(new KeyValuePair<int, int>(index, s.cost));
                        index++;
                    }
                }
                if (current.mov.left)
                {
                    isInClosed = false;
                    isTheGoal = false;
                    State s = new State(current, current.integers, emptyRowIndex, emptyColIndex, emptyRowIndex, emptyColIndex - 1, current.CostInDepth, ref isInClosed, ref ClosedStates, size);
                    if (ishamming)
                    {
                        Hcost = HammingCost(s.integers);
                    }
                    else
                    {
                        Hcost = ManhhatenCost(goalIndex, s.integers);
                    }
                    if (Hcost == 0)
                    {
                        Console.WriteLine("Number of Movements = " + s.CostInDepth);
                        PrintSolution(s);
                        return;
                    }
                    else
                    {
                        s.cost = s.CostInDepth + Hcost;
                    }

                    if (!isInClosed)
                    {
                        ALL.Add(s);
                        OpenStats.insert(new KeyValuePair<int, int>(index, s.cost));
                        index++;
                    }
                }
                ClosedStates.Add(current.unique);
            }
        }
        public void findIndex(int[,] arr, ref int row, ref int col)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (arr[i, j] == 0)
                    {
                        row = i;
                        col = j;
                    }
        }
        public void findMov(ref Movements mov, int RowIndex, int ColIndex, int rowSize, int Colsize)
        {
            if (RowIndex - 1 >= 0)
            {
                mov.up = true;
            }
            if (RowIndex + 1 < rowSize)
            {
                mov.down = true;
            }
            if (ColIndex - 1 >= 0)
            {
                mov.left = true;
            }
            if (ColIndex + 1 < Colsize)
            {
                mov.right = true;
            }
        }
        public int HammingCost(int[,] integers)
        {
            int cost = 0;
            int number = 1;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (number == size * size)
                        number = 0;
                    if (integers[i, j] != number)
                        cost++;
                    number++;
                }
            return cost;
        }
        public int ManhhatenCost(Dictionary<int, KeyValuePair<int, int>> goalindex, int[,] integers)
        {
            int Hcost = 0;
            KeyValuePair<int, int> GI;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    GI = goalindex[integers[i, j]];

                    if (i != GI.Key || j != GI.Value)
                    {
                        Hcost += Math.Abs(i - GI.Key) + Math.Abs(j - GI.Value);
                    }
                }
            }
            return Hcost;
        }
        void PrintSolution(State S)
        {
            int step = 1;
            Stack<State> ss=new Stack<State>();
            while (S.parent !=null)
            {
                ss.Push(S);
                S = S.parent;
            }
            ss.Push(S);
            State solution = ss.Pop();
            while (ss.Count>0)
            {
                for(int i = 0; i < size; i++)
                {
                    for(int j = 0; j < size; j++)
                    {
                        Console.Write(solution.integers[i, j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine("\n");
                }
                Console.WriteLine(solution.cost-solution.CostInDepth);
                Console.WriteLine("\n");
                solution = ss.Pop();
                Console.WriteLine("Step Number : " + step.ToString());
                Console.WriteLine("\n");
                step++;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(solution.integers[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine("\n");
            }
            return;
        }
    }
}

