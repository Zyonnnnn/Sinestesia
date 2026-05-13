using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour, IHitable
{
    [SerializeField] float moveSpeed, jumpForce, rayLenght, flipSpeed, acc, decc, health, knockbackStrenght, knockbackDuration;

    private bool canJump, jumping, flipped, isKnockedBack;

    private float knockbackTimer;

    [SerializeField] GameObject gc;
    [SerializeField] LayerMask groundtest;

    private SinestesyDetection sd;
    private Rigidbody rb;
    private InputManager inputManager;

    private Quaternion flipLeft = Quaternion.Euler(0, -180, 0);
    private Quaternion flipRight = Quaternion.Euler(0, 0, 0);

    private Vector3 hVelocity;

    public static Vector3 playerPosition { get; private set; }


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

        HandleFlip();
        HandleHealth();
    }


    private void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            HandleMovement();

            if (jumping)
            {
                rb.AddForce(new(0, jumpForce, 0), ForceMode.Impulse);
                jumping = false;
            }
        }
        else
        {
            knockbackTimer -= Time.fixedDeltaTime;

            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
        }

        HandleGroundCheck();
    }

    public void Execute(Transform executionSoruce)
    {
        if (!isKnockedBack)
        {
            HandleKnockback(executionSoruce);
        }
    }

    #region Handlers
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

    void HandleHealth()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("StartScene");
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
        if (inputManager.GetInputDirection().x != 0)
        {
            if (inputManager.GetInputDirection().x > 0 ? flipped = false : flipped = true) ;
        }

        if (flipped)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipLeft, flipSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, flipRight, flipSpeed * Time.deltaTime);
        }
    }

    private void HandleKnockback(Transform executionSoruce)
    {
        if (isKnockedBack)
            return;

        Vector3 dir = (transform.position - executionSoruce.position).normalized;
        dir.y = -dir.y;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(dir * knockbackStrenght, ForceMode.Impulse);

        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EyeJump"))
        {
            health--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gc.transform.position, Vector3.down * rayLenght);
    }
}