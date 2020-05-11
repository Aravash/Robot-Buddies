using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRoom : MonoBehaviour
{
    public Animator[] doorAnim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    public float waitTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim[0].SetBool(IsOpen, true);
        doorAnim[1].SetBool(IsOpen, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(transition());
        }
    }

    private IEnumerator transition()
    {
        doorAnim[0].SetBool(IsOpen, false);
        yield return new WaitForSeconds(waitTime);
        doorAnim[1].SetBool(IsOpen, true);
        
    }
}
