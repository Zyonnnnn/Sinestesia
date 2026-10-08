using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour, IHitable
{
    #region Variables
    [SerializeField] public float moveSpeed, jumpForce, rayLenght, flipSpeed, acc, decc, knockbackStrenght, knockbackDuration;

    [SerializeField] private int health;
    
    private bool canJump, canMove, jumping, flipped, isKnockedBack, canPick, onHand;
    private float knockbackTimer;
    private Vector3 hVelocity;

    public static bool canInteract { get; private set; }
    public static Vector3 playerPosition { get; private set; }

    public static event Action OnPicked;

    [SerializeField] GameObject gc;
    [SerializeField] LayerMask groundtest;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private SinestesyDetection sd;
    private Rigidbody rb;
    private InputManager inputManager;
    GameManager gameManager;

    private Quaternion flipLeft = Quaternion.Euler(0, -180, 0);
    private Quaternion flipRight = Quaternion.Euler(0, 0, 0);

    public event Action OnDie;

    #endregion

    #region Setup
    Color initColor;

    private void Awake()
    {
        inputManager = new InputManager();

        inputManager.OnJumpPressed += HandleJump;
        inputManager.OnSinestesyPressed += HandleSinestesy;
        inputManager.OnPickPressed += HandleInteract;
        inputManager.onShakePressed += HandleShakeOn;
        inputManager.onShakeReleased += HandleShakeOff;

    }

    private void Start()
    {
        sd = GetComponentInChildren<SinestesyDetection>();
        rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        var menuDie = GameObject.FindGameObjectWithTag("DeathM");

        canMove = true;
        initColor = spriteRenderer.material.color;
    }

    public void Execute(Transform executionSoruce, Rigidbody rb, int i)
    {
        if (!isKnockedBack)
        {
            HandleKnockback(executionSoruce);
        }
    }

    #endregion

    #region Loop

    void Update()
    {
        playerPosition = transform.position;

        if (canMove)
        {
            HandleFlip();
        }
        
        Debug.Log("Vida: " + health);
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack && canMove)
        {
            HandleMovement();

            if (jumping)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(new(0, jumpForce, 0), ForceMode.Impulse);
                jumping = false;
            }
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            knockbackTimer -= Time.fixedDeltaTime;

            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
            }
        }

        HandleGroundCheck();
    }

    #endregion

    #region Handlers
    private void HandleMovement()
    {
        var inputDirection = inputManager.GetInputDirection();
        var targetVelocity = new Vector3(inputDirection.x, 0f, inputDirection.y) * (moveSpeed * 100 * Time.deltaTime);
        var speedChangeRate = inputDirection.sqrMagnitude > 0f ? acc : decc;

        hVelocity = Vector3.MoveTowards(hVelocity, targetVelocity, speedChangeRate * Time.fixedDeltaTime);


        rb.linearVelocity = new Vector3(hVelocity.x, rb.linearVelocity.y, hVelocity.z);

        bool isMoving = inputDirection.sqrMagnitude > 0f;
        animator.SetBool("Walk", isMoving);
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

    IEnumerator Die()
    {
        canMove = false;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
    }
    IEnumerator DamageAnim()
    {
        spriteRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.5f);
        spriteRenderer.material.color = initColor;
    }

    void HandleInteract()
    {
        if (canPick)
        {
            canPick = false;
            IHitable hit = GameObject.FindGameObjectWithTag("Lighter").GetComponent<IHitable>();
            hit.Execute(transform, rb, 0);
        }

        rb.AddForce(Vector3.up * 1, ForceMode.Impulse);
        OnPicked?.Invoke();
    }

    private void HandleSinestesy()
    {
        SinestesyShake sinestesyShake = GameObject.FindObjectOfType<SinestesyShake>();
        sinestesyShake.StopShake();

        var ps = sd.GetClosestParticleSystem();

        if (ps != null)
        {
            if (!ps.isEmitting)
            {
                ps.Play();
                animator.SetBool("Sinestesia", true);
                canMove = false;
            }
            else
            {
                ps.Stop();
                animator.SetBool("Sinestesia", false);
                canMove = true;
            }
        }
    }

    private void HandleShakeOn()
    {
        SinestesyShake sinestesyShake = GameObject.FindObjectOfType<SinestesyShake>();
        sinestesyShake.StartShake();
    }
    
    private void HandleShakeOff()
    {
        SinestesyShake sinestesyShake = GameObject.FindObjectOfType<SinestesyShake>();
        sinestesyShake.StopShake();
    }

    private void HandleFlip()
    {
        if (inputManager.GetInputDirection().x != 0)
        {
            if (inputManager.GetInputDirection().x > 0 ? flipped = false : flipped = true) ;
        }

        transform.rotation =
            Quaternion.Slerp(transform.rotation, flipped ? flipLeft : flipRight, flipSpeed * Time.deltaTime);
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
    
    #region Collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EyeJump"))
        {
            TakeDamage(1);
        }
    }

    
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("AreaDamage"))
        {
            TakeDamage(1);
        }

        if (collision.CompareTag("1to2level"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, rb, 1);
        }

        if (collision.CompareTag("2to3level"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, rb, 2);
        }

        if (collision.CompareTag("areaDeath"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, rb, 0);
        }

        if (collision.GetComponent<Collider>().CompareTag("Lighter"))
        {
            canPick = true;
        }

        if (collision.GetComponent<Collider>().CompareTag("text"))
        {
            gameManager.ActivateDialogue();
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("InteractArea"))
        {
            canInteract = true;
        }

        if (collision.CompareTag("GetUpAreaTrigger"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, rb, 1);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("InteractArea"))
        {
            canInteract = false;
        }

        if (collision.CompareTag("GetUpAreaTrigger"))
        {
            IHitable hit = collision.gameObject.GetComponent<IHitable>();
            hit.Execute(transform, rb, 2);
        }

        if (collision.CompareTag("Lighter"))
        {
            canPick = false;
        }
    }

    #endregion

    private void TakeDamage(int damage)
    {
        health -= damage;
        CheckHealth();
    }

    public void EliminatePlayer()
    {
        TakeDamage(health);
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.LogWarning("MORREU! Avisando geral");
            StartCoroutine(Die());
            OnDie?.Invoke();
        }
        else
        {
            StartCoroutine(DamageAnim());
        }
    }

    #region Debug

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(gc.transform.position, Vector3.down * rayLenght);
    }

    #endregion
}