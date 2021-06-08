namespace Events
{
    public static class TurnEvents
    {
        public static readonly int CanReceiveInput = ++ProjectEvents.EventCounter;
        public static readonly int SetInputAction = ++ProjectEvents.EventCounter;
        public static readonly int SetText = ++ProjectEvents.EventCounter;
        public static readonly int Show = ++ProjectEvents.EventCounter;
    }
}