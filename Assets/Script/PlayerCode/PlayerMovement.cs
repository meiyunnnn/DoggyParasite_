using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    private float horizontal = 0f;
    private bool facingRight = true;

    [SerializeField] private Rigidbody2D rb2d;
    private Animator animator;

    private bool walk;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
        Flip();

        if (animator != null)
        {
            animator.SetBool("Walk", horizontal != 0);
        }
    }

    private void Flip()
    {
        if ((facingRight && horizontal < 0f) || (!facingRight && horizontal > 0f))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    // เรียกจากปุ่ม UI
    public void MoveLeft() => horizontal = -1f;
    public void MoveRight() => horizontal = 1f;
    public void StopMoving() => horizontal = 0f;
}
