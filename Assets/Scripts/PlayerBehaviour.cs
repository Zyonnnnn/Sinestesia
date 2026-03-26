using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpForce, sinestesyRange;
    
    bool canJump, jumping;
    
    Rigidbody rb;
    
    InputManager inputManager;
    [SerializeField] GameObject ps;
    
    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.OnJumpPressed += HandleJump;
        inputManager.OnSinestesyPressed += HandleSinestesy;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var moveX = inputManager.GetInputDirection().x * (moveSpeed * 100) * Time.deltaTime;
        var moveZ = inputManager.GetInputDirection().y * (moveSpeed * 100) * Time.deltaTime;

        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, moveZ);
        
        if (jumping)
        {
            Debug.Log("eita preula");
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
        var soundPosition = ps.transform.position;
        var distanceFromSound = Mathf.Abs( Vector3.Distance(soundPosition, transform.position));

        if (distanceFromSound <= sinestesyRange)
        {
            ps.SetActive(true);
        }
        else
        {
            ps.SetActive(false);
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
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sinestesyRange);
    }
}