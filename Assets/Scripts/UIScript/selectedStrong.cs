using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class selectedStrong : Blockselected
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Send());
    }

    // Update is called once per frame
    IEnumerator Send()
    {
        yield return new WaitForSeconds(0.03f);
        UIManager.instance.isBlockSelected(this);
    }

}
