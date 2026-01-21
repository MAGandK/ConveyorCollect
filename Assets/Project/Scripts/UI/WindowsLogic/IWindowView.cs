namespace UI.WindowsLogic
{
    public interface IWindowView
    {
        void Show();
        void Hide();
        
        void SetOrderInLayer(int order);
    }
}