using UI.WindowsLogic.PausePopup;

namespace UI.WindowsLogic.GameWindow
{
    public class GameWindowController : AbstractWindowController<GameWindowView>
    {
        private GameWindowView _gameWindowView;

        public GameWindowController(GameWindowView view) : base(view)
        {
            _gameWindowView = view;
        }

        public override void Initialize()
        {
            base.Initialize();

            _gameWindowView.SubscribeButton(OnPauseButtonClick, OnRestartButtonClick);

        }

        private void OnPauseButtonClick()
        {
            _uiController.ShowWindow<PausePopupController>();

        }

        private void OnRestartButtonClick()
        {
            _uiController.ShowWindow<GameWindowController>();
        }
    }
}
