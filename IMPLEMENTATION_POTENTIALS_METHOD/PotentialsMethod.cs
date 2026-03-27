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
                    Console.Write($"{planPotential[i][j],3} | ");
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

            for (int i = 0; i < planDef.Length - 1; i++)
            {
                for (int j = 0; j < planDef[i].Length - 1; j++)
                {
                    if (planDef[i][j] == 0)
                    {
                        double optimal = planDef[i][planDef[i].Length - 1] + planDef[planDef.Length - 1][j] - matrix[i][j];
                        if (optimal > 0)
                        {
                            Console.WriteLine($"\nДельта{i + 1}{j + 1} = {optimal} > 0\n");
                            if (max < optimal)
                            {
                                max = optimal;
                                maxI = i;
                                maxJ = j;
                            }

                        }
                        else
                        {
                            Console.WriteLine($"Дельта{i + 1}{j + 1} = {optimal} <= 0");
                        }
                    }
                }
            }
            if (max > double.MinValue)
            {
                Redistribution(planDef, matrix, plan, max, maxI, maxJ);
                return false;
            }
            return true;
        }

        public void Redistribution(double[][] planDef, double[][] matrix, double[][] plan, double optimal, int idef, int jdef)
        {
            List<(int i, int j)> cycle = FindRectangleCycle(plan, idef, jdef);

            if (cycle.Count == 0)
            {
                Console.WriteLine("Цикл не найден!");
                return;
            }

            double minLoad = double.MaxValue;
            for (int k = 1; k < cycle.Count - 1; k += 2)
            {
                int i = cycle[k].i;
                int j = cycle[k].j;
                if (plan[i][j] < minLoad)
                {
                    minLoad = plan[i][j];
                }
            }

            for (int k = 0; k < cycle.Count - 1; k++)
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

        private List<(int i, int j)> FindRectangleCycle(double[][] plan, int startI, int startJ)
        {
            int rows = plan.Length;
            int cols = plan[0].Length;

            for (int i = 0; i < rows; i++)
            {
                if (i == startI) continue;

                for (int j = 0; j < cols; j++)
                {
                    if (j == startJ) continue;

                    if (plan[startI][j] != 0 && plan[i][startJ] != 0 && plan[i][j] != 0)
                    {
                        return new List<(int i, int j)>
                        {
                            (startI, startJ),
                            (startI, j),
                            (i, j),
                            (i, startJ),
                            (startI, startJ)
                        };
                    }
                }
            }

            return new List<(int i, int j)>();
        }
    }
}