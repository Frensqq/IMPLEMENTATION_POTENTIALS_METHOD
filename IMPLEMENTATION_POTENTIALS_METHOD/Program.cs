using IMPLEMENTATION_POTENTIALS_METHOD;
using static IMPLEMENTATION_POTENTIALS_METHOD.constructionBFS;

//constructionBFS - basic feasible solution - построение опорных планов

Inputs inputs = new Inputs();

double[] ArrayA = inputs.Input();
double[] ArrayB = inputs.Input();
double[][] Matrix = inputs.Input(ArrayA, ArrayB);

Ouptput ouptput = new Ouptput();
Console.WriteLine("\nИсходная таблица\n");
ouptput.Output(ArrayA, ArrayB, Matrix);

MedotMinEl medotMinEl = new MedotMinEl();
PotentialsMethod potentialsMethod = new PotentialsMethod();

double[][] plan = medotMinEl.OutputPlan(ArrayA, ArrayB, Matrix);
ouptput.Output(ArrayA, ArrayB, plan);
Console.WriteLine($"\nЦелевая функция равна = {ouptput.outputFun(plan, Matrix)}");

 
bool optimal = false;
while (!optimal)
{
    double[][] PotentialPlan = potentialsMethod.PotentialsCalculate(Matrix, plan);
    optimal = potentialsMethod.OptimalityCalculate(PotentialPlan, Matrix, plan);
    if(optimal) break;
    Console.WriteLine("");
}

ouptput.Output(ArrayA, ArrayB, plan);
