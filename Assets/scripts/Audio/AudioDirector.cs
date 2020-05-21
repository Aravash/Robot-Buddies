using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDirector : MonoBehaviour
{
    public Animator[] mechArmAnim = new Animator[2];
    public Door[] wall = new Door[2];
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
        PlayLine(0);
        yield return new WaitForSeconds(28);
        wall[0].SetOpen(true);
        yield return new WaitForSeconds(2);
        mechArmAnim[0].enabled = true;
        yield return new WaitForSeconds(10);
        wall[1].SetOpen(true);
        yield return new WaitForSeconds(2);
        demoBot.SetParent(null);
        mechArmAnim[1].enabled = true;
    }

    public void PlayLine(int voiceLine)
    {
        source.clip = voiceLines[voiceLine];
        source.Play();
    }
}
