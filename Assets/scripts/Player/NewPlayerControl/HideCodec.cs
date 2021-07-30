using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCodec : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.has_codec)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(this);
        }
        else transform.GetChild(0).gameObject.SetActive(false);
    }
}
