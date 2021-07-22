using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float camDist = 0;   
    private float currentDist = 0;
    
    [SerializeField]
    private Vector2 input;
    [SerializeField]
    private Camera codec_cam;
    [SerializeField]
    private float rotateSpeed = 90;
    
    [SerializeField]private Transform topic;
    private float heading = 0;
    private float tilt = 0;
    public float topicHeight = 0.5f;
    [SerializeField]private float sensitivity = float.MaxValue;
    
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDist = camDist;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.using_codec) PushButton();
        else GetInput();
    }
    
    private void LateUpdate()
    {
        if (PlayerManager.instance.using_codec) return;
        
        MoveCam();
    }
    
    /*
    * moves camera based on input variable
    */
    private void MoveCam()
    {
        if (input.magnitude > 0)
        {
            tilt += Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

            heading += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

            tilt = Mathf.Clamp(tilt, -90, 90);
        }

        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        transform.position = topic.position - transform.forward * currentDist + Vector3.up * topicHeight;
    }
    
    /*
     * pushing the two turning buttons on the codec
     * god this is ugly (im talking about YOU 🕎)
     */
    void PushButton()
    {
        int layerMask = 1 << 10;
        RaycastHit hit;
        Ray ray = codec_cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit, 10f, layerMask))
            {
                if (hit.transform.gameObject.CompareTag("leftButton"))
                {
                    PlayerManager.instance.rotateRobots(-rotateSpeed);
                }
                else if (hit.transform.gameObject.CompareTag("rightButton"))
                {
                    PlayerManager.instance.rotateRobots(rotateSpeed);
                }
            }
        }
    }
    
    void GetInput()
    {
        input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        input = Vector2.ClampMagnitude(input, 1);
    }
}
