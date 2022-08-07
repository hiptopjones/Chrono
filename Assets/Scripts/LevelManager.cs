using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform levelParent;

    [SerializeField]
    private Transform blockParent;

    [SerializeField]
    private GameObject blockPrefab;

    [SerializeField]
    private GameObject girderPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject goalPrefab;

    [SerializeField]
    private GameObject liftPrefab;

    private List<Player> players = new List<Player>();
    private int currentPlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines = new string[]
        {
"================================================================",
"================================================================",
"================================================================",
"=============================   ================================",
"============================= G ================================",
"",
"",
"",
"============================= - ================================",
"=============================     ==============================",
"=============================   @ ==============================",
"====================   ======     ==============================",
"==================== @ ======   ================================",
"",
"                                          |                     ",
"",
"=============================   ================================",
"=============================   ================================",
"============================= - ==================   ===========",
"====================   ======   ================== v ===========",
"==================== @ ======   ==================   ===========",
"",
"",
"",
"================================================================",
"================================================================",
"================================================================",
"================================================================",
"================================================================",

//"",
//"",
//"",
//"===========   ===============   ================   =============",
//"=====         ===============   ================   =============",
//"===== >       ===============   ================   =============",
//"=====         ===============   ================   =============",
//"===========   ===============   ================   =============",
//"",
//"",
//"",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"",
//"",
//"",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"===========   ===============   ================   =============",
//"",
//"",
//"",
//"====   ====   ===============   ================   =============",
//"====   ====   ===============   ================       =========",
//"==== ^ ====   ===============   ================     < =========",
//"===========   ===============   ================       ========="
//"===========   ===============   ================   =============",

        };

        bool hasGoal = false;

        Vector3 spawnPosition = Vector3.zero;
        foreach (string line in lines.Reverse())
        {
            spawnPosition = new Vector3(0, 0, spawnPosition.z + 1);

            foreach (char c in line)
            {
                if (c == '=')
                {
                    Instantiate(blockPrefab, spawnPosition, Quaternion.identity, blockParent);
                }
                else if (c == '-' || c == '|')
                {
                    GameObject gameObject = Instantiate(girderPrefab, spawnPosition, Quaternion.identity, levelParent);
                    if (c == '|')
                    {
                        gameObject.transform.Rotate(Vector3.up, 90);
                    }
                }
                else if (c == 'G')
                {
                    if (hasGoal)
                    {
                        throw new System.Exception("Expected a single goal point");
                    }

                    Instantiate(goalPrefab, spawnPosition, Quaternion.identity, levelParent);
                    hasGoal = true;
                }
                else if (c == '>' || c == '<' || c == 'v' || c == '^')
                {
                    GameObject gameObject = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, levelParent);
                    Player player = gameObject.GetComponent<Player>();
                    players.Add(player);

                    if (c == '>')
                    {
                        player.transform.Rotate(Vector3.up, 90);
                    }
                    else if (c == 'v')
                    {
                        player.transform.Rotate(Vector3.up, 180);
                    }
                    else if (c == '<')
                    {
                        player.transform.Rotate(Vector3.up, 270);
                    }
                    else if (c == '^')
                    {
                        // This is the default rotation
                    }
                }
                else if (c == '@')
                {
                    Instantiate(liftPrefab, spawnPosition, Quaternion.identity, levelParent);
                }
                else if (c == ' ')
                {
                    // Empty
                }
                else
                {
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

    public Player GetCurrentPlayer()
    {
        if (currentPlayerIndex < players.Count)
        {
            return players[currentPlayerIndex];
        }

        return null;
    }
}