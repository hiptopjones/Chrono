using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform levelParent;

    [SerializeField]
    private GameObject brickPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject goalPrefab;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = new string[]
        {
            "****************",
            " E              ",
            "**********  ****",
            "**********    **",
            "************  **",
            "                ",
            "*****  *********",
            "*****  *********",
            "                ",
            "**********  ****",
            " S              ",
            "****************",
        };

        bool hasStart = false;
        bool hasEnd = false;

        Vector3 spawnPosition = Vector3.zero;
        foreach (string line in lines.Reverse())
        {
            spawnPosition = new Vector3(0, 0, spawnPosition.z + 1);

            foreach (char c in line)
            {
                switch (c)
                {
                    case '*':
                        Instantiate(brickPrefab, spawnPosition, Quaternion.identity, levelParent);
                        break;

                    case ' ':
                        break;

                    case 'S':
                        if (hasStart)
                        {
                            throw new System.Exception("Expected a single start point");
                        }

                        Instantiate(playerPrefab, spawnPosition, Quaternion.identity, levelParent);
                        hasStart = true;
                        break;

                    case 'E':
                        if (hasEnd)
                        {
                            throw new System.Exception("Expected a single end point");
                        }

                        Instantiate(goalPrefab, spawnPosition, Quaternion.identity, levelParent);
                        hasEnd = true;
                        break;

                    default:
                        throw new System.Exception("Unexpected level definition character");
                }

                spawnPosition += Vector3.right;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
