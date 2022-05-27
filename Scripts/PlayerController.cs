using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour , IDamageable
{
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private HealthSystem healthSystem;
    public GameObject fartFX;
    const int maxHealth = 4;
    public static int currentHealth;

    [Header("Input Properties")]
    private float inputX, inputY;
    private Vector2 moveDirection;

    private bool isInvinsible;
    [SerializeField] private float invinsibleTimer, invinsibleStart;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthSystem.SetHealth(maxHealth);


    }

    private void Update()
    {
        MovementInput();
        bool isFacingRight = WeaponController.isFacingRight;

        sr.flipX = !isFacingRight;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);

        }


        if (isInvinsible)
        {
            invinsibleStart -= Time.deltaTime;
            if (invinsibleStart < 0)
            {
                isInvinsible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();

        if (moveDirection == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);

        }


    }

    private void MovementInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(inputX, inputY).normalized;
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (isInvinsible)
            return;
        isInvinsible = true;
        invinsibleStart = invinsibleTimer;
        currentHealth -= damage;
        healthSystem.UpdateHealth(currentHealth);
        animator.SetTrigger("isHurt");


        if (currentHealth <= 0)
        {
            Instantiate(fartFX, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
