using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    
    public static UIToggle instance;
    
    [SerializeField]
    private GameObject[] UI_views = new GameObject[3];

    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void changeUIState(PlayerManager.UI_state state)
    {
        switch (state)
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

    public void ContinueTest()
    {
        PlayerManager.instance.ExitEscapeMenu();
    }

    public void EndTest()
    {
        //exit to the main menu
    }
}
