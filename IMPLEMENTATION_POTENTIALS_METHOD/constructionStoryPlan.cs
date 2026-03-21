using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMPLEMENTATION_POTENTIALS_METHOD
{
    internal class constructionStoryPlan
    {
        public class Inputs()
        {
            public double[] Input()
            {
                int a = 0;
                Console.Write("Введите кол-во пунктов производства: ");
                a = Convert.ToInt32(Console.ReadLine());

                double[] arrayA = new double[a];

                for (int i = 0; i < a; i++)
                {
                    Console.Write("Кол-во товара в пункте производства " + (i + 1) + " : ");
                    arrayA[i] = Convert.ToDouble(Console.ReadLine());
                }
                return arrayA;
            }
            public double[][] Input(double[] arrayA, double[] arrayB)
            {
                double[][] matrix = new double[arrayA.Length][];

                for (int i = 0; i < arrayA.Length; i++)
                {
                    matrix[i] = new double[arrayB.Length];
                    for (int j = 0; j < arrayB.Length; j++)
                    {
                        Console.Write("Введите стоимость [" + (i + 1) + ";" + (j + 1) + "] : ");
                        matrix[i][j] = Convert.ToDouble(Console.ReadLine());
                    }
                }
                return matrix;
            }
        }
        public class Ouptput()
        {
            public void Output(double[] arrayA, double[] arrayB, double[][] matrix)
            {
                for (int i = 0; i <= arrayA.Length; i++)
                {
                    for (int j = 0; j <= arrayB.Length; j++)
                    {
                        if ((j == 0) && (i == 0)) Console.Write($" | a/b | ");
                        else if (i == 0)
                        {
                            Console.Write($" | {arrayB[j - 1],3} | ");
                            if (j == arrayB.Length)
                            {
                                Console.Write("\n");
                            }
                        }
                        else if (j == 0)
                        {
                            Console.Write($" | {arrayA[i - 1],3} | ");
                        }
                        else if (j == arrayB.Length)
                        {
                            Console.Write($" | {matrix[i - 1][j - 1],3} | \n");
                        }
                        else
                        {
                            Console.Write($" | {matrix[i - 1][j - 1],3} | ");
                        }
                    }
                }
            }

            public double outputFun(double[][] plan, double[][] matrix)
            {
                double celFun = 0;
                for (int i = 0; i < plan.Length; i++)
                {
                    for (int j = 0; j < plan[i].Length; j++)
                    {
                        if (plan[i][j] != 0)
                        {
                            celFun = celFun + plan[i][j] * matrix[i][j];
                        }
                    }
                }
                return celFun;
            }
        }
        public class MedotMinEl()
        {
            private bool checkArray(double[] Array)
            {
                double summ = 0;
                for (int i = 0; i < Array.Length; i++)
                {
                    summ = summ + Array[i];
                }
                if (summ == 0)
                {
                    return false;
                }
                return true;


            }
            public double[][] OutputPlan(double[] ArrayA, double[] ArrayB, double[][] matrix)
            {
                Console.WriteLine("\nМетод минимального элемента\n");
                double[] arrayA = (double[])ArrayA.Clone();
                double[] arrayB = (double[])ArrayB.Clone();
                double minCost = matrix[0][0];
                int mini = 0;
                int minj = 0;

                double[][] plan = new double[arrayA.Length][];
                for (int i = 0; i < arrayA.Length; i++)
                {
                    plan[i] = new double[arrayB.Length];
                }

                while (checkArray(arrayB))
                {
                    minCost = double.MaxValue;

                    for (int i = 0; i < arrayA.Length; i++)
                    {
                        for (int j = 0; j < arrayB.Length; j++)
                        {
                            if ((minCost > matrix[i][j]) && (arrayA[i] != 0) && (arrayB[j] != 0))
                            {
                                minCost = matrix[i][j];
                                mini = i;
                                minj = j;
                            }
                        }
                        //Console.Write(minCost+" "+ mini+" "  + minj + "\n");
                    }

                    if (arrayA[mini] > arrayB[minj])
                    {
                        plan[mini][minj] = arrayB[minj];
                        arrayA[mini] = arrayA[mini] - arrayB[minj];
                        arrayB[minj] = 0;
                    }
                    else if (arrayA[mini] <= arrayB[minj])
                    {
                        plan[mini][minj] = arrayA[mini];
                        arrayB[minj] = arrayB[minj] - arrayA[mini];
                        arrayA[mini] = 0;
                    }
                }
                return plan;
            }
        }
    }
}
