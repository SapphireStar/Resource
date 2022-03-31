using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elavator : MonoBehaviour
{
    bool flag;
    float time;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        time = 0;
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (time < 1)
            {
                transform.Translate(new Vector2(0, speed * Time.deltaTime));
                time += Time.deltaTime;
            }
            else flag = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x - 1.5f, transform.position.y + 0.3f, transform.position.z), transform.right, 3, 1);
        Debug.DrawLine(new Vector3(transform.position.x - 1.5f, transform.position.y + 0.3f, transform.position.z), hit.point);
        if (hit.collider.tag == "Player")
        {
            flag = true;
        }
    }
}
