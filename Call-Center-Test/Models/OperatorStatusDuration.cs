using System.Text;

namespace Call_Center_Test.Dto
{
    internal record OperatorStatusDuration(string Name, IEnumerable<StatusDuration> StatusTimes)
    {
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            foreach (StatusDuration item in StatusTimes)
                stringBuilder.Append(item);

            return string.Format("\t|{0,-35}|{1,5}", Name, stringBuilder.ToString());
        }
    }
}
