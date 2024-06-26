using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;

    void Update()
    {
        transform.Rotate(transform.up, 360 * rotationSpeed * Time.deltaTime);
    }
}
