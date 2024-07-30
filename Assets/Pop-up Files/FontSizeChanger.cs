using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FontSizeChanger : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public float fontSizeIncrement = 1f;
 
    public void IncreaseFontSize()
    {
        if(textComponent != null)
        {
            textComponent.fontSize += fontSizeIncrement;
        }
    }

    public void DecreaseFontSize()
    {
        if(textComponent != null && textComponent.fontSize > fontSizeIncrement)
        {
            textComponent.fontSize -= fontSizeIncrement;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
