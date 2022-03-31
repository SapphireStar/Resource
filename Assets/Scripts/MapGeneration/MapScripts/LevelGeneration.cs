using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] StartPoints;
    public GameObject[] Rooms;//0 -> LR, 1 -> LRB, 2 -> LRT, 3 -> LRBT
    public GameObject InitialRoom;//起始房间
    public float MoveAmount = 10;
    int direction;
    float TimeBtRoom;
    float StartTimeBtRoom = 0.25f;
    public bool StopGeneration;
    public float minX;
    public float maxX;
    public float minY;
    public LayerMask room;
    int downCounter;
    GameObject player;
    public GameObject entrance;
    private GameObject exitDoor;
    public GameObject spawnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        exitDoor = GameObject.Find("ExitDoor");
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        StartTimeBtRoom = 0.25f;
        int RandStarting = Random.Range(0, StartPoints.Length);//随机地图起始位置
        transform.position = StartPoints[RandStarting].position;
        Instantiate(InitialRoom, transform.position, Quaternion.identity);
        entrance.transform.position = new Vector3(transform.position.x, transform.position.y-7.5f, transform.position.z);
        player.transform.position = new Vector3(transform.position.x, transform.position.y-7.5f, transform.position.z);
        direction = Random.Range(1, 4);//生成随机数来判断下一个地图块的方向
/*        if (direction == 1 || direction == 2)//生成出生平台
        {
            Instantiate(spawnPlatform, new Vector3(transform.position.x - 7, transform.position.y - 7, transform.position.z), Quaternion.identity);
            entrance.transform.position = new Vector3(transform.position.x - 7, transform.position.y - 5, transform.position.z);
            player.transform.position = new Vector3(transform.position.x - 7, transform.position.y - 5, transform.position.z);
        }
        else if(direction == 3 || direction == 4)
        {
           GameObject left =  Instantiate(spawnPlatform, new Vector3(transform.position.x + 7, transform.position.y -7, transform.position.z), Quaternion.identity);
            entrance.transform.position = new Vector3(transform.position.x + 7, transform.position.y - 5, transform.position.z);
            player.transform.position = new Vector3(transform.position.x + 7, transform.position.y - 5, transform.position.z);
        }
        else 
        {
            Instantiate(spawnPlatform, new Vector3(transform.position.x , transform.position.y+3 , transform.position.z), Quaternion.identity);
            entrance.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            player.transform.position = new Vector3(transform.position.x , transform.position.y + 5, transform.position.z);
        }*/

    }
    void Update()
    {

        if (TimeBtRoom <= 0 && !StopGeneration)
        {
            Move();
            TimeBtRoom = StartTimeBtRoom;
        }
        else
        {
            TimeBtRoom -= Time.deltaTime;
        }
    }
    public void GenerateLeftMaps()//在剩余空缺的地图块中随机生成模板地图块
    {

        for (int x = -5; x < 56; x += 20)
        {
            for (int y = 5; y > -56; y -= 20)
            {
                //Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (!Physics2D.OverlapCircle(new Vector2(x,y), 1, room))//判断当前位置是否已经存在地图块，若不存在，则随机生成一张地图
                {
                    int rand = Random.Range(0, 4);
                    Instantiate(Rooms[rand], new Vector2(x, y), Quaternion.identity);
                }
            }
        }
    }
    private void Move()
    {

        if (direction == 1 || direction == 2) //如果随机数等于1和2，则下一个地图块生成在右边
        {
            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + MoveAmount, transform.position.y);
                transform.position = newPos;
                int rand = Random.Range(0, 4);
                Instantiate(Rooms[rand], transform.position, Quaternion.identity);
                direction = Random.Range(1, 6);//判断下一个地图块的位置
                if (direction == 3)//如果随机出3，则当作2（因为下一个地图块不能往回生成在左边，因此3当作2）
                {
                    direction = 2;
                }
                else if (direction == 4)//如果随机出4，则当作5（因为下一个地图块不能生成在左边，因此当随机出4和5时，向下生成地图）
                {
                    direction = 5;
                }
            }
            else direction = 5;
        }
        else if (direction == 3 || direction == 4)//等于3和4，生成在左边
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - MoveAmount, transform.position.y);
                transform.position = newPos;
                direction = Random.Range(3, 6);
                int rand = Random.Range(0, 4);
                Instantiate(Rooms[rand], transform.position, Quaternion.identity);
            }
            else direction = 5;
        }
        else if (direction == 5)//等于5，生成在下面
        {
            if (transform.position.y > minY)
            {
                downCounter++;
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (downCounter >= 2)
                {
                    roomDetection.GetComponent<RoomTypes>().RoomDestruction();
                    Instantiate(Rooms[3], transform.position, Quaternion.identity);
                    GameManager.Instance.removeNullKeys();
                }
                else
                {
                    if (roomDetection.GetComponent<RoomTypes>().Type != 1 && roomDetection.GetComponent<RoomTypes>().Type != 3)
                    {

                        roomDetection.GetComponent<RoomTypes>().RoomDestruction();
                        int random = Random.Range(0, 2);
                        random = random == 0 ? random = 1 : random = 3;
                        Instantiate(Rooms[random], transform.position, Quaternion.identity);
                        GameManager.Instance.removeNullKeys();
                    }
                }
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - MoveAmount);
                transform.position = newPos;
                direction = Random.Range(1, 6);//因为这里需要判断向下生成地图块后，下一个地图块的位置，所以可以是任意方向。
                int rand = Random.Range(2, 4);
                Instantiate(Rooms[rand], transform.position, Quaternion.identity);

            }
            else
            {
                StopGeneration = true;
                exitDoor.transform.position = new Vector3(transform.position.x, transform.position.y - 8, transform.position.z);
                GenerateLeftMaps();
            }
        }


    }
}
