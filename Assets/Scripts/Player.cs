using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private Transform groundChecker;

    [SerializeField]
    private float rotateSpeed = 180f;

    [SerializeField]
    private float normalSpeed = 5f;

    [SerializeField]
    private float dashSpeed = 10f;

    [SerializeField]
    private float jumpSpeed = 10f;

    [SerializeField]
    private float startDelay = 500f;

    [SerializeField]
    private float groundedDistance = 2f;

    [SerializeField]
    private bool isGrounded;

    private float horizontalSpeed;
    private float verticalSpeed;
    private bool isMoving;
    private float remainingStartDelay;
    private bool isGoalReached;

    private CharacterController characterController;
    private Animator animator;

    public event EventHandler GoalReached;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
        {
            return;
        }

        if (remainingStartDelay > 0)
        {
            remainingStartDelay -= Time.deltaTime;
            return;
        }

        Debug.DrawRay(groundChecker.position, Vector3.down * groundedDistance, Color.green);

        RaycastHit hit;
        isGrounded = Physics.Raycast(groundChecker.position, Vector3.down, out hit, groundedDistance);
        if (isGrounded)
        {
            Debug.DrawLine(hit.point - Vector3.right, hit.point + Vector3.right, Color.red);
            Debug.DrawLine(hit.point - Vector3.forward, hit.point + Vector3.forward, Color.red);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        bool isDashing = Input.GetKey(KeyCode.UpArrow);

        horizontalSpeed = normalSpeed;
        if (isDashing)
        {
            horizontalSpeed = dashSpeed;
        }

        if (!isGrounded || verticalSpeed > 0)
        {
            verticalSpeed += gravity * Time.deltaTime;
        }
        else
        {
            verticalSpeed = 0;
        }

        characterController.Move((Vector3.up * verticalSpeed + transform.forward * horizontalSpeed) * Time.deltaTime);

        if (!isGrounded)
        {
            animator.Play("Jumping");
        }
        else if (isDashing)
        {
            animator.Play("Running");
        }
        else
        {
            animator.Play("Walking");
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void StartMoving()
    {
        remainingStartDelay = startDelay;
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
        if (other.GetComponent<Lift>() != null)
        {
            verticalSpeed = jumpSpeed;
        }
        else if (other.GetComponent<Goal>() != null)
        {
            if (!isGoalReached)
            {
                animator.Play("GoalReached");
                NotifyGoalReached();
                isGoalReached = true;
                isMoving = false;
            }
        }
        else if (other.GetComponent<Girder>() != null)
        {
            foreach (Rigidbody rigidbody in other.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.isKinematic = false;
            }
        }
    }

    private void NotifyGoalReached()
    {
        EventHandler handler = GoalReached;
        handler?.Invoke(this, null);
    }
}
