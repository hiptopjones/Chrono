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
    private bool isRunning;
    private float remainingStartDelay;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isRunning)
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

        horizontalSpeed = normalSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
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
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public void StartRunning()
    {
        remainingStartDelay = startDelay;
        isRunning = true;
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
            
        }
        else if (other.GetComponent<Girder>() != null)
        {
            foreach (Rigidbody rigidbody in other.GetComponentsInChildren<Rigidbody>())
            {
                rigidbody.isKinematic = false;
            }
        }
    }
}
