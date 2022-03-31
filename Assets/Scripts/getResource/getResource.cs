using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getResource : MonoBehaviour
{
    public PlayerController player;
    public float distance;
    public float customDistance;
    public Vector2 mouse;
    public float X;
    public float Y;
    public bool canGet;
    // Start is called before the first frame update
    public void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }
    void Start()
    {


        customDistance = 3f;

    }

    // Update is called once per frame
    void Update()
    {
       // Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosOnScreen = Input.mousePosition;

        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);

        float x = player.transform.position.x - this.transform.position.x;
        float y = player.transform.position.y - this.transform.position.y;
        distance = Mathf.Sqrt(x * x + y * y);
        X = this.transform.position.x;
        Y = this.transform.position.y;
        mouse = mousePosInWorld;
        if (distance < customDistance)
        {
            if (mouse.x < X + 0.5f && mouse.x > X - 0.5f && mouse.y < Y + 0.5f && mouse.x > Y - 0.5f)
            {
                canGet = true;
            }
            else canGet = false;
        }
        else canGet = false;
    }




}
