using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMPLEMENTATION_POTENTIALS_METHOD
{
    public class PotentialsMethod
    {
        public double[][] PotentialsCalculate(double[][] matrix, double[][] planDef)
        {
            double[][] plan = planDef.Select(row => row.ToArray()).ToArray();
            int horizontal = plan[0].Length;
            int vertical = plan.Length;


            double[][] planPotential = new double[vertical + 1][];
            for (int i = 0; i < plan.Length; i++)
            {
                planPotential[i] = new double[plan[i].Length + 1];
                Array.Copy(plan[i], planPotential[i], plan[i].Length);
            }
            planPotential[planPotential.Length - 1] = new double[plan[0].Length + 1];


            planPotential[0][horizontal] = 0;


            while (true)
            {
                double count = 0;
                for (int i = 0; i < plan.Length; i++)
                {
                    for (int j = 0; j < plan[i].Length; j++)
                    {
                        if (plan[i][j] != 0 && i == 0)
                        {
                            planPotential[vertical][j] = matrix[i][j] - planPotential[vertical][j];
                            count += plan[i][j];
                            plan[i][j] = 0;
                        }

                        if (plan[i][j] != 0 && i != 0)
                        {
                            if (planPotential[vertical][j] == 0 && planPotential[i][horizontal] == 0)
                            {

                            }
                            else if (planPotential[vertical][j] != 0)
                            {
                                planPotential[i][horizontal] = matrix[i][j] - planPotential[vertical][j];
                                count += plan[i][j];
                                plan[i][j] = 0;

                            }
                            else if (planPotential[i][horizontal] != 0)
                            {
                                planPotential[vertical][j] = matrix[i][j] - planPotential[i][horizontal];
                                count += plan[i][j];
                                plan[i][j] = 0;
                            }
                        }
                    }
                }
                if (count == 0) break;
            }

            for (int i = 0; i < planPotential.Length; i++)
            {
                for (int j = 0; j < planPotential[i].Length; j++)
                {
                    Console.Write(planPotential[i][j] + " | ");
                }
                Console.WriteLine("");
            }

            return planPotential;
        }

        public bool OptimalityCalculate(double[][] planDef, double[][] matrix, double[][] plan)
        {
            double max = double.MinValue;
            int maxI = int.MinValue;
            int maxJ = int.MinValue;

            for (int i = 0; i < planDef.Length-1; i++)
            {
                for (int j = 0; j < planDef[i].Length-1; j++)
                {
                    if (planDef[i][j] == 0)
                    {
                       double optimal = planDef[i][planDef[i].Length - 1] + planDef[planDef.Length - 1][j] - matrix[i][j];
                       if (optimal > 0)
                       {
                            Console.WriteLine($"Δ{i+1}{j+1} > 0");
                            if(max < optimal)
                            {
                                max = optimal;
                                maxI = i;
                                maxJ = j;
                            }
                            
                       }
                    }
                }
            }
            if (max > double.MinValue)
            {
                Redistribution(planDef, matrix, plan, max, maxI, maxJ);
                return false;
            }
            Console.WriteLine($"Оптимально");
            return true;
        }

        public void Redistribution(double[][] planDef, double[][] matrix, double[][] plan, double optimal, int idef, int jdef)
        {


            int vertical = plan.Length;
            int horizontal = plan[0].Length;

            List<(int i, int j)> cycle = FindCycle(plan, idef, jdef);

            if (cycle.Count == 0)
            {
                Console.WriteLine("Цикл не найден!");
                return;
            }

        
            double minLoad = double.MaxValue;
            for (int k = 1; k < cycle.Count; k += 2) 
            {
                int i = cycle[k].i;
                int j = cycle[k].j;
                if (plan[i][j] < minLoad)
                {
                    minLoad = plan[i][j];
                }
            }

            for (int k = 0; k < cycle.Count; k++)
            {
                int i = cycle[k].i;
                int j = cycle[k].j;

                if (k % 2 == 0) 
                {
                    plan[i][j] += minLoad;
                }
                else 
                {
                    plan[i][j] -= minLoad;
                }
            }

            Console.WriteLine($"\nПерераспределение: загружаем ячейку ({idef + 1},{jdef + 1}) с оценкой Δ = {optimal}");
            Console.WriteLine($"Минимальный груз для перераспределения: {minLoad}");
        }

        private List<(int i, int j)> FindCycle(double[][] plan, int startI, int startJ)
        {
            int vertical = plan.Length;
            int horizontal = plan[0].Length;

            List<(int i, int j)> cycle = new List<(int i, int j)>();
            cycle.Add((startI, startJ));

            if (FindNextPoint(plan, cycle, startI, startJ, true, startI, startJ))
            {
                return cycle; 
            }

            return new List<(int i, int j)>();
        }

        private bool FindNextPoint(double[][] plan, List<(int i, int j)> cycle, int currentI, int currentJ, bool isHorizontal, int startI, int startJ)
        {
            int vertical = plan.Length;
            int horizontal = plan[0].Length;

            if (isHorizontal)
            {
                // Ищем по горизонтали (строка currentI, ищем столбец)
                for (int j = 0; j < horizontal; j++)
                {
                    if (j == currentJ) continue;

                    // Проверяем, что клетка занята (есть груз) или это стартовая клетка
                    bool isOccupied = (plan[currentI][j] != 0);
                    bool isStart = (currentI == startI && j == startJ && cycle.Count >= 2);

                    if (isOccupied || isStart)
                    {
                        // Проверяем, не вернулись ли мы в начало
                        if (isStart)
                        {
                            // НЕ ДОБАВЛЯЕМ стартовую ячейку повторно
                            return true;
                        }

                        // Проверяем, не посещали ли уже эту клетку
                        bool alreadyVisited = false;
                        foreach (var point in cycle)
                        {
                            if (point.i == currentI && point.j == j)
                            {
                                alreadyVisited = true;
                                break;
                            }
                        }

                        if (!alreadyVisited)
                        {
                            cycle.Add((currentI, j));
                            if (FindNextPoint(plan, cycle, currentI, j, false, startI, startJ))
                            {
                                return true;
                            }
                            cycle.RemoveAt(cycle.Count - 1);
                        }
                    }
                }
            }
            else
            {
                // Ищем по вертикали (столбец currentJ, ищем строку)
                for (int i = 0; i < vertical; i++)
                {
                    if (i == currentI) continue;

                    // Проверяем, что клетка занята (есть груз) или это стартовая клетка
                    bool isOccupied = (plan[i][currentJ] != 0);
                    bool isStart = (i == startI && currentJ == startJ && cycle.Count >= 2);

                    if (isOccupied || isStart)
                    {
                        // Проверяем, не вернулись ли мы в начало
                        if (isStart)
                        {
                            // НЕ ДОБАВЛЯЕМ стартовую ячейку повторно
                            return true;
                        }

                        // Проверяем, не посещали ли уже эту клетку
                        bool alreadyVisited = false;
                        foreach (var point in cycle)
                        {
                            if (point.i == i && point.j == currentJ)
                            {
                                alreadyVisited = true;
                                break;
                            }
                        }

                        if (!alreadyVisited)
                        {
                            cycle.Add((i, currentJ));
                            if (FindNextPoint(plan, cycle, i, currentJ, true, startI, startJ))
                            {
                                return true;
                            }
                            cycle.RemoveAt(cycle.Count - 1);
                        }
                    }
                }
            }

            return false;
        }
    }
}
