namespace Events
{
    public static class MainBoardPieceEvents
    {
        public static readonly int Show = ++ProjectEvents.EventCounter;
        public static readonly int Hide = ++ProjectEvents.EventCounter;
        public static readonly int Setup = ++ProjectEvents.EventCounter;
    }
}
