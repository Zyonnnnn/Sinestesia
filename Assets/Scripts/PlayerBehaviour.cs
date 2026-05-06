using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpForce, rayLenght, flipSpeed, acc, decc;

    private bool canJump, jumping, flipped;

    [SerializeField] GameObject gc;
    [SerializeField] LayerMask groundtest;

    private SinestesyDetection sd;
    private Rigidbody rb;
    private InputManager inputManager;

    private Quaternion flipLeft = Quaternion.Euler(0, -180, 0);
    private Quaternion flipRight = Quaternion.Euler(0, 0, 0);
    
    private Vector3 hVelocity;
    
    public static Vector3 playerPosition { get; private set;}

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
        playerPosition = transform.position;
        
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
        if (jumping)
        {
            rb.AddForce(new(0, jumpForce, 0), ForceMode.Impulse);
            jumping = false;
        }

        HandleGroundCheck();
        HandleMovement();
    }

    private void HandleMovement()
    {
        var inputDirection = inputManager.GetInputDirection();
        var targetVelocity = new Vector3(inputDirection.x, 0f, inputDirection.y) * (moveSpeed * 100 * Time.deltaTime);
        var speedChangeRate = inputDirection.sqrMagnitude > 0f ? acc : decc;
        
        hVelocity = Vector3.MoveTowards(hVelocity, targetVelocity, speedChangeRate * Time.deltaTime);
        
        rb.linearVelocity = new Vector3(hVelocity.x, rb.linearVelocity.y, hVelocity.z);
    }

    private void HandleGroundCheck()
    {
        canJump = Physics.Raycast(gc.transform.position, Vector3.down, out _, rayLenght, groundtest) ? true : false;
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