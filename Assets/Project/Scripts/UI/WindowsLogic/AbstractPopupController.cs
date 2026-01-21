using DG.Tweening;
using UnityEngine;

namespace UI.WindowsLogic
{
    public abstract class AbstractPopupController<T> : AbstractWindowController<T>, IPopController where T : IWindowView
    {
        [Space] private T _baseView;
        private Tweener _showTween;

        protected AbstractPopupController(T view) : base(view)
        {
            _baseView = view;
        }

        public void SetOrderInLayer(int order)
        {
            _baseView.SetOrderInLayer(order);
        }
    }
}