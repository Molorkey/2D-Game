using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    public static Action OnAttack;

    public Transform aimTransform;
    public static bool isFacingRight;

    [Header("Attack Properties")]
    public float attackRate;
    public float nextAttack;

    public int damage;
    public static int attackDamage;

    private void Start()
    {
        attackDamage = damage;
        

    }
    private void Update()
    {
        WeaponAim();
        if(Time.time >= nextAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnAttack?.Invoke();
                nextAttack = Time.time + 1f / attackRate;
            }
               
        }
      
    }
    private void WeaponAim()
    {
        //kinukuha yung posisyon ng mouse kung saan nakatutok
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // kinukuha yung position tapos minus sa position ng player upang ma get ang direksyon ng sword/wep
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        // ito yung dahilan kung bakit 360 rotation ng sword / para magkaroon ng rotation
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        // applying the angle of wep// nilalagay na yung value ng angle sa trashform
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        //  declared a vector with a value of scale(1,1,1)
        Vector3 localScale = Vector3.one;

        //eto flip ng weapon
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
            isFacingRight = false;
        }
        else
        {
            localScale.y = +1f;
            isFacingRight = true;
        }
        aimTransform.localScale = localScale;
    }
}
