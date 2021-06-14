namespace Events
{
    public static class PlayerMovementEvents
    {
        public static readonly int StartMove = ++ProjectEvents.EventCounter;
        public static readonly int GoToPosition = ++ProjectEvents.EventCounter;
    }
}