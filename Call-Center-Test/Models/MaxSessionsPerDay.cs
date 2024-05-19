namespace Call_Center_Test.Dto
{
    internal record MaxSessionsPerDay(DateOnly Day, int MaxSessions)
    {
        public override string ToString() 
            => string.Format("\t|{0,5}|{1,5}|", Day, MaxSessions);
    }
}
