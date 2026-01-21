using UnityEngine;

namespace Game.Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager Instance { get; private set; }

        private bool _pause;

        public bool IsPause => _pause;

        public PauseManager()
        {
            Instance = this;
        }

        public void SetPause(bool pause)
        {
            _pause = pause;

            if (pause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
