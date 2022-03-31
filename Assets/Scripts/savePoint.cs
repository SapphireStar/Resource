using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePoint : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerStatus;
    public int playerResource;
    public List<getResource> resourcesGotBefore = new List<getResource>();//创建一个list储存在存档点前已经被玩家获取的资源
    public List<DestroyBlock> blockPlacedBefore = new List<DestroyBlock>();//创建一个list储存在存档点前已经被玩家放置的方块
    public List<Enemy> enemyBeated = new List<Enemy>();
    public List<Key> PlayerKeyHad;//存档时玩家的钥匙数量

    bool isSaved;
     void Start()
    {

        player = GameObject.Find("Player");
    }
    public void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Player"&&!isSaved)
        {
            foreach (var key in player.GetComponent<PlayerController>().PlayerKeys )
            {
                PlayerKeyHad.Add(key);
            }
            int tempCount =GameManager.Instance.blockPlaced.Count;
            int x = tempCount;
            List<DestroyBlock> tempList = new List<DestroyBlock>();
            for (int i = 0; i < tempCount; i++)//去除在读取存档前被移除的方块
            {

                if (GameManager.Instance.blockPlaced[i] != null)
                    tempList.Add(GameManager.Instance.blockPlaced[i]);//存储不是空的对象
            }//在存档时清理gamemanager中已经被删除的block
            GameManager.Instance.blockPlaced = tempList;
            Debug.Log("saved");
            playerStatus = collision.transform.position;//存储玩家的位置
            foreach (getResource item in GameManager.Instance.resourceGot)
            {
                resourcesGotBefore.Add(item);
            }//储存到达存档点时，玩家已经获取的资源点
            foreach (DestroyBlock item in GameManager.Instance.blockPlaced)
            {
                blockPlacedBefore.Add(item);
            }
            foreach (Enemy item in GameManager.Instance.DeadEnemies)
            {
                enemyBeated.Add(item);
            }
            playerResource = player.GetComponent<Instantiate>().resource;//存储玩家的资源
            GameManager.Instance.currentSavePoint = this;
            isSaved = true;


        }
    }
}
