using TMPro;
using UnityEngine;

namespace UI.WindowsLogic.Timer
{
    public class TimerView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        public void UpdateTimerText(float currentTime)
        {
            var minutes = Mathf.FloorToInt(currentTime / 60);
            var seconds = Mathf.FloorToInt(currentTime % 60);
            _timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
