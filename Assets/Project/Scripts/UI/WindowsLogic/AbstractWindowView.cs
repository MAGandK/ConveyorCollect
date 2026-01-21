using UnityEngine;

namespace UI.WindowsLogic
{
    public abstract class AbstractWindowView : MonoBehaviour, IWindowView
    {
        [SerializeField] private Canvas _canvas;
        
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        public void SetOrderInLayer(int order)
        {
            _canvas.sortingOrder = order;
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}