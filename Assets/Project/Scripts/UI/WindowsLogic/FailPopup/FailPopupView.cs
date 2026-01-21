using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.WindowsLogic.FailPopup
{
    public class FailPopupView : AbstractWindowView
    {
       [SerializeField] private Button _restartButton;

       public void SubscribeButton(UnityAction onRestartButtonClick)
       {
           _restartButton.onClick.AddListener(onRestartButtonClick);
       }
    }
}
