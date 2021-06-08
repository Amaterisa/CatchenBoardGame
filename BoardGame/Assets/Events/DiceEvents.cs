namespace Events
{
    public static class DiceEvents
    {
        public static readonly int Show = ++ProjectEvents.EventCounter;
        public static readonly int Hide = ++ProjectEvents.EventCounter;
    }
}