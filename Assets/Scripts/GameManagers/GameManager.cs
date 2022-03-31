using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;
    public bool isDead;
    public List<getResource> resourceGot = new List<getResource>();
    public List<DestroyBlock> blockPlaced = new List<DestroyBlock>();//记录已经放置的方块
    public savePoint currentSavePoint;
    public List<Enemy> Enemies = new List<Enemy>();
    public List<Enemy> DeadEnemies = new List<Enemy>();
    public List<PlayerAttack> playerAttackBlocks = new List<PlayerAttack>();
    public List<MovingDamageBlock> movingBlocks = new List<MovingDamageBlock>();
    public List<Key> keys;
    public nextLevel ExitDoor;
    // Start is called before the first frame update
    public void Awake()
    {
        
        player = GameObject.FindObjectOfType<PlayerController>();
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
            
        }
        else Destroy(gameObject);
    }
    public void Start()
    {
        ExitDoor = GameObject.FindObjectOfType<nextLevel>();
    }
    public void Update()
    {
        if (ExitDoor == null)
        {
            return;
        }
        if (player.health <= 0)
        {
            isDead = true;

        }
        if (isDead)
        {
            foreach (var key in keys)
            {
                if (key != null && !key.isEnemyKey)
                {
                    key.gameObject.SetActive(true);//如果不是敌人掉落的钥匙，则将钥匙显示，若是敌人掉落的钥匙则保持隐藏
                }
            }

            player.PlayerKeys.Clear();
            ExitDoor.GetComponent<Animator>().SetBool("Open",false);//这里将切换动画状态的判定变量设为bool值，因为使用Trigger
                                                                    //会导致玩家死亡后出口依然打开
            ExitDoor.coll.enabled = false;//玩家死亡后让出口重新关闭

            // player.hasKey = currentSavePoint.PlayerKeyHad;
            if (currentSavePoint.PlayerKeyHad.Count > 0)
            {
                foreach (var key in currentSavePoint.PlayerKeyHad)//从存档中读取玩家在存档时的钥匙数量

                {
                    player.PlayerKeys.Add(key);
                }
            }
            if (movingBlocks.Count > 0)
            {
                foreach (MovingDamageBlock item in movingBlocks)
                {
                    item.transform.position = item.OriginalPosition;
                }
            }
            if (playerAttackBlocks != null)
            {
                foreach (PlayerAttack item in playerAttackBlocks)
                {
                    if(item!=null)//防止玩家的攻击方块因为自毁从而发生空引用
                    Destroy(item.gameObject);

                }
            }
            playerAttackBlocks.Clear();
            int tempCount = blockPlaced.Count;
            List<DestroyBlock> tempList = new List<DestroyBlock>();
            for (int i = 0; i < tempCount; i++)//去除在读取存档前被移除的方块
            {

                if (GameManager.Instance.blockPlaced[i] != null)
                    tempList.Add(GameManager.Instance.blockPlaced[i]);//存储不是空的对象
            }//在存档时清理gamemanager中已经被删除的block
            GameManager.Instance.blockPlaced = tempList;
            tempCount = blockPlaced.Count;

            
            for (int i = 0; i < tempCount; i++)//清除在上一存档点后玩家放置的方块
            {
                if (!currentSavePoint.blockPlacedBefore.Contains(blockPlaced[blockPlaced.Count-1]))
                {
                    DestroyBlock temp = blockPlaced[blockPlaced.Count - 1];
                    blockPlaced.Remove(temp);
                    Destroy(temp.gameObject);

                }
            }
            foreach (getResource item in resourceGot)
            {
                if (currentSavePoint.resourcesGotBefore.Contains(item))
                {

                    item.gameObject.SetActive(false);

                }
                else item.gameObject.SetActive(true);
            }
            foreach (Enemy item in Enemies)
            {
                if (!currentSavePoint.enemyBeated.Contains(item))
                {
                    item.gameObject.SetActive(true);
                    item.transform.position = item.OriginalPosition;
                    item.TransitionToState(item.patrolState);
                    item.GetComponent<Animator>().SetBool("isDead", false);
                    item.isDead = false;
                    item.gameObject.layer = 13;
                }
            }
            player.transform.position = currentSavePoint.playerStatus;
            player.GetComponent<Instantiate>().resource = currentSavePoint.playerResource;
            isDead = false;
            player.health = 5;
            UIManager.instance.UpdatePlayerHealthBar();

        }

    }
    public void deadEnemy(Enemy enemy)
    {
        DeadEnemies.Add(enemy);
    }
    public void isEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }
    public void isPlayerAttack(PlayerAttack attackBlocks)
    {
        playerAttackBlocks.Add(attackBlocks);
    }
    public void isMovingDamageBlock(MovingDamageBlock block)
    {
        movingBlocks.Add(block);
    }
    public void isKey(Key key)
    {

        keys.Add(key);
    }
    public void removeNullKeys()
    {
        List<Key> tempkeys = new List<Key>();
        foreach (var key in keys)
        {
            if (key != null)
                tempkeys.Add(key);
        }
        keys = tempkeys;
    }

    public void checkIfPlayerHasKey()
    {
        if (player.PlayerKeys.Count==keys.Count||Enemies.Count==DeadEnemies.Count&&GameObject.FindObjectOfType<Key>() == null)//判断何时打开出口
        {
            ExitDoor.GetComponent<Animator>().SetBool("Open",true);

        }


    }


}
