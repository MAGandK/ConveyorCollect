using System;

namespace UI.WindowsLogic.FailPopup
{
    public class FailPopupController : AbstractPopupController<FailPopupView>
    {
        private FailPopupView _failPopupView;
        public event Action RestartClicked;

        public FailPopupController(FailPopupView view) : base(view)
        {
            _failPopupView = view;
        }

        public override void Initialize()
        {
            base.Initialize();
            _failPopupView.SubscribeButton(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            _uiController.CloseLastOpenPopup();
            RestartClicked?.Invoke();
        }
    }
}
