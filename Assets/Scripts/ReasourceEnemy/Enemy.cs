using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    public Animator anim;
    public EnemyBaseState currentState;
    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    public int animState;
    // Start is called before the first frame update
    public Transform target_A;
    public Transform target_B;
    public Transform TargetPoint;
    public float speed;
    private GameObject AlarmSign;
    public Transform player;
    public GameObject key;
    public GameObject KeySignal;

    [Header("Basic States")]
    public float health;
    public bool isDead;
    public bool isBoss;
    public bool hasBomb;
    public Vector3 OriginalPosition;
    public bool hasKey;
    [Header("AttackSettings")]
    public float attackRange;
    public float skillRange;
    public float attackRate;
    public float nextAttack;
    public float nextSkill;
    public List<Transform> attackList = new List<Transform>();
    public void Awake()
    {
        Init();

    }
    public virtual void Init()
    {
        if (GameObject.FindObjectOfType<PlayerController>() != null)
        {
            player = GameObject.FindObjectOfType<PlayerController>().transform;
        }
        anim = GetComponent<Animator>();
        AlarmSign = transform.GetChild(0).gameObject;
        OriginalPosition = transform.position;


    }
    void Start()
    {
        if (hasKey)
        {
            Invoke("hideKey", 0.5f);
            KeySignal.SetActive(true);

        }
        if (GameManager.Instance != null)
        {
            GameManager.Instance.isEnemy(this);
        }
        currentState = patrolState;
        currentState.EnterState(this);


        /*        if (isBoss)
                {

                    UIManager.instance.SetBossHealth(health);
                }*/
    }
    void hideKey()
    {
        key.SetActive(false);
    }
    // Update is called once per frame
    public virtual void Update()
    {
        /*        if (isBoss)
                    UIManager.instance.UpdateBossHealth(health);

                anim.SetBool("isDead", isDead);
                if (isDead)
                {
                    GameManager.instance.RemoveEnemy(this);
                    return;
                }*/
        if (isDead)
        {
            return;
        }
        currentState.OnUpdate(this);
        anim.SetInteger("state", animState);
        if (attackList.Count > 0)
        {
            TransitionToState(attackState);
        }


    }

    public void TransitionToState(EnemyBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);

    }
    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPoint.position, speed * Time.deltaTime);
        changeDirection();
    }
    public void switchTarget()
    {
        if (Mathf.Abs(target_A.position.x - transform.position.x) > Mathf.Abs(target_B.position.x - transform.position.x))
        {
            TargetPoint = target_A;
        }
        else TargetPoint = target_B;
    }
    public void changeDirection()
    {
        if (transform.position.x > TargetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void AttackAction()
    {

        if (Vector2.Distance(TargetPoint.position, transform.position) < attackRange)
        {
            if (Time.time > nextAttack)
            {

                anim.SetTrigger("Attack");
                nextAttack = attackRate + Time.time;
            }
        }
    }
    public virtual void SkillAction()
    {

        if (Vector2.Distance(TargetPoint.position, transform.position) < skillRange)
        {
            if (Time.time > nextSkill)
            {

                anim.SetTrigger("Skill");
                nextSkill = attackRate + Time.time;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (!attackList.Contains(collision.transform)&&collision.tag!="PlayerAttack"/*&&!GameManager.instance.GameOver&&!hasBomb*/)
        {
            attackList.Add(collision.transform);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isDead)
        {
            if (collision.CompareTag("Player") /*&&!GameManager.instance.GameOver*/)
            {
                StartCoroutine(FindObject());//Hitbox的碰撞体碰到玩家也会触发该语句。
            }
        }

    }
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        { 
            if (hasKey)
            {
                key.SetActive(true);
            }
            gameObject.layer = 2;
            health = 0;
            isDead = true;
            anim.SetBool("isDead", true);

            GameManager.Instance.deadEnemy(GetComponent<Enemy>());

        }
        anim.SetTrigger("Hit");
    }
    IEnumerator FindObject()
    {
        AlarmSign.SetActive(true);
        yield return new WaitForSeconds(AlarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        AlarmSign.SetActive(false);
    }
}
