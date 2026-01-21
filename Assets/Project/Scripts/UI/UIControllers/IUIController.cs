using UI.WindowsLogic;

namespace UI.UIControllers
{
    public interface IUIController
    {
        void ShowWindow<T>() where T : IWindowController;

        T GetWindow<T>() where T : class, IWindowController;

        void CloseLastOpenPopup();
    }
}