using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Vector2 MoveInput;
    [SerializeField] private float panSpeed;
    // Start is called before the first frame update
    void Start()
    {

        panSpeed = 10;
    }

    // Update is called once per frame
    void Update()//根据鼠标的移动改变摄像机的位置
    {
        Vector2 currentPos = transform.position;
        Vector2 mousePos = Input.mousePosition;
        MoveInput.Set(0,0);
        if(Input.GetAxis("Mouse ScrollWheel") > 0)//改变视角远近
        {
            gameObject.GetComponent<Camera>().orthographicSize -= 0.5f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            gameObject.GetComponent<Camera>().orthographicSize += 0.5f;
        }
        if (mousePos.x > 0.9f * Screen.width && mousePos.x < Screen.width&&transform.position.x<15)
        {
            MoveInput.x = 1;
        }

        if (mousePos.x < 0.1f * Screen.width && mousePos.x > 0&&transform.position.x>-15)
        {
            MoveInput.x = -1;
        }
        if (mousePos.y > 0.9f * Screen.height && mousePos.y < Screen.height&&transform.position.y<13)
        {
            MoveInput.y = 1;
        }
        if (mousePos.y < 0.1f * Screen.height && mousePos.y > 0&&transform.position.y>-13)
        {
            MoveInput.y = -1;
        }
        currentPos += MoveInput * Time.deltaTime*panSpeed;

        transform.position =new Vector3(currentPos.x,currentPos.y,-10);
    }
}
