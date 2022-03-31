using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public MouseEditorState mouse;
    public GameObject TargetChunk;
    public List<GameObject> chunkObjects;
    [Header("Rooms")]
    public GameObject roomLR;
    public GameObject roomLRB;
    public GameObject roomLRT;
    public GameObject roomLRBT;
    public List<RoomTypes> roomTypes;
    void Start()
    {

        mouse = GameObject.FindObjectOfType<MouseEditorState>();

        TargetChunk = GameObject.Find("TargetChunk");

    }
    public void EraseAll()//删除之前所画的内容
    {

        int count = TargetChunk.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            chunkObjects.Add(TargetChunk.transform.GetChild(i).gameObject);
        }
        foreach (var block in chunkObjects)
        {
            if (block.GetComponent<PaintWall>() != null)
            {
                block.GetComponent<PaintWall>().EraseWall();
            }
            else if (block.GetComponent<PaintDamageBlock>() != null)
            {
                block.GetComponent<PaintDamageBlock>().EraseWall();
            }
            
            Destroy(block);
        }
        chunkObjects.Clear();

        Debug.Log("EraseAll");
    }
    public void GoToLR()//对LR地图块进行编辑，并且将当前地图状态设置为0，以便判断
    {
        RoomTypes tempObject = GameObject.FindObjectOfType<RoomTypes>();
        tempObject.RoomDestruction();
        Instantiate(roomLR, Vector3.zero, Quaternion.identity);
        EraseAll();
        mouse.MapState = 0;

        PlayerPrefs.SetFloat("currentRoom", 0);
    }
    public void GoToLRB()
    {

        GameObject tempObject = GameObject.FindObjectOfType<RoomTypes>().gameObject;
        tempObject.GetComponent<RoomTypes>().RoomDestruction();
        Destroy(tempObject);
        Instantiate(roomLRB, Vector3.zero, Quaternion.identity);
        EraseAll();
        mouse.MapState = 1;
        PlayerPrefs.SetFloat("currentRoom", 1);
    }
    public void GoToLRT()
    {
        RoomTypes tempObject = GameObject.FindObjectOfType<RoomTypes>();
        tempObject.RoomDestruction();
        Instantiate(roomLRT, Vector3.zero, Quaternion.identity);
        EraseAll();
        mouse.MapState = 2;
        PlayerPrefs.SetFloat("currentRoom", 2);
    }
    public void GoToLRBT()
    {
        RoomTypes tempObject = GameObject.FindObjectOfType<RoomTypes>();
        tempObject.RoomDestruction();
        Instantiate(roomLRBT, Vector3.zero, Quaternion.identity);
        EraseAll();
        mouse.MapState = 3;
        PlayerPrefs.SetFloat("currentRoom", 3);
    }
}
