using UI.Other;
using UnityEngine;

namespace Game.Managers
{
    public class InitPoint : MonoBehaviour
    {
        [SerializeField] private UiInitializer _uiInitializer;

        private void Awake()
        {
            new PauseManager();

            _uiInitializer.Init();
        }
    }
}
