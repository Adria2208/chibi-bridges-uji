using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private LayerMask groundLayer;
    private float feetCollision = 0.05f;

    private InputAction moveAction;
    private InputAction jumpAction;
    public Vector2 moveValue;
    public bool isJumping;

    public float moveSpeed = 6f; 
    public float jumpForce = 6f;
    public bool isGrounded { get; private set; }
    public Rigidbody2D rb { get; private set; }
    private BoxCollider2D bodyCollider;
    private Vector2 bodySize;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider =  GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction = InputSystem.actions.FindAction("Jump");

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
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(moveValue.x * moveSpeed, rb.linearVelocityY);
    }
    public void Jump()
    {
        rb.linearVelocity += new Vector2(rb.linearVelocityX, rb.linearVelocityY + jumpForce);
    }
    public void Stop()
    {
        rb.linearVelocityX = 0;
    }
}