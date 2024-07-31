using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightToggle : MonoBehaviour
{
    public GameObject sunLight;  // Assign your Sun light here
    public GameObject moonLight; // Assign your Moon light here

    void Start()
    {
    }

    public void OnToggleValueChanged(bool toggle)
    {
        if (toggle)
        {
            sunLight.SetActive(false);
            moonLight.SetActive(true);
        }
        else
        {
            moonLight.SetActive(false);
            sunLight.SetActive(true);
        }
    }
}