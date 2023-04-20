using System.Collections;
using UnityEngine;
using TMPro;

public class FinalTimeDisplay : MonoBehaviour
{
    private Timer timer;
    public float countUpDuration = 2f; // Time for the counting up animation

    private void Start()
    {
        timer = FindObjectOfType<Timer>();

        if (timer != null)
        {
            StartCoroutine(AnimateFinalTime());
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Final Time: Not available";
        }
    }

    private IEnumerator AnimateFinalTime()
    {
        //float finalTime = timer.GetTime();
        float finalTime = 145;
        float elapsedTime = 0f;

        while (elapsedTime < countUpDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / countUpDuration;
            float displayedTime = Mathf.Lerp(0, finalTime, progress);

            GetComponent<TextMeshProUGUI>().text = "Final Time:\n" + displayedTime.ToString("F0") + " Seconds";
            yield return null;
        }

        GetComponent<TextMeshProUGUI>().text = "Final Time:\n" + finalTime.ToString("F0") + " Seconds";
    }
}
