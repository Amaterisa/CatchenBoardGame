namespace Events
{
    public static class PlayerManagerEvents
    {
        public static readonly int AddPlayer = ++ProjectEvents.EventCounter;
        public static readonly int RemovePlayer = ++ProjectEvents.EventCounter;
        public static readonly int Move = ++ProjectEvents.EventCounter;
        public static readonly int GoToNextPlayer = ++ProjectEvents.EventCounter;
    }
}