using Zenject;

namespace UI.WindowsLogic
{
    public abstract class AbstractWindowController<T> : IInitializable, IWindowController where T : IWindowView
    {
        private T _baseView;
        protected UIControllers.UIController _uiController;

        protected AbstractWindowController(T view)
        {
            _baseView = view;
        }

        public virtual void Initialize()
        {
        }

        public void Show()
        {
            _baseView.Show();
            OnShow();
        }

        public void Hide()
        {
            _baseView.Hide();
            OnHide();
        }

        public void SetUIController(UIControllers.UIController uiController)
        {
            _uiController = uiController;
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}