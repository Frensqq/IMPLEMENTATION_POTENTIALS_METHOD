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
            double[][] plan = (double[][])planDef.Clone();
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


    }
}
