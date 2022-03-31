using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintDamageBlock : PaintWall
{

    public Tile DamageWallTile;
    // Start is called before the first frame update
    protected override void Start()
    {
      
        GameObject Map = GameObject.Find("DamageBlock");
        WallMap = Map.GetComponent<Tilemap>();
        Vector3Int currentPos = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), Mathf.FloorToInt(transform.position.z));
        WallMap.SetTile(currentPos, DamageWallTile);

    }
}
