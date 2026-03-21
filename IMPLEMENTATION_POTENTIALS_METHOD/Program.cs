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

double[][] plan = medotMinEl.OutputPlan(ArrayA, ArrayB, Matrix);
ouptput.Output(ArrayA, ArrayB, plan);
Console.WriteLine($"\nЦелевая функция равна = {ouptput.outputFun(plan, Matrix)}");