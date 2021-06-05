namespace Events
{
    public static class PlayerCreationEvents
    {
        public static readonly int ConfirmPlayerCreation = ++ProjectEvents.EventCounter;
        public static readonly int Hide = ++ProjectEvents.EventCounter;
    }
}