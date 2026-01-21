using System;
using UI.WindowsLogic.GameWindow;

namespace UI.WindowsLogic.WinPopup
{
    public class WinPopupController : AbstractPopupController<WinPopupView>
    {
        public event Action Won;

        private readonly WinPopupView _winView;

        public WinPopupController(WinPopupView view) : base(view)
        {
            _winView = view;
        }

        public override void Initialize()
        {
            base.Initialize();

            _winView.SubscribeButton(OnContinueButtonClick);
        }

        private void OnContinueButtonClick()
        {
            _uiController.ShowWindow<GameWindowController>();
            Won?.Invoke();
        }
    }
}
