namespace Events
{
    public static class BoardEvents
    {
        public static readonly int PositionPlayers = ++ProjectEvents.EventCounter;
        public static readonly int Show = ++ProjectEvents.EventCounter;
        public static readonly int GetBoardPiece = ++ProjectEvents.EventCounter;
    }
}