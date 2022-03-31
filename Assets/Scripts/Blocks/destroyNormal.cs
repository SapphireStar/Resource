using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyNormal : DestroyBlock
{
    void Update()
    {

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosOnScreen = Input.mousePosition;

        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        mouse = mousePosInWorld;
        X = this.transform.position.x;
        Y = this.transform.position.y;
        float x = player.transform.position.x - this.transform.position.x;
        float y = player.transform.position.y - this.transform.position.y;
        distance = Mathf.Sqrt(x * x + y * y);
        if (distance < 2.5 && mouse.x < X + 0.5f && mouse.x > X - 0.5f && mouse.y < Y + 0.5f && mouse.x > Y - 0.5f && Input.GetKeyUp(KeyCode.Mouse1) && player.GetComponent<PlayerController>().isGrounded)//检测鼠标是否在方块内抬起右键
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<PlayerController>().isGrounded = false;
            player.GetComponent<PlayerController>().flag = true;
            flag = 1;


        }
        if (flag == 1 && time <= 1)
        {
            time += Time.deltaTime;
        }
        if (time >= 1)
        {
            player.GetComponent<Instantiate>().resource += 2;
            /*            player.GetComponent<Instantiate>().blocks.Remove(gameObject);*/
            flag = 0;
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }


    }
}