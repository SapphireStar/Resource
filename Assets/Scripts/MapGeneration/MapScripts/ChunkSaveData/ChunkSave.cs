using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class ChunkSave : MonoBehaviour
{
    public Transform TargetChunk;
    // Start is called before the first frame update

    public void SaveButton(string path)
    {
        SaveByXML(path);
    }
    public void LoadButton()
    {
        LoadByXML();
    }
    private void SaveByXML(string path)
    {
        Chunk chunk = createChunkData();
        XmlDocument xmlDocument = new XmlDocument();

        #region CreateXML elements
        XmlElement root = xmlDocument.CreateElement("Chunk");

        XmlElement WallX, WallY, DamageWallX,DamageWallY, MovingDamageBlockXPosX, MovingDamageBlockXPosY, MovingDamageBlockYPosX, MovingDamageBlockYPosY, EnemyX, EnemyY;

        foreach (var wall in chunk.Wall)
        {
            WallX = xmlDocument.CreateElement("WallX");
            WallY = xmlDocument.CreateElement("WallY");
            WallX.InnerText = wall.x.ToString();
            WallY.InnerText = wall.y.ToString();
            root.AppendChild(WallX);
            root.AppendChild(WallY);
        }
        foreach (var damageWall in chunk.DamageWall)
        {
            DamageWallX = xmlDocument.CreateElement("DamageWallX");
            DamageWallY = xmlDocument.CreateElement("DamageWallY");
            DamageWallX.InnerText = damageWall.x.ToString();
            DamageWallY.InnerText = damageWall.y.ToString();
            root.AppendChild(DamageWallX);
            root.AppendChild(DamageWallY);
        }
        foreach (var movingDamageBlockX in chunk.MovingDamageBlockX)
        {
            MovingDamageBlockXPosX = xmlDocument.CreateElement("MovingDamageBlockXPosX");
            MovingDamageBlockXPosY = xmlDocument.CreateElement("MovingDamageBlockXPosY");
            MovingDamageBlockXPosX.InnerText = movingDamageBlockX.x.ToString();
            MovingDamageBlockXPosY.InnerText = movingDamageBlockX.y.ToString();
            root.AppendChild(MovingDamageBlockXPosX);
            root.AppendChild(MovingDamageBlockXPosY);
        }
        foreach (var movingDamageBlockY in chunk.MovingDamageBlockX)
        {
            MovingDamageBlockYPosX = xmlDocument.CreateElement("MovingDamageBlockYPosX");
            MovingDamageBlockYPosY = xmlDocument.CreateElement("MovingDamageBlockYPosY");
            MovingDamageBlockYPosX.InnerText = movingDamageBlockY.x.ToString();
            MovingDamageBlockYPosY.InnerText = movingDamageBlockY.y.ToString();
            root.AppendChild(MovingDamageBlockYPosX);
            root.AppendChild(MovingDamageBlockYPosY);
        }
        foreach (var enemy in chunk.Enemy)
        {
            EnemyX = xmlDocument.CreateElement("EnemyX");
            EnemyY = xmlDocument.CreateElement("EnemyY");
            EnemyX.InnerText = enemy.x.ToString();
            EnemyY.InnerText = enemy.y.ToString();
            root.AppendChild(EnemyX);
            root.AppendChild(EnemyY);
        }

        #endregion
        xmlDocument.AppendChild(root);
        xmlDocument.Save(path);
        if (File.Exists(path))
        {
            Debug.Log("XML FILE SAVED");
        }
    }
    private void LoadByXML()
    {

    }
    public Chunk createChunkData()
    {
        TargetChunk = GameObject.Find("TargetChunk").transform;
        
        Chunk chunk = new Chunk();
        for(int i = 0; i < TargetChunk.childCount; i++)
        {
            if (TargetChunk.GetChild(i).tag == "Wall")
            {
                chunk.Wall.Add(TargetChunk.GetChild(i).position);
            }
            if (TargetChunk.GetChild(i).tag == "DamageWall")
            {
                chunk.DamageWall.Add(TargetChunk.GetChild(i).position);
            }
            if (TargetChunk.GetChild(i).tag == "MovingDamageBlockX")
            {
                chunk.MovingDamageBlockX.Add(TargetChunk.GetChild(i).position);
            }
            if (TargetChunk.GetChild(i).tag == "MovingDamageBlockY")
            {
                chunk.MovingDamageBlockY.Add(TargetChunk.GetChild(i).position);
            }
            if (TargetChunk.GetChild(i).tag == "Enemy")
            {
                chunk.Enemy.Add(TargetChunk.GetChild(i).position);
            }

        }

        return chunk;
    }
}
