namespace Call_Center_Test.Dto
{
    public record StatusDuration(int Duration, OperatorStates State)
    {
        public override string ToString() 
            => string.Format("{0,5} -> {1,10} y.e.|", Extensions.GetEnumDescription(State), Duration);
    }
}
