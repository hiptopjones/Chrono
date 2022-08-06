using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float normalSpeed = 5f;

    [SerializeField]
    private float boostSpeed = 10f;

    private float speed;
    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 90);
        }

        speed = normalSpeed;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed = boostSpeed;
        }

        transform.position += Time.deltaTime * transform.forward * speed;
    }

    public void StartRunning()
    {
        isRunning = true;
    }
}
