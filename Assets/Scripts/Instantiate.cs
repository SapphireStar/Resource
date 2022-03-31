using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Instantiate : MonoBehaviour
{
    public float maxDistance;

    [Header("Prefebs")]
    public GameObject normalObstacle;
    public GameObject weakObstacle;
    public GameObject strongObstacle;

    private GameObject currentObstacle;
    public GameObject saw;
    public GameObject playerLaser;

    [Header("PlayerStates")]
    public int resource;
    public bool flag;
    public int blockState=1;//用于判断当前生成物的对象
    public Quaternion rotate;
    float angle;

    GameObject text;
    GameObject mouse;
/*    public List<GameObject> blocks = new List<GameObject>();*/
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 3;
        mouse = GameObject.Find("Mouse");
        text = GameObject.Find("Canvas/Text");
        currentObstacle = normalObstacle;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //text.GetComponent<Text>().text = "Resource: " + resource;
        InstantiateObstacles();
        rotateObject();
        rotate = Quaternion.Euler(0, 0, angle);
    }
    void rotateObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            angle -= 90;

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
           angle += 90;

        }
    }
/*    void InstantiateAttackBlocks()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            blockState = 4;
            currentObstacle = saw;
        }
    }*/
    void InstantiateObstacles()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentObstacle = normalObstacle;
            blockState = 1;

        }
        if (weakObstacle != null)//判断当前关卡是否允许使用该方块，如果prefab未指定（为空），则无法使用该方块
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentObstacle = weakObstacle;
                blockState = 2;
            }
        }
        if (strongObstacle != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentObstacle = strongObstacle;
                blockState = 3;

            }
        }
        if (saw != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentObstacle = saw;
                blockState = 4;

            }
        }
        if (playerLaser != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentObstacle = playerLaser;
                blockState = 5;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && flag)
        {

            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 mousePosOnScreen = Input.mousePosition;

            Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
            float X = mousePosInWorld.x - transform.position.x;
            float Y = mousePosInWorld.y - transform.position.y;
            float distance = Mathf.Sqrt(X * X + Y * Y);
            if (distance < maxDistance)
            {
                if (currentObstacle == normalObstacle && resource >= 2)
                {
                    resource -= 2;
                    Instantiate(currentObstacle, mouse.transform.position, rotate);

                }
                else if (currentObstacle == weakObstacle && resource >= 1)
                {
                    resource -= 1;
                    Instantiate(currentObstacle, mouse.transform.position, rotate);
   
                }
                else if (currentObstacle == strongObstacle && resource >= 4)
                {
                    resource -= 4;
                    Instantiate(currentObstacle, mouse.transform.position, rotate);
        
                }
                else if(currentObstacle == saw && resource >= 2)
                {
                    saw.GetComponent<saw>().flag = true;
                    resource -= 2;
                    Instantiate(currentObstacle, mouse.transform.position, rotate);

                }
                else if (currentObstacle == playerLaser && resource >= 4)
                {
   
                    
                    resource -= 4;
                    Instantiate(currentObstacle, mouse.transform.position,rotate);
                }
                else Debug.Log("Run out of Ressources!");
            } 
            else Debug.Log("Too far");
        }
    } 


    /*    public void OnTriggerEnter2D(Collider2D collision)
        {

            flag = false;
        }
        public void OnTriggerExit2D(Collider2D collision)
        {

            flag = true;
        }*/
}
