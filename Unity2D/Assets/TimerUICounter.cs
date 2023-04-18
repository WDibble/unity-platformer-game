using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUICounter : MonoBehaviour
{
    // Reference to the Timer script
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = timer.GetTime().ToString("F0");
    }
}
