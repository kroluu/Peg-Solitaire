using System;

namespace Board
{
    public static class RaycastInfo
    {
        public static IMouseDetect detectedGameElement;

        public static event Action OnPotDetectAction;

        public static void OnPotDetect()
        {
            OnPotDetectAction?.Invoke();
        }

        public static void OnPotReset()
        {
            OnPotDetectAction = null;
        }
    }
}

