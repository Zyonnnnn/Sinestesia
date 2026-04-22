using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpForce, rayLenght, flipSpeed;

    private bool canJump, jumping, flipped;

    [SerializeField] GameObject gc;
    [SerializeField] LayerMask groundtest;

    private SinestesyDetection sd;
    private Rigidbody rb;
    private InputManager inputManager;

    private Quaternion flipLeft = Quaternion.Euler(0, -180, 0);
    private Quaternion flipRight = Quaternion.Euler(0, 0, 0);

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

    void Update()
    {
        if (inputManager.GetInputDirection().x > 0)
        {
            flipped = false;
        } 
        else if (inputManager.GetInputDirection().x < 0)
        {
            flipped = true;
        }

        HandleFlip();
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

        HandleGroundCheck();
    }

    private void HandleGroundCheck()
    {
        if (Physics.Raycast(gc.transform.position, Vector3.down, out _, rayLenght, groundtest))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
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
    
    private void HandleFlip()
    {
        if (flipped)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipLeft, flipSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRight, flipSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gc.transform.position, Vector3.down * rayLenght);
    }
}