using System;
using System.Collections.Generic;

namespace Eight_Queens_Problem
{
    class Program
    {
        static void Main(string[] args)
        {

            //GetNeighborStates fonksiyonunun ilk çalıştırıldığında fazladan zaman harcadığını tespit ettik
            //Bunun sistemin hafıza allocate etmesiyle ilgili olduğunu tahmin ediyoruz.
            //Bu yüzden doğru zaman ölçümü yapabilmek için en başta bir kere çağırıyoruz
            // Bunu yapmazsak 25 döngünün ilk döngüsündeki HillClimb işlemi olması gerekenden çok daha uzun sürüyor
            // Ve ortalama istatistiklerini bozuyor.
            BoardState state = new BoardState();
            state.RandomFill();
            state.GetNeighborStates ();

            int ClimbCount = 25;
            double[][] infos = new double[ClimbCount+1][];
            double moveSum = 0;
            double resSum = 0;
            double timeSum = 0;
            for (int i =0; i < ClimbCount; i++)
            {
                BoardState startState = new BoardState();
                infos[i] = new double[3];
                Console.WriteLine("STARTING STATE\n----------------");
                startState.RandomFill();
                startState.PrintState();
                startState = HillClimb(startState, out infos[i][0], out infos[i][1], out infos[i][2]);
                Console.WriteLine("\nPROBLEM SOLVED\n----------------");
                startState.PrintState();
                moveSum += infos[i][0];
                resSum += infos[i][1];
                timeSum += infos[i][2];
                Console.WriteLine("\nRUN " + (i + 1) + " MoveCounts: " + infos[i][0] + " ResCounts: " + infos[i][1] + " RunTime: " + infos[i][2] + "µs");
                Console.WriteLine("###########################################################\n");
            }
            infos[ClimbCount] = new double[3];
            infos[ClimbCount][0]=moveSum/ClimbCount;
            infos[ClimbCount][1]=resSum/ClimbCount;
            infos[ClimbCount][2]=timeSum/ClimbCount;
            Console.WriteLine();
            Console.WriteLine("AVERAGES: Moves: " + infos[ClimbCount][0] + " Random Restarts: " + infos[ClimbCount][1] + " Run Times: " + infos[ClimbCount][2]);
            
        }
        static BoardState HillClimb(BoardState state, out double moves, out double randomRestarts, out double time)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Reset();
            watch.Start();
            BoardState[] successors = new BoardState[56];
            
            int moveCount = 0;
            int randomRestartCount = 0;

            int it = 0;
            while(state.fitness != 0)
            {
                
                successors = state.GetNeighborStates();
                BoardState bestSuccessor = selectBestSuccessor(successors);
                moveCount++;
                if (bestSuccessor.fitness < state.fitness)
                {
                    state = bestSuccessor;
                }
                else {
                    state.RandomFill();
                    randomRestartCount++;
                }

            }
            moves = moveCount;
            randomRestarts = randomRestartCount;
            watch.Stop();
            time = Convert.ToDouble(  watch.ElapsedTicks / (System.Diagnostics.Stopwatch.Frequency/(1000L*1000L)));
            return state;
        }

        static BoardState selectBestSuccessor(BoardState[] successors)
        {
            List<BoardState> bests = new List<BoardState>();
            int minval = successors[0].fitness;
            foreach(BoardState i in successors)
            {
                if (i.fitness > minval) break;
                bests.Add(i);
            }
            Random rand = new Random();
            return bests[rand.Next(0, bests.Count)];
        }
    }

    
}
