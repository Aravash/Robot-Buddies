using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressPromptUI : MonoBehaviour
{
    
    [SerializeField] private string button_name = "Fire1";
    [SerializeField]private GameObject prompt;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button_name))
        {
            prompt.SetActive(true);
            Destroy(gameObject);
        }
    }
}
