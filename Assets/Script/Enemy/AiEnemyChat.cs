using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemyChat : MonoBehaviour
{
        public float speed = 2f;
        public float attackRange = 1.5f;
        public float attackCooldown = 1f;
        public int damage = 10;
    public float attackDelay = 0.5f;
    

    private Transform player;
        private bool canAttack = true;
        private Rigidbody2D rb;
        private bool facingRight = true;
        private Animator animator;



    void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

        if (player == null)
        {
            Debug.LogError("Player ไม่ถูกพบ! ตรวจสอบว่า Player มี Tag เป็น 'Player'");
        }
        }

        void Update()
        {
        if (player == null)
        {
            Debug.LogError("❌ ไม่พบ Player! ตรวจสอบว่า Player มี Tag เป็น 'Player'");
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    void MoveTowardsPlayer()
        {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (player.position.x > transform.position.x && facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && !facingRight)
        {
            Flip();
        }
    }

        IEnumerator Attack()
        {
        canAttack = false;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Atk");
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("🗡 Enemy Attacks Player!");

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("✅ Player ได้รับดาเมจ: " + damage);
        }
        else
        {
            Debug.LogError("❌ ไม่พบ PlayerHealth บน Player");
        }

        yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1); // ป้องกันค่า x ผิดพลาด
        transform.localScale = scale;
    }
}
