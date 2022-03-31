using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PaintWall : MonoBehaviour
{
    public Tilemap WallMap;
    public RuleTile WallTile;

    // Start is called before the first frame update
    protected virtual void Start()
    {

        GameObject Map = GameObject.Find("WallMap");
        WallMap = Map.GetComponent<Tilemap>();
        Vector3Int currentPos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), Mathf.FloorToInt(transform.position.z));
        WallMap.SetTile(currentPos, WallTile);

    }
    public void EraseWall()
    {
        Vector3Int currentPos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), Mathf.FloorToInt(transform.position.z));
        WallMap.SetTile(currentPos, null);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            EraseWall();
        }
    }


}
