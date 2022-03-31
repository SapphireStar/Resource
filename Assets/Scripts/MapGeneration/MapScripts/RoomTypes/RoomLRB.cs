using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLRB : RoomTypes
{
    public void Awake()
    {
        addChunk();
    }
    protected override void Start()
    {
        MapType = "LRBChunks";
        base.Start();
    }
    public void addChunk()
    {
        GameObject[] newObjects = Resources.LoadAll<GameObject>("LRBChunks");//利用resourcesAPI来读取prefabs
        foreach (var item in newObjects)
        {
            ChunkType.Add(item);//将文件夹中的prefabs动态添加到对应地图块的chunks list中
        }
        /*        DirectoryInfo dir = new DirectoryInfo("Assets/Scripts/MapGeneration/Prefabs/Resources/LRChunks");//获取该类型地图块的区块数量
                if (dir.Exists)
                {

                    FileInfo[] fil = dir.GetFiles();


                }*/
    }
}
