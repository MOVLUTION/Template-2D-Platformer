using UnityEngine;

namespace Movlution.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 200;
        [SerializeField] float jumpForce = 300;

        //reference component
        Rigidbody2D rb;
        Animator animator;
        SpriteRenderer spriteRenderer;
        Collider2D col;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            col = GetComponent<Collider2D>();
        }

        private void Update()
        {
            Movement(Input.GetAxis("Horizontal"));
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                Jump();
            }
            Fall();
        }
        void Movement(float direction)
        {
            if (direction != 0 && IsGrounded())
            {
                rb.velocity = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, 0);
            }
            else if (direction != 0 && !IsGrounded())
            {
                rb.velocity = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            }
            animator.SetBool("running", true);
            if (direction > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                animator.SetBool("running", false);
            }
        }
        void Jump()
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            animator.Play("jump");
        }
        void Fall()
        {
            if (rb.velocity.y < -0.15f && !IsGrounded())
            {
                animator.Play("fall");
            }
        }
        private bool IsGrounded()
        {
            Vector2 size = new Vector2(col.bounds.size.x / 5, col.bounds.size.y);
            return Physics2D.BoxCast(col.bounds.center, size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        }
    }
}
