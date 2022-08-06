using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 timeVelocity = new Vector3(1, 0, 0);

    private int timeScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            timeScale = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            timeScale = 1;
        }

        transform.position += Time.deltaTime * timeScale * timeVelocity;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward;
        }
    }
}
