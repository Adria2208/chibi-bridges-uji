using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private float feetCollision = 0.05f;

    private InputAction moveAction;
    private InputAction jumpAction;
    public Vector2 moveValue;
    public bool isJumping;


    [SerializeField] private float maxSpeed;

    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fastFallMultiplier;
    [SerializeField] private float originalGravityScale;

    [SerializeField] private double debugCurrentSpeedX;
    [SerializeField] private double debugCurrentSpeedY;


    public bool isGrounded { get; private set; }
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private CapsuleCollider2D bodyCollider;
    private Vector2 bodySize;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        originalGravityScale = rb.gravityScale;

        bodySize = bodyCollider.bounds.size;
        bodySize.y -= feetCollision;
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        isJumping = jumpAction.WasPressedThisFrame();

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            bodyCollider.bounds.center,
            bodySize,
            0f, // Angle
            Vector2.down, // Direction
            feetCollision,
            groundLayer
        );

        isGrounded = hit.collider != null;


        if (rb.linearVelocityY < 0) // Is falling
            rb.gravityScale = originalGravityScale * fastFallMultiplier;
        else
            rb.gravityScale = originalGravityScale;

        debugCurrentSpeedX = Math.Truncate(100 * rb.linearVelocityX) / 100;
        debugCurrentSpeedY = Math.Truncate(100 * rb.linearVelocityY) / 100;
    }

    public void Move()
    {
        float targetSpeed = moveValue.x * maxSpeed;
        float speedDifference = targetSpeed - rb.linearVelocityX;

        // Use acceleration when input exists, deceleration when stopping
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = accelRate * speedDifference * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(
            rb.linearVelocityX + movement,
            rb.linearVelocityY
        );
    }
    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

}