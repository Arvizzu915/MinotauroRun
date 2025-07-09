using UnityEngine;
using UnityEngine.InputSystem;

public class MinotaurMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce, goDownForce, standingColliderSize, crouchingColliderSize;

    [SerializeField] private BoxCollider2D minotaurCollider;

    private bool goingDown = false, grounded = true;

    private void Update()
    {
        if (grounded)
        {
            Crouch();
        }
    }

    private void FixedUpdate()
    {
        if (goingDown && !grounded)
        {
            GoDown();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void GoDown()
    {
        rb.AddForce(Vector2.down * goDownForce, ForceMode2D.Force);
    }

    private void Crouch()
    {
        if (goingDown)
        {
            minotaurCollider.size = new Vector2(minotaurCollider.size.x, crouchingColliderSize);
            minotaurCollider.offset = new Vector2(0, crouchingColliderSize / 2);
        }
        else
        {
            minotaurCollider.size = new Vector2(minotaurCollider.size.x, standingColliderSize);
            minotaurCollider.offset = new Vector2(0, 0);
        }
        
    }

    #region Inputs

    public void JumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Jump();
        }
    }

    public void GoDownInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            goingDown = true;
        }

        if (ctx.canceled)
        {
            goingDown = false;
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeakGround"))
        {
            //go underground
        }
    }
}
