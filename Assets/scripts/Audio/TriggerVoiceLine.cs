using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVoiceLine : MonoBehaviour
{
    private AudioDirector dir;
    public int voiceLineNumber = 0;
    private bool hasPlayed = false;

    private void Start()
    {
        dir = GetComponentInParent<AudioDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;
            if(other.CompareTag("Player"))
        {
            dir.PlayLine(voiceLineNumber);
            hasPlayed = false;
        }
    }
}
