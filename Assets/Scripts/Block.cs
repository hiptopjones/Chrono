using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private GameObject debrisPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Block collision with collider: " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.GetComponent<Player>() != null)
        {
            Destroy(Instantiate(debrisPrefab, transform.position, transform.rotation), 5);
            Destroy(this.gameObject);
        }
    }
}
