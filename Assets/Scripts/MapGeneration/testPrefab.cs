using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class testPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PrefabUtility.CreatePrefab("Assets/Scripts/MapGeneration/Prefabs/testPrefabs/testPre.prefab", this.gameObject);
        PrefabUtility.SaveAsPrefabAsset(this.gameObject, "Assets/Scripts/MapGeneration/Prefabs/testPrefabs/testPre.prefab");
        Destroy(gameObject,1);
    }


}
