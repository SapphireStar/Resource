﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class RoomLR : RoomTypes
{
    // Start is called before the first frame update
    public void Awake()
    {
        addChunk();
    }
    protected override void Start()
    {
        MapType = "LRChunks";
        base.Start();
    }
    public void addChunk()
    {
        GameObject[] newObjects = Resources.LoadAll<GameObject>("LRChunks");//利用resourcesAPI来读取prefabs
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
    public string deletePrefab(string s)
    {

        return s;
    }


}
