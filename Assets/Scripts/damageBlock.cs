using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class damageBlock : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            GameManager.Instance.isDead = true;
        }
        else if (collision.tag == "strongBlock"&&GetComponent<Animation>()!=null)
        {
            GetComponent<Animation>().enabled = false;
        }
    }
}
