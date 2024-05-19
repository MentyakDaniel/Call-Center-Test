using Call_Center_Test.Dto;

namespace Call_Center_Test.Models
{
    internal record CallCenterData(DateTime StartSession, DateTime EndSession,
        string Project, string Operator, OperatorStates State, int Duration) : IComparable
    {
        public int CompareTo(object? obj)
        {
            if (obj is not null and CallCenterData)
            {
                CallCenterData? item = obj as CallCenterData;

                if (StartSession > item?.StartSession)
                    return 1;
                else if (StartSession < item?.StartSession)
                    return -1;
                else return 0;
            }
            return -1;
        }

        public override string ToString() 
            => string.Format("|{0,-20}|{1,-20}|{2,-30}|{3,-40}|{4,-10}|{5,-5}|", 
                StartSession, EndSession, Project, Operator, Extensions.GetEnumDescription(State), Duration);
    }
}