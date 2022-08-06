using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private SmoothFollow smoothFollow;

    private Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                return;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        Player player = levelManager.GetCurrentPlayer();
        if (player == null)
        {
            return;
        }

        if (player != currentPlayer)
        {
            currentPlayer = player;
            smoothFollow.SetTarget(currentPlayer.gameObject);
        }

        if (!currentPlayer.IsRunning())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentPlayer.StartRunning();
            }
        }
    }
}
