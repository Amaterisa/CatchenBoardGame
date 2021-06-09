namespace Events
{
    public static class TurnEvents
    {
        public static readonly int CanReceiveInput = ++ProjectEvents.EventCounter;
        public static readonly int SetupInputAction = ++ProjectEvents.EventCounter;
        public static readonly int SetText = ++ProjectEvents.EventCounter;
        public static readonly int Show = ++ProjectEvents.EventCounter;
    }
}