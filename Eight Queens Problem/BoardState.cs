using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eight_Queens_Problem
{
    class BoardState
    {
        public int[] Board;
        
        public int fitness;
        public BoardState()
        {
            Board = new int[8];
            fitness = int.MaxValue;
        }
        public void Fill(int[] indexes)
        {
            Board = indexes;
            CalculateFitness();
        }
        public void RandomFill()
        {
            Random rand = new Random();
            int[] indexes = new int[8];
            for(int i =0; i < 8; i++)
            {
                indexes[i] = rand.Next(0, 8);
            }
            Fill(indexes);
        }
        void CalculateFitness()
        {
            int fitness = 0;
            for(int i =0; i < 8; i++)
            {
                for(int j = i+1; j < 8; j++)
                {
                    if (Board[i] == Board[j]) fitness++;
                    if (Math.Abs(Board[i] - Board[j]) == Math.Abs(i - j)) fitness++;
                }
            }
            this.fitness = fitness;

        }

        public void PrintState()
        {
            String boardString = "";
                for(int i =0; i < 8; i++)
            {
                for(int j =0; j < 8; j++)
                {
                    if (Board[j] == i) boardString += 'X';
                    else boardString += "O";
                    boardString += " ";
                }
                boardString += "\n";
            }
            Console.WriteLine(boardString);
            Console.WriteLine("Fitness= " + fitness);
            
        }

        public BoardState[] GetNeighborStates()
        {
            BoardState[] nei = new BoardState[56];
            int addindex = 0;
            for(int i =0; i < 8; i++)
            {
                for(int j =0; j < 8; j++)
                {
                    if (j == Board[i]) continue;
                    BoardState tempNeighbor = new BoardState();
                    int[] indexes = new int[8];
                    Array.Copy(Board, indexes, 8);
                    indexes[i] = j;
                    tempNeighbor.Fill(indexes);
                    nei[addindex] = tempNeighbor;
                    addindex++;

                }
            }
            
            return nei.OrderBy(x => x.fitness).ToArray();
        }

    }
}
