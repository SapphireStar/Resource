using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlocks : MonoBehaviour
{
    GameObject player;
    Vector2 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x - 2f, transform.position.y + 0.7f, transform.position.z), transform.right, 4, 1);
        Debug.DrawLine(new Vector3(transform.position.x - 2f, transform.position.y + 0.7f, transform.position.z), hit.point);

    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x - 2f, transform.position.y + 0.7f, transform.position.z), -transform.right, 4, 1);
        Debug.DrawLine(new Vector3(transform.position.x - 2f, transform.position.y + 0.7f, transform.position.z), hit.point);
        if (hit.collider.tag == "Player")
        {
            player.transform.SetParent(gameObject.transform);
        }

    }

    public void OnCollisionExit2D(Collision2D collision)
    {

        player.transform.SetParent(null);
    }

}
