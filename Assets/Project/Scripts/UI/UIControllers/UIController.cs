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

            if (window == null)
            {
                return;
            }

            if (window is IPopController popController)
            {
                if (_openedWindows.Contains(window))
                {
                    _openedWindows.Remove(window);
                }
                else
                {
                    popController.SetOrderInLayer(++_orderIndex);
                }

                _openedWindows.Add(window);
                window.Show();
                return;
            }

            foreach (var openedWindow in _openedWindows)
            {
                openedWindow.Hide();
            }

            _openedWindows.Clear();
            _orderIndex = 0;
            _openedWindows.Add(window);
            window.Show();
        }

        public T GetWindow<T>() where T : class, IWindowController
        {
            return _controllers.FirstOrDefault(x => x is T) as T;
        }

        public void CloseLastOpenPopup()
        {
            if (_openedWindows.Count == 0)
            {
                return;
            }

            var windowController = _openedWindows[^1];

            if (windowController is not IPopController)
            {
                return;
            }

            windowController.Hide();
            _openedWindows.Remove(windowController);

            if (_orderIndex > 0)
            {
                _orderIndex--;
            }
        }
    }
}
