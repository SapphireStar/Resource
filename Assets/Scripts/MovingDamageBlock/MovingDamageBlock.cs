using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingDamageBlock : MonoBehaviour
{
    public Vector3 OriginalPosition;
    protected bool IsEditorMode;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (SceneManager.GetActiveScene().name=="MapEditor")
        {
            IsEditorMode = true;
        }
        OriginalPosition = transform.position;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.isMovingDamageBlock(this);
        }
    }
    public void EraseBlock()//擦除方块
    {
        Destroy(transform.parent.gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Mouse1)&&IsEditorMode)//如果鼠标进入方块并右击，则擦除方块
        {
            EraseBlock();
        }
    }



}
