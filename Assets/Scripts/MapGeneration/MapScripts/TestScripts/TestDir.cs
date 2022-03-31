using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestDir : MonoBehaviour
{
    public DirectoryInfo dir;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        dir = new DirectoryInfo("Assets/Scripts/MapGeneration/Prefabs/LRBChunks");
        FileInfo[] fil = dir.GetFiles();
        foreach (var item in fil)
        {
            Debug.Log(item);
            string s = item.ToString();
            if (!s.Contains("meta"))
            {
                count++;
            }
        }
        Debug.Log(count);
    }


}
