using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWithMouse : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject checkObject;
    public GameObject player;
    public float radius;
    protected Vector2 mousePosOnScreen;
    protected Vector2 mousePosInWorld;
    public LayerMask layers;
    public LayerMask BlockLayer;
    public Collider2D CheckIsIn;
    public bool isIn;
    protected int currentState;
    public bool EditorState;//判断编辑模式（是否在地图编辑器内）是否开启的判定依据
    public List<GameObject> Prefebs = new List<GameObject>();

    float angle;
    protected virtual void Start()
    {
        EditorState = false;//普通模式下编辑器模式默认关闭
        mousePosOnScreen = Input.mousePosition;

        mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        transform.position = new Vector3(Mathf.RoundToInt(mousePosInWorld.x)+0.5f, Mathf.RoundToInt(mousePosInWorld.y)+0.5f, transform.position.z);

        for (int i = 0; i < transform.childCount; i++)
            {

              Prefebs.Add(transform.GetChild(i).gameObject);//需要注意子物体顺序

            }
        radius = 0.45f;
        player = GameObject.Find("Player");
        GetComponent<SpriteRenderer>().color = new Color((95/255f), (255/255f), (116/255f), (130/255f));
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        currentState = player.GetComponent<Instantiate>().blockState;
        setObject();
        moveToMouse();
        checkHasCollider();
        rotateObject();
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    protected virtual void setObject()
    {
        for(int i=0; i < Prefebs.Count; i++)
        {
            if (currentState - 4 == i)
            {
                Prefebs[i].SetActive(true);
            }
            else Prefebs[i].SetActive(false);
            
        }
    }
    protected virtual void rotateObject()
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
    protected virtual void checkHasCollider()//判断当前位置是否可以放置方块
    {
                bool checkIsInBlock = Physics2D.OverlapBox(transform.position, new Vector2(1.5f,1.5f), 0, BlockLayer);
         isIn=Physics2D.OverlapCircle(transform.position, radius, layers);


        if (isIn || checkIsInBlock)
        {
 
            player.GetComponent<Instantiate>().flag = false;
            GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 90 / 255f, 67 / 255f, 130 / 255f);
        }
        else if (!isIn && !checkIsInBlock)
        {

            player.GetComponent<Instantiate>().flag = true;
            GetComponent<SpriteRenderer>().color = new Color(95 / 255f, 255 / 255f, 116 / 255f, 130 / 255f);
        }
    }


    protected void moveToMouse()
    {
        mousePosOnScreen = Input.mousePosition;

        mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        if (mousePosInWorld.x > transform.position.x + 0.5)//获取鼠标位置后，将预选框移动到鼠标位置，实现方法：当鼠标 x上的位置大于预选框时，预选框在x上的位置+1，以此类推
        {
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (mousePosInWorld.x < transform.position.x - 0.5)
        {
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
        if (mousePosInWorld.y > transform.position.y + 0.5)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
        if (mousePosInWorld.y < transform.position.y - 0.5)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
