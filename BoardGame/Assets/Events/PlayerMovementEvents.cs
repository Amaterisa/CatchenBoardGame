namespace Events
{
    public static class PlayerMovementEvents
    {
        public static readonly int Move = ++ProjectEvents.EventCounter;
        public static readonly int GoToPosition = ++ProjectEvents.EventCounter;
    }
}