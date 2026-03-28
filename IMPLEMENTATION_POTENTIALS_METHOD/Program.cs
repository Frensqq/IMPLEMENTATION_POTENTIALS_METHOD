using IMPLEMENTATION_POTENTIALS_METHOD;
using static IMPLEMENTATION_POTENTIALS_METHOD.constructionBFS;

//constructionBFS - basic feasible solution - построение опорных планов

Inputs inputs = new Inputs();
PotentialsMethod potentialsMethod = new PotentialsMethod();

double[] ArrayA = inputs.Input();
double[] ArrayB = inputs.Input();
double[][] Matrix = inputs.Input(ArrayA, ArrayB);

Ouptput ouptput = new Ouptput();
Console.WriteLine("\nИсходная таблица\n");
ouptput.Output(ArrayA, ArrayB, Matrix);

MedotMinEl medotMinEl = new MedotMinEl();

double[][] plan = medotMinEl.OutputPlan(ArrayA, ArrayB, Matrix);
ouptput.Output(ArrayA, ArrayB, plan);
Console.WriteLine($"\nЦелевая функция равна = {ouptput.outputFun(plan, Matrix)}\n");

bool optimal = false;
bool stop = true;
while (!optimal)
{
    double[][] PotentialPlan = potentialsMethod.PotentialsCalculate(Matrix, plan);
    optimal = potentialsMethod.OptimalityCalculate(PotentialPlan, Matrix, plan);
    if (optimal)
    {
        Console.WriteLine("План оптимальный!");
        break;
    }
    else if (stop)
    {
        Console.WriteLine("План не оптимальный!");
        Console.WriteLine("Выберите:\n1)ввести оптимальный план в ручную\n2)расчитать оптимальный план");
        Console.Write("Ввод: ");
        string check = Console.ReadLine();
        if (check == "1")
        {
            Console.WriteLine("Введите матрицу стоимости:");
            Matrix = inputs.Input(ArrayA, ArrayB);
            Console.WriteLine("Введите составленный план:");
            plan = inputs.Input(ArrayA, ArrayB);
        }
        else stop = false;
    }
    Console.WriteLine("");
}

ouptput.Output(ArrayA, ArrayB, plan);
Console.WriteLine($"\nЦелевая функция равна = {ouptput.outputFun(plan, Matrix)}");
