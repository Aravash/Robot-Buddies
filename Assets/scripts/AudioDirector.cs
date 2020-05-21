using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    public Animator[] mechArmAnim = new Animator[2];
    public Transform demoBot;
    public AudioClip[] voiceLines;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        source.clip = voiceLines[0];
        source.Play();
        yield return new WaitForSeconds(30);
        mechArmAnim[0].enabled = true;
        yield return new WaitForSeconds(12);
        demoBot.SetParent(null);
        mechArmAnim[1].enabled = true;
    }
}
