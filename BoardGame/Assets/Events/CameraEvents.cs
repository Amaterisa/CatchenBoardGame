namespace Events
{
    public static class CameraEvents
    {
        public static readonly int SetReferenceTransform = ++ProjectEvents.EventCounter;
        public static readonly int EnableFollowTransform = ++ProjectEvents.EventCounter;
        public static readonly int SetLerpFactor = ++ProjectEvents.EventCounter;
    }
}