namespace Events
{
    public static class GameConfigurationMenuEvents
    {
        public static readonly int SetButtonsInteractable = ++ProjectEvents.EventCounter;
        public static readonly int SetStartButtonInteractable = ++ProjectEvents.EventCounter;
    }
}