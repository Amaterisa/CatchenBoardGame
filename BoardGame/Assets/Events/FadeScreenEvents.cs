namespace Events
{
    public static class FadeScreenEvents
    {
        public static readonly int Show = ++ProjectEvents.EventCounter;
        public static readonly int Hide = ++ProjectEvents.EventCounter;
    }
}