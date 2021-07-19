using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUIElement : MonoBehaviour
{
    [SerializeField]private GameObject prompt;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;
        if(other.CompareTag("Player"))
        {
            prompt.SetActive(true);
            hasPlayed = true;
        }
    }
}
