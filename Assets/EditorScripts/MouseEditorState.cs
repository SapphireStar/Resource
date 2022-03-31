using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.EventSystems;

public class MouseEditorState : moveWithMouse
{
    private int tempPrefab;
    public List<GameObject> WallPrefab;
    public int currentPrefab;
    public LayerMask tiles;
    public DirectoryInfo dir;
    public string[] MapType = { "LRChunks", "LRBChunks", "LRTChunks", "LRBTChunks" };//
    public int MapState;//判断当前所在的地图块类型
    int count;//储存在当前区块文件夹中区块的数量,并以此为依据来对区块进行命名
    // Start is called before the first frame update
    protected override void Start()
    {
        EditorState = true;//编辑模式下编辑器模式默认开启
        MapState = 0;
        currentPrefab = 0;
        mousePosOnScreen = Input.mousePosition;

        mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        transform.position = new Vector3(Mathf.RoundToInt(mousePosInWorld.x) + 0.5f, Mathf.RoundToInt(mousePosInWorld.y) + 0.5f, transform.position.z);
        radius = 0.45f;
        player = GameObject.Find("Player");
        GetComponent<SpriteRenderer>().color = new Color((95 / 255f), (255 / 255f), (116 / 255f), (130 / 255f));

/*        GameObject[] Chunks = Resources.LoadAll<GameObject>(MapType[MapState]);//计算当前文件夹中有多少区块，赋值给count
        count = Chunks.Length;*/
        dir = new DirectoryInfo("Assets/Scripts/MapGeneration/Prefabs/Resources/" + MapType[MapState]);//获取该类型地图块的区块数量
        FileInfo[] fil = dir.GetFiles();
        foreach (var item in fil)
        {
            string s = item.ToString();
            if (!s.Contains("meta"))//去除临时文件
            {
                count++;
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        moveToMouse();
        checkHasCollider();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pause");
            Time.timeScale = 0;
        }
        if (Input.GetKeyUp(KeyCode.Space) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))//判断当前的瓦片
        {
            currentPrefab = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentPrefab = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentPrefab = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentPrefab = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentPrefab = 4;
        }
        if (!Physics2D.OverlapCircle(transform.position, 0.2f, tiles))
        {
            if (Input.GetKey(KeyCode.Mouse0) && !isIn)
            {
                OnClick();
            }
        }

/*        if (Input.GetKeyDown(KeyCode.E))
        {
            addToPrefab();
        }*/
    }
    protected override void checkHasCollider()
    {
        bool checkIsInBlock = Physics2D.OverlapBox(transform.position, new Vector2(0.9f, 0.9f), 0, BlockLayer);
        isIn = Physics2D.OverlapCircle(transform.position, radius, layers);


        if (isIn || checkIsInBlock)
        {


            GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 90 / 255f, 67 / 255f, 130 / 255f);
        }
        else if (!isIn && !checkIsInBlock)
        {


            GetComponent<SpriteRenderer>().color = new Color(95 / 255f, 255 / 255f, 116 / 255f, 130 / 255f);
        }
    }

    public void OnClick()
    {
        if (currentPrefab < WallPrefab.Count)
        {
            GameObject newTile = Instantiate(WallPrefab[currentPrefab], transform.position, Quaternion.identity);
            newTile.transform.SetParent(GameObject.Find("TargetChunk").transform);//将新生成的瓦片加入目标区块的子物体
        }
    }
    public void addToPrefab()
    {
        string name = "Assets/Scripts/MapGeneration/Prefabs/Resources/" + MapType[MapState] + "/" + MapType[MapState] + count + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(GameObject.Find("TargetChunk"), "Assets/Scripts/MapGeneration/Prefabs/Resources/" + MapType[MapState] + "/" + MapType[MapState] + count + ".prefab");//导出prefab
        count++;
    }
    public void saveAsXML()
    {
        ChunkSave save = new ChunkSave();

        string name = "Assets/Scripts/MapGeneration/Prefabs/Resources/" + MapType[MapState] + "/" + MapType[MapState] + count + ".txt";
        save.SaveButton(name);
        count++;
    }
    public void UpdateCount()//切换场景后更新count
    {

        count = 0;
        dir = new DirectoryInfo("Assets/Scripts/MapGeneration/Prefabs/Resources/" + MapType[MapState]);//获取该类型地图块的区块数量
        FileInfo[] fil = dir.GetFiles();
        foreach (var item in fil)
        {
            string s = item.ToString();
            if (!s.Contains("meta"))//去除临时文件
            {
                count++;
            }
        }

/*        GameObject[] Chunks = Resources.LoadAll<GameObject>(MapType[MapState]);//计算当前文件夹中有多少区块，赋值给count
        count = Chunks.Length;
        Debug.Log(count);*/
    }


    
}
