using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0,1)]
    public float TimeOfDay;
    public float DayDuration = 30f;

    public Light Sun;

    private void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;
        if (TimeOfDay >= 1)
        {
            TimeOfDay -= 1;
        }

        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, -30, 0);
    }
}
