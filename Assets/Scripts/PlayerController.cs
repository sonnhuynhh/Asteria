using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform visual;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius = 0.12f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpQueued;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ReadInput();

        isGrounded = groundCheck != null &&
            Physics2D.OverlapCircle(
                groundCheck.position,
                groundRadius,
                groundLayer
            );

        if (moveInput != 0f && visual != null)
        {
            Vector3 scale = visual.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveInput);
            visual.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput * moveSpeed;

        if (jumpQueued && isGrounded)
        {
            velocity.y = jumpForce;
        }

        rb.linearVelocity = velocity;
        jumpQueued = false;
    }

    private void ReadInput()
    {
        moveInput = 0f;

        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            moveInput -= 1f;

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            moveInput += 1f;

        if (keyboard.spaceKey.wasPressedThisFrame)
            jumpQueued = true;
    }
}