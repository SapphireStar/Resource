using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed = 10f;
    bool isGrounded;
    public float height = 5f;


    void Start()
    {
        isGrounded = true;
        speed = 7f;
        height = 10;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.A)) //左
        {
            /*            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);*/
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);


        }
        if (Input.GetKey(KeyCode.D)) //右
        {
            //this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);

        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.up * height;
            // this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * height);
        }





    }
    public void OnTriggerEnter2D(Collider2D collider2D)
    {

        if (collider2D.tag == "floor")
        {

            isGrounded = true;
        }
    }


}
