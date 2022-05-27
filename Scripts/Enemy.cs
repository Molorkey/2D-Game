using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float maxHealth;
    protected float currentHealth;
    private SpriteRenderer sr;
    public float moveSpeed;

    protected Rigidbody2D rb;

    protected Transform target;
    protected Animator animator;

    public float minDistanceRange, maxDistanceRange;

    public GameObject fartFx;
    public GameObject bloodSpill;
    
    [Header("Attack Properties")]
    [SerializeField] private int damage;
    [SerializeField] private float nextAttackTimer;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRadius;
    private bool canAttack;
    public LayerMask whatIsPlayer;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sr = GetComponentInChildren<SpriteRenderer>();

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("I GOT HIT " + this.gameObject.name);
        currentHealth -= amount;
        Instantiate(bloodSpill, target.transform.position, Quaternion.identity);
        animator.SetTrigger("isHurt");
        if (currentHealth <= 0)
        {
            Instantiate(fartFx, this.transform.position, Quaternion.identity);
            KillCounter kill = FindObjectOfType<KillCounter>();
            kill.killRemaining--;
            Destroy(this.gameObject);
        }
    }

   

    public virtual void Update()
    {
        if (target == null) return;
        FlipX();
        Movement();
        
       
        if (nextAttackTimer <= 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(this.transform.position, attackRadius, whatIsPlayer);
            foreach (var entity in hit)
            {
                IDamageable damageable = entity.GetComponent<IDamageable>();
                damageable.TakeDamage(damage);


            }


            nextAttackTimer = attackCooldown;
        }
        else
        {
            nextAttackTimer -= Time.deltaTime;
        }
    }

    
    private void FlipX()
    {
        
        if (this.transform.position.x < target.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    public virtual void Movement()// PARA SUMUNOD SA PLAYER
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) > minDistanceRange)
            rb.MovePosition(Vector2.MoveTowards(rb.position, target.position, moveSpeed * Time.deltaTime));

    }

    public virtual void Attack()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);

    }
}
