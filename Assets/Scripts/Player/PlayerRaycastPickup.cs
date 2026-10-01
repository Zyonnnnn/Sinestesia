using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycastPickup : MonoBehaviour
{
    GameManager gameManager;
    InputManager inputManager;

    PlayerBehaviour playerBehaviour;
    ObjectGrabbable newObjectGrabbable;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float baseDistanceX;

    [SerializeField] Transform targetGrab;
    Animator animator;

    private bool picked;
    Vector2 inputDirection;

    Vector3 baseDistance;
    Vector3 lastValidDirection = Vector3.right;

    private void Awake()
    {
        inputManager = new InputManager();
        playerBehaviour = GetComponent<PlayerBehaviour>();

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerBehaviour.OnPicked += HandleRaycast;
        baseDistance = Vector3.zero;
    }

    private void Update()
    {
        if (!picked)
        {
            animator.SetBool("isDrag", false);

        }

        inputDirection = inputManager.GetInputDirection();

        if (inputDirection.sqrMagnitude >= 0.01f)
        {
            Vector3 direction = new Vector3(inputDirection.x, 0f, inputDirection.y);
            direction.Normalize();
            lastValidDirection = direction;
        }
    }

    private void HandleRaycast()
    {
        Vector3 direction = lastValidDirection;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, baseDistanceX, layerMask))
        {
            if (picked == false)
            {
                if (hit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
                {
                    animator.SetBool("isDrag", true);
                    newObjectGrabbable = objectGrabbable;
                    objectGrabbable.Grab(targetGrab);
                    picked = true;
                }
            }
            else
            {
                picked = false;

                newObjectGrabbable.Release();
                newObjectGrabbable = null;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, lastValidDirection * baseDistanceX);
    }
}