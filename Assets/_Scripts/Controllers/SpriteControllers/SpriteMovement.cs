using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(InputController))]
public class SpriteMovement : MonoBehaviour
{
    /*
     * Requires child "Ground Check" object at feet/bottom of object
     * Create layer named "Ground" and add to groundLayers and objects sprite can jump off of
     * Default paramters: 1 mass, 3 gravity, 12 jump force, 10 speed
     */

    protected Rigidbody2D rb;
    protected InputController inputController;

    protected Transform groundCheck;
    [SerializeField] protected List<LayerMask> groundLayers;

    [SerializeField] protected float jumpForce;

    protected bool isGrounded;

    protected float jumpTimeCounter = 0;

    protected float minJumpTime = 0.08f;
    protected float minJumpTimeCounter = 0;

    protected int extraJumps = 0;
    protected int extraJumpsCounter = 0;

    protected float coyoteTime = 0.06f;
    protected float coyoteTimeCounter = 0;
    protected float jumpBuffer = 0.1f;
    protected float jumpBufferCounter = 0;

    protected Vector2 velocity;

    [SerializeField] protected float speed;

    protected Vector2 additionalForce;
    protected Vector2 additionalForceDecrement;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();

        groundCheck = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        if (GameManager.isGameActive)
        {
            MovementUpdate();
        }
    }
    void Update()
    {
        if (GameManager.isGameActive)
        {
            JumpUpdate();

            if (!isGrounded && rb.velocity.y < 0)
            {
                inputController.isFalling = true;
            }

            additionalForce -= additionalForceDecrement;
        }
    }
    private void MovementUpdate()
    {
        velocity = new Vector3(inputController.horizontalInput, 0) * speed;

        rb.velocity = new Vector2(velocity.x, rb.velocity.y) + additionalForce;
    }
    private void JumpUpdate()
    {
        float dt = Time.deltaTime;

        // Coyote time
        if (isGrounded || extraJumpsCounter < extraJumps)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= dt;
        }

        // Jump buffer
        if (inputController.doJump)
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= dt;
        }

        // Jump
        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0 && (!inputController.isJumping || jumpTimeCounter > 0.01f))
        {
            // Jump counter to prevent multiple jump button forces
            if (inputController.isJumping)
            {
                jumpTimeCounter += dt;
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpBufferCounter = 0;
            if (!isGrounded)
            {
                extraJumpsCounter++;
            }
            isGrounded = false;
            inputController.isJumping = true;
            inputController.isFalling = false;
            //AudioManager.PlaySound("Jump Sound");
            jumpTimeCounter = 0;
            minJumpTimeCounter = 0;
        }
        // Variable jump height
        if (!inputController.isJumpHeld && rb.velocity.y > 0 && minJumpTimeCounter > minJumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.96f);

            coyoteTimeCounter = 0;
        }
        if (inputController.isJumping)
        {
            minJumpTimeCounter += dt;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Grounded
        foreach (LayerMask groundLayer in groundLayers)
        {
            if (1 << collision.gameObject.layer == groundLayer && collision.transform.position.y < groundCheck.position.y + 0.01f)
            {
                isGrounded = true;
                extraJumpsCounter = 0;
                inputController.isJumping = false;
                inputController.isFalling = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ungrounded
        foreach (LayerMask groundLayer in groundLayers)
        {
            if (1 << collision.gameObject.layer == groundLayer)
            {
                isGrounded = false;
            }
        }
    }
}
