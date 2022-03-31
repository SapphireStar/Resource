using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chunk
{
    public  List<Vector2> Wall = new List<Vector2>();

    public  List<Vector2> DamageWall = new List<Vector2>();

    public  List<Vector2> MovingDamageBlockX = new List<Vector2>();

    public  List<Vector2> MovingDamageBlockY = new List<Vector2>();

    public  List<Vector2> Enemy = new List<Vector2>();




}
