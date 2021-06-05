namespace Events
{
    public static class TextFieldPopupEvents
    {
        public static readonly int Show = ++ProjectEvents.EventCounter;
        public static readonly int Hide = ++ProjectEvents.EventCounter;
    }
}