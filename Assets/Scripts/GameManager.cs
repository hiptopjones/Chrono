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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            player.StartRunning();
        }
    }
}
