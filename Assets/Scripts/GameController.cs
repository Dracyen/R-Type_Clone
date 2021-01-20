using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private MainMenu mainMenu;

    private PlayerController Player;

    private MapController Map;

    private bool Loaded;

    private bool Stop;

    private int Lives;

    private GameObject EnemiesParent;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        mainMenu = GameObject.Find("Canvas").GetComponent<MainMenu>();

        Loaded = false;

        Stop = false;

        Lives = 3;
    }

    void newAwake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Map = GameObject.Find("Map").GetComponent<MapController>();
        resetGame();
        Loaded = true;
        Lives -= 1;
    }

    private void Update()
    {
        destroyOnLoad();
    }

    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if(Loaded == false)
            {
                newAwake();
            }

            if(Stop == false)
            {
                playerState();
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        Debug.Log("Lives: " + Lives);
    }

    void playerState()
    {
        if(Player.isDead == true)
        {
            resetGame();
            Debug.Log("Restart");
            SceneManager.LoadScene("Level1");
        }
    }

    void destroyOnLoad()
    {
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject);
        }
    }

    public void resetGame(bool fullReset = false)
    {
        if (Lives > 0)
        {
            Vector3 position = new Vector3();

            if (fullReset == true)
            {
                position = Map.StartPoint;
            }
            else if (Map.Checkpoints[0].enableCP == true)
            {
                if (Map.Checkpoints[1].enableCP == true)
                {
                    if (Map.Checkpoints[2].enableCP == true)
                    {
                        //position = FinalCkp.position;
                    }
                    else
                    {
                        //position = CircleCkp.position;
                    }
                }
                else
                {
                    //position = ShipCkp.position;
                }
            }
            else
            {
                position = Map.StartPoint;
            }

            Map.transform.position = position;

            Loaded = false;
        }
        else
        {
            Debug.Log("GameOver");
            Stop = true;
        }
    }
}
