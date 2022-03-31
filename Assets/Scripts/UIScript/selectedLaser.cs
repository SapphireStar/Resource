using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedLaser : Blockselected
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Send());
    }

    IEnumerator Send()
    {
        yield return new WaitForSeconds(0.05f);
        UIManager.instance.isBlockSelected(this);
    }

}
