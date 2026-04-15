using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpForce;

    private bool canJump, jumping;

    private Rigidbody rb;
    private InputManager inputManager;
    private SinestesyDetection sd;

    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.OnJumpPressed += HandleJump;
        inputManager.OnSinestesyPressed += HandleSinestesy;
    }

    private void Start()
    {
        sd = GetComponentInChildren<SinestesyDetection>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var moveX = inputManager.GetInputDirection().x * (moveSpeed * 100) * Time.deltaTime;
        var moveZ = inputManager.GetInputDirection().y * (moveSpeed * 100) * Time.deltaTime;

        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, moveZ);

        if (jumping)
        {
            rb.AddForce(new(0, jumpForce, 0), ForceMode.Impulse);
            jumping = false;
        }
    }

    void HandleJump()
    {
        if (canJump)
        {
            jumping = true;
            canJump = false;
        }
    }

    private void HandleSinestesy()
    {
        var ps = sd.GetClosestParticleSystem();

        if (ps != null)
        {
            if (!ps.isEmitting)
            {
                ps.Play();
            }
            else
            {
                ps.Stop();
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}