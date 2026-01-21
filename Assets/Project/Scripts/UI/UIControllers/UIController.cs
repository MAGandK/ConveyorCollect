using System;
using System.Collections.Generic;
using System.Linq;
using UI.WindowsLogic;

namespace UI.UIControllers
{
    public class UIController : IUIController
    {
        public static UIController Instance { get; private set; }

        private readonly IEnumerable<IWindowController> _controllers;
        private readonly List<IWindowController> _openedWindows = new();

        private int _orderIndex;

        public UIController(IEnumerable<IWindowController> controllers)
        {
            if (Instance != null)
            {
                throw new Exception("UIControllers instance already exists!");
            }

            Instance = this;

            _controllers = controllers;

            foreach (var windowController in _controllers)
            {
                windowController.SetUIController(this);
                windowController.Hide();
            }
        }

        public void ShowWindow<T>() where T : IWindowController
        {
            var window = _controllers.FirstOrDefault(x => x is T);

            if (window == null || (_openedWindows.Count > 0 && window == _openedWindows[^1]))
            {
                return;
            }

            if (window is not IPopController popController)
            {
                foreach (var windowController in _openedWindows)
                {
                    windowController.Hide();
                }

                _orderIndex = 0;
            }
            else
            {
                popController.SetOrderInLayer(++_orderIndex);
            }

            _openedWindows.Add(window);
            window.Show();
        }

        public T GetWindow<T>() where T : class, IWindowController
        {
            return _controllers.FirstOrDefault(x => x is T) as T;
        }

        public void CloseLastOpenPopup()
        {
            var windowController = _openedWindows[^1];

            if (windowController is not IPopController)
            {
                return;
            }

            windowController.Hide();
            _openedWindows.Remove(windowController);
            _orderIndex--;
        }
    }
}
