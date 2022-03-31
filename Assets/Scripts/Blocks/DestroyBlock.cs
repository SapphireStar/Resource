using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    public GameObject player;
    public Vector2 mouse;
    public float X;
    public float Y;
    public int flag;
    public float time = 0;
    public float distance;
    public bool isAdd;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.blockPlaced.Add(this);
        isAdd = true;

        player = GameObject.Find("Player");

        flag = 0;

    }

    // Update is called once per frame
    


}

