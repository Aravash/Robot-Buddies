using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    public Animator[] mechArmAnim = new Animator[2];
    public AudioClip[] voiceLines;

    private void Start()
    {
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        Debug.Log("Track 1 Plays");
        yield return new WaitForSeconds(2);
        mechArmAnim[0].enabled = true;
        yield return new WaitForSeconds(2);
        mechArmAnim[1].enabled = true;
    }
}
