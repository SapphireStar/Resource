using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Win : MonoBehaviour
{
    GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = GameObject.Find("Win");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Text.GetComponent<Text>().text = "YOU WIN!";
            Time.timeScale = 0;
        }
    }
}
