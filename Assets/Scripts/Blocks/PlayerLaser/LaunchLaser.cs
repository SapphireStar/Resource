using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchLaser : MonoBehaviour
{
    public float launchTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("launchLaser", launchTime);
    }
    void launchLaser()
    {
        gameObject.GetComponent<PlayerLaser>().enabled = true;
    }
}
