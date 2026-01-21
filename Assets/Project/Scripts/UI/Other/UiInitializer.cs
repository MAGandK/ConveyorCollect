using System.Collections.Generic;
using UI.WindowsLogic;
using UI.WindowsLogic.GameWindow;
using UI.WindowsLogic.PausePopup;
using UI.UIControllers;
using UI.WindowsLogic.FailPopup;
using UI.WindowsLogic.WinPopup;
using UnityEngine;

namespace UI.Other
{
    public class UiInitializer : MonoBehaviour
    {
        [SerializeField] private GameWindowView _gameWindowView;
        [SerializeField] private FailPopupView _failPopupView;
        [SerializeField] private WinPopupView _popupView;
        [SerializeField] private PausePopupView _pausePopupView;
        public UIController UIController { get; private set; }
        public WinPopupController WinPopupController { get; set; }
        public GameWindowController GameWindowController { get; set; }
        public PausePopupController PausePopupController { get; set; }
        public FailPopupController FailPopupController { get; set; }

        public void Init()
        {
            var gameView  = GetComponentInChildren<GameWindowView>(true);
            var failView  = GetComponentInChildren<FailPopupView>(true);
            var winView   = GetComponentInChildren<WinPopupView>(true);
            var pauseView = GetComponentInChildren<PausePopupView>(true);

            GameWindowController  = new GameWindowController(gameView);
            FailPopupController  = new FailPopupController(failView);
            WinPopupController   = new WinPopupController(winView);
            PausePopupController  = new PausePopupController(pauseView);

            var list = new List<IWindowController>
            {
                GameWindowController,
                FailPopupController,
                WinPopupController,
                PausePopupController
            };

            list.ForEach(c => c.Initialize());

            UIController = new UIController(list);
        }
    }
}
