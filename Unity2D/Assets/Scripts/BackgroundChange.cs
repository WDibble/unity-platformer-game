using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    [SerializeField] private GameObject background1_1;
    [SerializeField] private GameObject background1_2;
    [SerializeField] private GameObject background1_3;
    [SerializeField] private GameObject background1_4;
    [SerializeField] private GameObject background2_1;

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BackgroundTrigger"))
        {
            //background1_1.SetActive(false);
            //background1_2.SetActive(false);
            //background1_3.SetActive(false);
            //background1_4.SetActive(false);
            
            if (!background2_1.activeInHierarchy)
            {
                background2_1.SetActive(true);
            }
        }
    }

}
