namespace UI.WindowsLogic
{
    public interface IWindowController
    {
        void Initialize();
        void Show();
        void Hide();
        void SetUIController(UIControllers.UIController uiController);
    }
}
