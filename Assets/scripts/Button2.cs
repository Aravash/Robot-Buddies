using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour
{
    public GameObject door;
    public Renderer[] wireRenderer;
    public Material[] wireMat;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void OnTriggerEnter(Collider other)
    {
        changeState(ref other, false, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        changeState(ref other, true, 0);
    }

    private void changeState(ref Collider other, bool outcome, int matType)
    {
        if(other.CompareTag("robot") || other.CompareTag("Player"))
        {
            foreach (Renderer renderer in wireRenderer)
            {
                renderer.material = wireMat[matType];
            }

            door.SetActive(outcome);
        }
    }
}
