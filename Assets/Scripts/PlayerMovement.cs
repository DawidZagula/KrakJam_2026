using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    [SerializeField] private InputActionReference jumpAction;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    private bool jumpHeld = false;

    private void OnEnable()
    {
        jumpAction.action.Enable();
        jumpAction.action.started += OnJumpStarted;
        jumpAction.action.canceled += OnJumpCanceled;
    }

    private void OnDisable()
    {
        jumpAction.action.started -= OnJumpStarted;
        jumpAction.action.canceled -= OnJumpCanceled;
        jumpAction.action.Disable();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        if (isGrounded && !jumpHeld)
        {
            isJumping = false;
            jumpTimer = 0f;
        }

        if (isJumping && jumpHeld)
        {
            if (jumpTimer < jumpTime)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    private void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        jumpHeld = true;

        if (!isGrounded) return;

        isJumping = true;
        jumpTimer = 0f;

        rb.linearVelocity = Vector2.up * jumpForce;
    }

    private void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        jumpHeld = false;
        isJumping = false;
        jumpTimer = 0f;
    }
}
