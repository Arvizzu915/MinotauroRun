using UnityEngine;
using UnityEngine.InputSystem;

public class MinotaurMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce, goDownForce, standingColliderSize, crouchingColliderSize;
    [SerializeField] private float minYPos;

    public float currentSpeed, thrustingSpeed, normalSpeed, thrustingAcceleration;

    private bool thrusting = false;

    [SerializeField] private BoxCollider2D minotaurCollider;

    private bool goingDown = false, grounded = true;


    public static MinotaurMovement singleton;

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);

            return;
        }

        singleton = this;
    }

    private void Update()
    {
        if(transform.position.y < minYPos)
        {
            transform.position = new Vector3(transform.position.x, minYPos, transform.position.z);
        }

        if (transform.position.y <= minYPos)
        {
            grounded = true;
        }

        if (grounded)
        {
            Thrust();

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

    private void Thrust()
    {
        if (thrusting)
        {
            Mathf.MoveTowards(currentSpeed, thrustingSpeed, thrustingAcceleration);
        }
        else
        {
            Mathf.MoveTowards(currentSpeed, normalSpeed, thrustingAcceleration);
        }
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
        if (ctx.started && grounded)
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

    public void ThrustInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            thrusting = true;
        }

        if (ctx.canceled)
        {
            thrusting = false;
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            //die
        }

        if (collision.CompareTag("WeakGround"))
        {
            //go underground
        }
    }
}
