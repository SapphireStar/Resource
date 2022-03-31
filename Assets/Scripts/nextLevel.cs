using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class nextLevel : MonoBehaviour
{
    public BoxCollider2D coll;
    GameObject player;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        player = GameObject.Find("Player");
       index = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            player.GetComponent<Instantiate>().resource = 0;
            /*            player.transform.Translate(new Vector2(11, 0));*/
            SceneManager.LoadScene(index + 1);
        }
    }
    public void OpenDoor()
    {
        coll.enabled = true;
    }
}
