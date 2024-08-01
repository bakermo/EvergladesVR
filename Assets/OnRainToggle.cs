using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRainToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rain;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnToggleValueChanged(bool toggle)
    {
        if (toggle)
        {
            rain.SetActive(true);
        }
        else
        {
            rain.SetActive(false);
        }
    }
}
