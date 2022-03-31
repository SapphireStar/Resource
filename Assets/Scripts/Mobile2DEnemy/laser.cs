using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class laser : MonoBehaviour
{
    public GameObject blocks;
    protected LineRenderer lineRederer;
    protected GameObject player;
    public LayerMask layers;
    public RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        lineRederer = GetComponent<LineRenderer>();

        lineRederer.enabled = true;

        lineRederer.useWorldSpace = true;

        player = GameObject.Find("Player");

      
    }








    // Update is called once per frame
    public virtual void Update()
    {


        hit= Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, layers);


        Debug.DrawLine(transform.position, hit.point);



        lineRederer.SetPosition(0, transform.position);

        lineRederer.SetPosition(1, hit.point);

        //激光触碰到玩家

        if (hit.collider.tag == "Player")

        {
            GameManager.Instance.isDead=true;
            /*player.transform.position = GameManager.Instance.savepoint;*/
           /* Destroy(hit.collider.gameObject, 0);*/
            
        }
        if (hit.collider.tag == "block")
        {
            Debug.Log("触发销毁");

            Destroy(hit.collider.gameObject, 0);
        }







    }
    public void FixedUpdate()
    {

    }

}

