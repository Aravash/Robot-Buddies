using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour
{
    public GameObject door;
    public Renderer[] wireRenderer;
    public Material[] wireMat;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    private bool firstTimePressed = true;
    public AudioDirector dir;

    private void OnTriggerEnter(Collider other)
    {
        ChangeState(ref other, false, 1);
        if (!firstTimePressed) return;
        firstTimePressed = false;
        //dir.PlayLine(1);
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeState(ref other, true, 0);
    }

    private void ChangeState(ref Collider other, bool outcome, int matType)
    {
        if (!other.CompareTag("robot") && !other.CompareTag("Player")) return;
        foreach (Renderer rend in wireRenderer)
        {
            rend.material = wireMat[matType];
        }

        door.SetActive(outcome);
    }
}
