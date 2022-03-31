using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw : MonoBehaviour
{
    public Animator anim;
    public float launchTime;
    public float launch;
    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

                Invoke("Launch",0.5f);
                Destroy(gameObject, 2);

    }
    void Launch()
    {
        anim.SetTrigger("launch");
    }

}
