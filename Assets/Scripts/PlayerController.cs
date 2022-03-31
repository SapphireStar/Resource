using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour, IDamageable
{
    private LevelGeneration levelgeneration;
    public static PlayerController instance;
    // Start is called before the first frame update
    public GameObject CheckObject;
    public GameObject player;
    public Animator anim;
    public bool flag;
    float time;
    Scene scene;
    public float downSpeed;
    float jumpTime;
    public float ForceInAir;
    Rigidbody2D rb;
    [Header("PlayerStates")]
    public float speed = 10f;
    public bool isGrounded;
    public float height = 5f;
    public bool isInAir;
    public float health;
    public List<Key> PlayerKeys;
    [Header("CheckIsGrounded")]
    public float radius;
    public LayerMask GroundLayers;

    void Start()
    {
        levelgeneration = GameObject.FindObjectOfType<LevelGeneration>().GetComponent<LevelGeneration>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isInAir = false;
        jumpTime = 0;
        downSpeed = 10;

        //transform.position = GameManager.currentSavepoint.playerStatus.position;
        scene =  SceneManager.GetActiveScene(); 
        isGrounded = true;
        speed = 7f;
        height = 12;

        flag = false;
        UIManager.instance.UpdatePlayerHealthBar();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isDead)
            return;
        Movement(); 
        ResetScene();
        Jump();
        CheckIsGrounded();
        if(levelgeneration.StopGeneration==true)//生成完地图后再判断玩家是否有钥匙
        GameManager.Instance.checkIfPlayerHasKey();
        if (flag)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            flag = false;
        }
        anim.SetFloat("SpeedY", rb.velocity.y);




    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("jump");
            isInAir = true;
            isGrounded = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, Vector2.up.y * height);
            //this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * height);
        }
        if (Input.GetKeyUp(KeyCode.Space) && isInAir && GetComponent<Rigidbody2D>().velocity.y >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, 0);
            isInAir = false;
        }
    }
    public void ResetScene()
    {
        if (transform.position.y < -70f)
        {
            GameManager.Instance.isDead=true ;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.isDead = true;
            /* transform.position = GameManager.Instance.savepoint;*/
        }
    }
    public void Movement()
    {
        float horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        float horizontalInput = Input.GetAxis("Horizontal");
        if(isGrounded)
        rb.velocity = new Vector2(horizontalInputRaw * speed, rb.velocity.y);//地面移动
        else if (horizontalInput != 0 && !isGrounded)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);//空中移动
        }
        if (horizontalInputRaw != 0)
        {
            transform.localScale = new Vector3(horizontalInputRaw, 1, 1);//翻转人物
            anim.SetBool("isMoving", true);
        }
        else anim.SetBool("isMoving", false);



        
    }
    public void CheckIsGrounded()
    {
       isGrounded= Physics2D.OverlapCircle(new Vector2(CheckObject.transform.position.x,CheckObject.transform.position.y), radius, GroundLayers);
        anim.SetBool("Ground", 
             Physics2D.OverlapCircle(new Vector2(CheckObject.transform.position.x, CheckObject.transform.position.y), radius, GroundLayers));//判断是否落地，并更改动画的状态


    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(CheckObject.transform.position.x, CheckObject.transform.position.y), radius);
    }

    public void GetHit(float damage)
    {
        health -= damage;
        UIManager.instance.UpdatePlayerHealthBar();
        anim.SetTrigger("Hit");
    }
}
