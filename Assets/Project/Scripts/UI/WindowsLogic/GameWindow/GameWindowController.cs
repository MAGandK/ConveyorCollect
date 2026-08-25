using System;
using UI.WindowsLogic.PausePopup;

namespace UI.WindowsLogic.GameWindow
{
    public class GameWindowController : AbstractWindowController<GameWindowView>
    { 
        public event Action PauseClicked;
        public event Action RestartClicked;

        private readonly GameWindowView _gameWindowView;

        public GameWindowController(GameWindowView view) : base(view)
        {
            _gameWindowView = view;
        }

        public override void Initialize()
        {
            _gameWindowView.SubscribeButton(
                OnPauseButtonClick,
                OnRestartButtonClick
            );
        }
        
        public void SetLevel(int level)
        {
            _gameWindowView.SetLevel(level);
        }

        private void OnPauseButtonClick()
        {
            _uiController.ShowWindow<PausePopupController>();
            PauseClicked?.Invoke();
        }

        private void OnRestartButtonClick()
        {
            RestartClicked?.Invoke();
        }
    }
}
