using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWeak : DestroyBlock
{


    // Update is called once per frame
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

        if (distance<2&&mouse.x < X + 0.5f && mouse.x > X - 0.5f && mouse.y < Y + 0.5f && mouse.x > Y - 0.5f && Input.GetKeyUp(KeyCode.Mouse1) && player.GetComponent<PlayerController>().isGrounded)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<PlayerController>().isGrounded = false;
            player.GetComponent<PlayerController>().flag = true;
            flag = 1;


        }
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 0.501f, transform.position.z), transform.up, 0.1f, 1);
        Debug.DrawLine(transform.position, hit.point);

        if (flag == 1 && time <= 1)
        {
            time += Time.deltaTime;
        }
        if (time >= 1)
        {
            player.GetComponent<Instantiate>().resource += 1;
            gameObject.SetActive(false);
            flag = 0;
        }


    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DestroyWeak());
    }
    IEnumerator DestroyWeak()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

}
