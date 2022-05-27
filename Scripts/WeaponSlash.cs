using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSlash : MonoBehaviour
{
    private Animator animator;
    
    private int attackDamage;

    public Transform weaponCenter;
    private float attackRadius= 0.8f;
    public LayerMask whatIsEnemy;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();

        

    }
    public void Start()
    {
        WeaponController.OnAttack += Attack;
    }

    private void Attack() 
    {
        
        animator.SetTrigger("isAttacking");
    }

    public void Damage()
    {
        Debug.Log("TARGET");
        Collider2D[] hit = Physics2D.OverlapCircleAll(weaponCenter.position, attackRadius, whatIsEnemy);
        foreach (var enemy in hit)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable.TakeDamage(WeaponController.attackDamage);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(weaponCenter.position, attackRadius);
    }
}
