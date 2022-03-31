using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTargetBlock : MonoBehaviour
{
    public LayerMask hitLayers;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(mouse);
            RaycastHit2D hit = Physics2D.Raycast(mousePosInWorld, Vector2.right, 0.01f, hitLayers);
            if (hit)
            {
                Debug.DrawLine(mousePosInWorld, hit.point);
                this.transform.position = mousePosInWorld;
                Debug.Log("hit");
            }
        }
    }
}
