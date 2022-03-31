using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectedNormal : Blockselected
{
    // Start is called before the first frame update

     protected override void Start()
    {
        base.Start();
        StartCoroutine(send());
        image.sprite = selected;
    }

    // Update is called once per frame
    IEnumerator send()
    {
        yield return new WaitForSeconds(0.01f);
        UIManager.instance.isBlockSelected(this);
    }

}
