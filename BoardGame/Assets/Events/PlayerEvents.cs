namespace Events
{
    public static class PlayerEvents
    {
        public static readonly int SetName = ++ProjectEvents.EventCounter;
        public static readonly int SetColor = ++ProjectEvents.EventCounter;
    }
}