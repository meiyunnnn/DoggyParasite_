using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    public WeaponChanger weaponChanger;
    private float nextAttackTime = 0f; // เวลาโจมตีถัดไป
   
    public AudioSource audioSource;

    [Header("Attack Direction")]
    private int attackDirection = 1; // 1 = ขวา, -1 = ซ้าย
    public Transform attackPointRight;
    public Transform attackPointLeft;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time >= nextAttackTime)
            AttackLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time >= nextAttackTime)
            AttackRight();

    }
    public void AttackLeft()
    {
        if (Time.time >= nextAttackTime)
        {
            attackDirection = -1;
            StartCoroutine(Attack());
            audioSource.Play();
        }
    }

    public void AttackRight()
    {
        if (Time.time >= nextAttackTime)
        {
            attackDirection = 1;
            StartCoroutine(Attack());
            audioSource.Play();

        }
    }
    IEnumerator Attack()
    {
        float delay = weaponChanger.GetAttackDelay();
        float cooldown = weaponChanger.GetAttackCooldown();
        float range = weaponChanger.GetAttackRange();
        int damage = weaponChanger.GetCurrentWeaponDamage();

        nextAttackTime = Time.time + cooldown;
        animator.SetTrigger("Atk");

        yield return new WaitForSeconds(delay);

        Transform attackPoint = (attackDirection == 1) ? attackPointRight : attackPointLeft;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                    Debug.Log("✅ โจมตี " + (attackDirection == 1 ? "ขวา" : "ซ้าย"));
                }
            }
        }

    }


    void OnDrawGizmosSelected()
    {
        if (attackPointLeft != null)
            Gizmos.DrawWireSphere(attackPointLeft.position, weaponChanger.GetAttackRange());
        if (attackPointRight != null)
            Gizmos.DrawWireSphere(attackPointRight.position, weaponChanger.GetAttackRange());
    }
}