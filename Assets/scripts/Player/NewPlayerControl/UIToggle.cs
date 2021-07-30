using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] UI_views = new GameObject[3];

    // Update is called once per frame
    void Update()
    {
        switch (PlayerManager.instance.current_ui_state)
        {
            case PlayerManager.UI_state.ESCAPE_MENU: 
                UI_views[0].SetActive(true);
                UI_views[1].SetActive(false);
                UI_views[2].SetActive(false);
                break;
            case PlayerManager.UI_state.OUT_OF_CODEC:
                UI_views[0].SetActive(false);
                UI_views[1].SetActive(true);
                UI_views[2].SetActive(false);
                break;
            case PlayerManager.UI_state.USING_CODEC:
                UI_views[0].SetActive(false);
                UI_views[1].SetActive(false);
                UI_views[2].SetActive(true);
                break;
        }
    }
}
