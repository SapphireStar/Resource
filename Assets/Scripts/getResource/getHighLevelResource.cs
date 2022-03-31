using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getHighLevelResource : getResource
{
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.Mouse0) && canGet)
        {

            StartCoroutine(addHighLevelResource());
        }

    }

    IEnumerator addHighLevelResource()
    {

        yield return new WaitForSecondsRealtime(0.1f);
        if(!GameManager.Instance.resourceGot.Contains(this))
        GameManager.Instance.resourceGot.Add(this);
        player.GetComponent<Instantiate>().resource += 4;
        gameObject.SetActive(false);

    }


}
