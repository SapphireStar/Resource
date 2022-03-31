using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getNormalResource : getResource
{


    // Update is called once per frame
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.Mouse0) && canGet)
            StartCoroutine(addNormalResource());

    }

    IEnumerator addNormalResource()
    {

        yield return new WaitForSecondsRealtime(0.1f);
        if(!GameManager.Instance.resourceGot.Contains(this))
        GameManager.Instance.resourceGot.Add(this);

        player.GetComponent<Instantiate>().resource += 2;
        gameObject.SetActive(false);

    }  
}
