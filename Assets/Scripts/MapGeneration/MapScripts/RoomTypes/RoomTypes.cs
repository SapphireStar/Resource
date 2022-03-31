using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Xml;

public class RoomTypes : MonoBehaviour
{
    public Tilemap WallMap;
    public Tilemap DamageMap;
    public int Type;
    public Transform[] children;
    public GameObject chunk;
    public Transform[] Chunks;
    public List<GameObject> ChunkType;//储存属于该地图块的所有区块

    public GameObject WallPrefab;
    public GameObject DamageWallPrefab;
    public string MapType;
    public int count;//用来记录文件夹内的文件数量，在随机生成区块中需要用到
    private DirectoryInfo dir;
    protected virtual void Start()
    {
        LoadFromXML();
        if (ChunkType.Count > 0)
        {

/*            GameObject ChunkObject = Instantiate(*//*ChunkType[Random.Range(0, ChunkType.Length)]*//*ChunkType[Random.Range(0, ChunkType.Count)], transform.position, Quaternion.identity);//随机生成一个区块，并将其作为地图块的子物体
            ChunkObject.transform.SetParent(transform);//将区块设为地图块的子物体 */
        }

    }
    public void RoomDestruction()
    {
       
        chunk = transform.GetChild(transform.childCount-1).gameObject;
        children = new Transform[this.gameObject.transform.childCount+chunk.transform.childCount];
        GameObject Map = GameObject.Find("WallMap");
        WallMap = Map.GetComponent<Tilemap>();
        GameObject Damage = GameObject.Find("DamageBlock");
        DamageMap = Damage.GetComponent<Tilemap>();//获取tilemap组件
        
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {

            children[i] = this.gameObject.transform.GetChild(i);//获取想要消除的地图块的子物体
        }

        int mainLength = children.Length-chunk.transform.childCount;
        for (int i = children.Length- chunk.transform.childCount; i < children.Length; i++)
        {
            children[i] = chunk.transform.GetChild(i - mainLength);
        }


        foreach (var child in children)//消除主体
        {
            if (child != null)
            {
                Vector3Int currentPos = new Vector3Int(Mathf.FloorToInt(child.position.x), Mathf.FloorToInt(child.position.y), Mathf.FloorToInt(child.position.z));
                WallMap.SetTile(currentPos, null);//去除原地图块绘制的tile，因为如果只消除地图块，只会消除定位点，所以需要清除所有的tilemap
                DamageMap.SetTile(currentPos,null);
            }
        }
        if (GameManager.Instance != null)
        {

            List<MovingDamageBlock> tempmovingBlocks = new List<MovingDamageBlock>();
            foreach (var blocks in GameManager.Instance.movingBlocks)//修改地图块时，若地图块含有movingdamageblock，则需要把game manager list中的对应moving block移除（因为该moving block已为空）
            {
                if (blocks != null)
                    tempmovingBlocks.Add(blocks);
            }
            GameManager.Instance.movingBlocks = tempmovingBlocks;
        }
        

        Destroy(gameObject);
    }
    public void LoadFromXML()
    {

        if (File.Exists("Assets/Scripts/MapGeneration/Prefabs/Resources/"+MapType+"/"+MapType+"0.txt"))
        {

            Chunk chunk = new Chunk();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Assets/Scripts/MapGeneration/Prefabs/Resources/"+MapType+"/" + MapType + Random.Range(0, returnCount()).ToString() + ".txt");

            #region LoadInformationFromXML
            XmlNodeList WallX = xmlDocument.GetElementsByTagName("WallX");
            XmlNodeList WallY = xmlDocument.GetElementsByTagName("WallY");
            for (int i = 0; i < WallX.Count; i++)
            {
                //GameObject newObject = Instantiate(WallPrefab, new Vector2(float.Parse(WallX[i].InnerText), float.Parse(WallY[i].InnerText)), Quaternion.identity);
                GameObject newObject = Instantiate(WallPrefab, Vector2.zero, Quaternion.identity);
                newObject.transform.parent = transform;
                newObject.transform.localPosition = new Vector2(float.Parse(WallX[i].InnerText), float.Parse(WallY[i].InnerText));
            }

            XmlNodeList DamageWallX = xmlDocument.GetElementsByTagName("DamageWallX");
            XmlNodeList DamageWallY = xmlDocument.GetElementsByTagName("DamageWallY");
            for (int i = 0; i < DamageWallX.Count; i++)
            {
                //GameObject newObject = Instantiate(WallPrefab, new Vector2(float.Parse(WallX[i].InnerText), float.Parse(WallY[i].InnerText)), Quaternion.identity);
                GameObject newObject = Instantiate(DamageWallPrefab, Vector2.zero, Quaternion.identity);
                newObject.transform.parent = transform;
                newObject.transform.localPosition = new Vector2(float.Parse(DamageWallX[i].InnerText), float.Parse(DamageWallY[i].InnerText));
            }

            #endregion
        }
        else Debug.Log("FILE NOT FOUND");
    }
    public int returnCount()
    {

        count = 0;
        dir = new DirectoryInfo("Assets/Scripts/MapGeneration/Prefabs/Resources/"+MapType);//获取该类型地图块的区块数量
        FileInfo[] fil = dir.GetFiles();
        foreach (var item in fil)
        {
            //Debug.Log(item);
            string s = item.ToString();
            if (!s.Contains("meta"))//去除临时文件
            {
                count++;
            }
        }
        return count;
    }


}
