using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInputPrompt : MonoBehaviour
{

    private TMP_Text txt;
    private Image img;
    private float opacity = 0;
    [SerializeField] private float goal_pos;
    [SerializeField] private string button_name;
    [SerializeField] private bool i_want_to_die = false;

    private Vector3 target_pos;
    
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TMP_Text>();
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button_name == "selfdestruct" && i_want_to_die) Destroy(gameObject, 4);
        else if (Input.GetButtonDown(button_name) && i_want_to_die) Destroy(gameObject);

        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;

        Vector2 target_pos = new Vector2(pos.x, goal_pos);

        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(pos, target_pos, Time.deltaTime);
        
        if(opacity < 1) opacity += Time.deltaTime;

        Color col = new Color(1, 1, 1, opacity);

        img.color = col;
        txt.color = col;
    }
}
