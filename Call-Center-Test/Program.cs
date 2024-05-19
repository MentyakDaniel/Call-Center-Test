using Call_Center_Test;
using Call_Center_Test.Dto;

if (args.Length < 2 && args[0] is not "-f")
{
    Console.WriteLine("Please enter a file path argument.");
    Console.WriteLine("Usage: -f \"filepath\"");
    return;
}

string fileName = args[1];

CsvReader csvReader = new(fileName);

Console.WriteLine("Максимальное количество одновременных сессий по дням:\n");

Console.WriteLine(string.Format("\t|{0,10}|{1,5}|", "Дата", "Макс"));

foreach (MaxSessionsPerDay day in csvReader.MaxSessionsPerDays())
    Console.WriteLine(day);

Console.WriteLine("Время проведенное операторами в разных статусах:\n");

Console.WriteLine(string.Format("\t|{0,35}|{1}", "Оператор", "Тип и длительность"));

foreach (OperatorStatusDuration operators in csvReader.GetOperatorStatusDurations())
    Console.WriteLine(operators);