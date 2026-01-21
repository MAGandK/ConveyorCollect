using System;
using Game.Managers;
using UI.WindowsLogic.GameWindow;

namespace UI.WindowsLogic.PausePopup
{
    public class PausePopupController : AbstractPopupController<PausePopupView>
    {
        public event Action Restarted;
        public event Action Closed;

        private PausePopupView _pausePopupView;

        public PausePopupController(PausePopupView view) : base(view)
        {
            _pausePopupView = view;
        }

        public override void Initialize()
        {
            base.Initialize();
            _pausePopupView.BackButton.onClick.AddListener(OnCloseButtonClick);
            _pausePopupView.RestartButton.onClick.AddListener(OnRestartButtonClick);
        }

        protected override void OnShow()
        {
            base.OnShow();
            PauseManager.Instance.SetPause(true);
        }

        private void OnCloseButtonClick()
        {
            PauseManager.Instance.SetPause(false);
            _uiController.CloseLastOpenPopup();
            Closed?.Invoke();
        }

        private void OnRestartButtonClick()
        {
            _uiController.ShowWindow<GameWindowController>();
            _uiController.CloseLastOpenPopup();
            Restarted?.Invoke();
        }
    }
}
