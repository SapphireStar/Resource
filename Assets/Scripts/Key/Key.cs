using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public PlayerController player;
    public bool isEnemyKey;

    // Start is called before the first frame update
    void Start()
    {

            if (GameManager.Instance.Enemies.Count > 0)
            {
                isEnemyKey = true;
            }
            GameManager.Instance.isKey(this);
        
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.PlayerKeys.Add(this);
            gameObject.SetActive(false);
        }
    }
}
