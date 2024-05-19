using Call_Center_Test.Dto;
using Call_Center_Test.Models;

namespace Call_Center_Test
{
    internal sealed class CsvReader(string fileName)
    {
        private readonly List<CallCenterData> _data = ReadCsv(fileName);

        public List<CallCenterData> Data => _data;

        public IEnumerable<MaxSessionsPerDay> MaxSessionsPerDays()
        {
            Dictionary<DateOnly, List<CallCenterData>> dict = _data
                .GroupBy(item => DateOnly.FromDateTime(item.StartSession))
                .ToDictionary(key => key.Key, value => value.ToList());

            foreach (KeyValuePair<DateOnly, List<CallCenterData>> day in dict)
            {
                List<CallCenterData> info = day.Value;

                int maxValue = 0;
                int counter = 1;

                for (int i = 0; i < info.Count; i++)
                {
                    for (int j = i + 1; j < info.Count; j++)
                    {
                        if (IsIntersect(info[i].StartSession,
                            info[i].EndSession, info[j].StartSession, info[j].EndSession))
                            counter++;
                        else
                        {
                            if (maxValue < counter)
                                maxValue = counter;

                            counter = 1;
                            break;
                        }
                    }
                }

                yield return new(day.Key, maxValue);
            }
        }
        public IEnumerable<OperatorStatusDuration> GetOperatorStatusDurations()
        {
            Dictionary<string, List<CallCenterData>> dict = _data
                .GroupBy(item => item.Operator)
                .ToDictionary(key => key.Key, value => value.ToList());

            foreach (KeyValuePair<string, List<CallCenterData>> item in dict)
            {
                int pauseDuration = item.Value
                    .Where(data => data.State == OperatorStates.Pause)
                    .Sum(data => data.Duration);

                int readyDuration = item.Value
                    .Where(data => data.State == OperatorStates.Ready)
                    .Sum(data => data.Duration);

                int talkDuration = item.Value
                    .Where(data => data.State == OperatorStates.Talk)
                    .Sum(data => data.Duration);

                int reCallDuration = item.Value
                    .Where(data => data.State == OperatorStates.ReСall)
                    .Sum(data => data.Duration);

                int processingDuration = item.Value
                    .Where(data => data.State == OperatorStates.Processing)
                    .Sum(data => data.Duration);

                yield return new OperatorStatusDuration(item.Key, new List<StatusDuration>()
                {
                    new(pauseDuration, OperatorStates.Pause),
                    new(readyDuration, OperatorStates.Ready),
                    new(talkDuration, OperatorStates.Talk),
                    new(reCallDuration, OperatorStates.ReСall),
                    new(processingDuration, OperatorStates.Processing)
                });
            }
        }

        private static bool IsIntersect(DateTime startFirst, DateTime endFirst, DateTime startSecond, DateTime endSecond)
            => (endFirst > startSecond && endFirst <= endSecond) || (startSecond >= startFirst && startSecond < endFirst);
        private static List<CallCenterData> ReadCsv(string fileName)
        {
            try
            {
                Console.WriteLine($"Начинаем чтение файла: {fileName}");

                IEnumerable<string> fileData = File.ReadAllLines(fileName).Skip(1);

                List<CallCenterData> result = [];

                foreach (string line in fileData)
                {
                    string[] cell = line.Split(';');

                    DateTime start = DateTime.Parse(cell[0]);
                    DateOnly date = DateOnly.FromDateTime(start);

                    DateTime end = DateTime.Parse(cell[1]);
                    string project = cell[2];
                    string operatorName = cell[3];
                    OperatorStates state = Extensions.GetEnumValueFromDescription<OperatorStates>(cell[4]);
                    int duration = int.Parse(cell[5]);

                    result.Add(new(start, end, project, operatorName, state, duration));
                }

                result.Sort();

                Console.WriteLine($"Файл прочитан. Получено {result.Count} строк информации.");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
