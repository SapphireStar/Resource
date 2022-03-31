using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Hide", 1);
    }
    void Hide()
    {
        gameObject.SetActive(false);
        Invoke("Show", 1.5f);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
}
