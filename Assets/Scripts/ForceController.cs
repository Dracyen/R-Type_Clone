using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    public PlayerController Player;

    public int PowerLvl;

    private float CCTimer;

    public ForceEnum State;

    public Dictionary<string,Transform> Positions;

    private void Awake()
    {
        PowerLvl = 0;

        CCTimer = 2;

        State = ForceEnum.DISABLED;

        Positions = new Dictionary<string, Transform>();
    }

    private void LateUpdate()
    {
        Movement();
        NoTouchTime();
    }

    void Movement()
    {
        switch(State)
        {
            case ForceEnum.ATTFRONT:
                gameObject.transform.position = Positions["AttFront"].position;
                break;

            case ForceEnum.ATTBACK:
                gameObject.transform.position = Positions["AttBack"].position;
                break;

            case ForceEnum.RETREAT:
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Positions["Behind"].position, 0.02f);
                break;

            case ForceEnum.ADVANCE:
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Positions["Ahead"].position, 0.02f);
                break;

            case ForceEnum.SHTFRONT:
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Positions["ShtFront"].position, 0.1f);

                if (Vector2.Distance(gameObject.transform.position, new Vector2(Positions["ShtFront"].position.x, Positions["ShtFront"].position.y)) < 0.1)
                {
                    State = ForceEnum.ADVANCE;
                }
                break;

            case ForceEnum.SHTBACK:
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Positions["ShtBack"].position, 0.1f);

                if (Vector2.Distance(gameObject.transform.position, new Vector2(Positions["ShtBack"].position.x, Positions["ShtBack"].position.y)) < 0.1)
                {
                    State = ForceEnum.RETREAT;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && CCTimer <= 0)
        {
            if(State == ForceEnum.RETREAT || State == ForceEnum.ADVANCE)
            {
                float DistToFront = Vector2.Distance(gameObject.transform.position, new Vector2(Positions["AttFront"].position.x, Positions["AttFront"].position.y));

                float DistToBack = Vector2.Distance(gameObject.transform.position, new Vector2(Positions["AttBack"].position.x, Positions["AttBack"].position.y));

                if (DistToFront < DistToBack)
                {
                    State = ForceEnum.ATTFRONT;
                }
                else
                {
                    State = ForceEnum.ATTBACK;
                }
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IKillable>().Death();
        }
        else if (collision.gameObject.tag == "EnemyShot")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Shoot()
    {
        GameObject Shot;

        Debug.Log("PowerLevel: " + PowerLvl);
        if(State == ForceEnum.RETREAT || State == ForceEnum.ATTBACK)
        {
            switch (PowerLvl)
            {
                case 1:
                    Shot = Instantiate(Player.Shots[0], gameObject.transform.position, Player.Shots[0].transform.rotation);
                    Shot.GetComponent<MainProjectile>().Reverse();
                    Debug.Log("Normal Shot, Backwards");
                    break;

                case 2:
                    Debug.Log("Slightly Up and Slightly Down Shots, Backwards");
                    break;

                case 3:
                    Debug.Log("Slightly Up and Slightly Down Shots + Upwards and Downwards Shots, Backwards");
                    break;
            }
        }
        else if(State == ForceEnum.ADVANCE)
        {
            switch (PowerLvl)
            {
                case 1:
                    Instantiate(Player.Shots[0], gameObject.transform.position, Player.Shots[0].transform.rotation);
                    Debug.Log("Normal Shot");
                    break;

                case 2:
                    Debug.Log("Slightly Up and Slightly Down Shots");
                    break;

                case 3:
                    Debug.Log("Slightly Up and Slightly Down Shots + Upwards and Downwards Shots");
                    break;
            }
        }
    }

    public void SetVars(PlayerController Player)
    {
        this.Player = Player;

        foreach (Transform item in Player.Positions[0].gameObject.GetComponentsInChildren<Transform>())
        {
            Positions.Add(item.gameObject.name, item);
        }
    }

    private void NoTouchTime()
    {
        if(State != ForceEnum.DISABLED)
        {
            if(CCTimer > 0)
            CCTimer -= Time.deltaTime;
        }
    }
}
