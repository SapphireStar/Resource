using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider PlayerHealthBar;
    public PlayerController player;
    public List<Blockselected> blockSelectedList = new List<Blockselected>();

    // Start is called before the first frame update
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<PlayerController>();
        PlayerHealthBar = GameObject.FindObjectOfType<Slider>();
    }
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthBar == null||player==null)
        {
            PlayerHealthBar = GameObject.FindObjectOfType<Slider>();
            player = FindObjectOfType<PlayerController>();
        }
        selectBlock();
    }
    public void UpdatePlayerHealthBar()
    {
        PlayerHealthBar.value = player.health;
    }
    public void isBlockSelected(Blockselected blockSprite)
    {
        blockSelectedList.Add(blockSprite);
    }
    public void selectBlock()//更改选中方块在UI中显示的图片
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeBlockImage(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeBlockImage(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeBlockImage(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeBlockImage(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            changeBlockImage(4);
        }

    }
    void changeBlockImage(int number)
    {
        if (number < blockSelectedList.Count)
        {
            blockSelectedList[number].switchToSelected();
            foreach (var block in blockSelectedList)
            {
                if (block != blockSelectedList[number])
                {
                    block.switchToUnselected();
                }
            }
        }
    }

}
