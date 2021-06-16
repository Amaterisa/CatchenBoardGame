namespace Events
{
    public static class FadeScreenEvents
    {
        public static readonly int Show = ++ProjectEvents.EventCounter;
        public static readonly int Hide = ++ProjectEvents.EventCounter;
        public static readonly int HideInstantly = ++ProjectEvents.EventCounter;
        public static readonly int SetAlpha = ++ProjectEvents.EventCounter;
    }
}