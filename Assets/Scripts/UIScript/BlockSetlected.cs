using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blockselected : MonoBehaviour
{
    protected Image image;
    public Sprite selected;
    public Sprite unselected;
    // Start is called before the first frame update
     protected virtual void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame

    public void switchToSelected()
    {
        image.sprite = selected;
    }
    public void switchToUnselected()
    {
        image.sprite = unselected;
    }
}
