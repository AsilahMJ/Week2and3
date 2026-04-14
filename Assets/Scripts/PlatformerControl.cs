using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int coinsCollected = 0;
    public bool isGrounded = false;
    public float blinkDistance;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private bool isSlow;
    private Animator playerAnim;
    private SpriteRenderer playerSpriteRenderer;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float currentSpeed = isSlow ? speed * 0.25f : speed;
        rb.linearVelocity = new Vector2(moveDir.x * currentSpeed, rb.linearVelocity.y);
    }

    void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        playerAnim.SetBool("isRunning", rb.linearVelocity.x != 0);

        // ✅ Fixed: use moveDir.x, and corrected isFacingRight conditions
        if (moveDir.x < 0 && isFacingRight)
        {
            playerSpriteRenderer.flipX = true;
            isFacingRight = false;
        }
        if (moveDir.x > 0 && !isFacingRight)
        {
            playerSpriteRenderer.flipX = false;
            isFacingRight = true;
        }
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnSlow(InputValue value)
    {
        if (value.isPressed)
        {
            isSlow = !isSlow;
        }
    }

    void OnBlink(InputValue value)
    {
        if (value.isPressed)
        {
            float blinkDir = moveDir.x != 0 ? moveDir.x : (isFacingRight ? 1f : -1f);
            
            transform.position = new Vector2(transform.position.x + blinkDir * blinkDistance, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        isGrounded = true;
    }

    // ✅ Fixed: added missing Collider2D parameter
    void OnTriggerExit2D(Collider2D col)
    {
        isGrounded = false;
    }
}