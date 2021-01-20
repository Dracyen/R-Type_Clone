using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool enabledCtrl;

    public Rigidbody2D Player;

    public float Speed;

    private float shotPower;

    private float shotPwrMax;

    private bool Missiles;

    public bool isDead;

    private PwrEnum CurrentPwr;

    public ForceController Force;

    public List<PwrUp_Bit> Bits;

    public List<GameObject> Shots;

    public List<Transform> Positions;

    private void Awake()
    {
        enabledCtrl = true;

        Player = gameObject.GetComponent<Rigidbody2D>();

        Speed = 3.5f;

        shotPower = 0;

        shotPwrMax = 1.5f;

        Missiles = false;

        isDead = false;

        CurrentPwr = PwrEnum.NONE;

        Bits = new List<PwrUp_Bit>();

        foreach (Transform item in gameObject.GetComponentsInChildren<Transform>())
        {
            if(item.gameObject.name.Contains("Bit") || item.gameObject.name.Contains("Force") || item.gameObject.name.Contains("Shot"))
            Positions.Add(item);
        }
    }

    void Update()
    {
        //DisabledForce();

        if (enabledCtrl == true)
        {
            Movements();
            Shoot();
        }
    }

    void Movements()
    {
        if (Input.GetKey(KeyCode.W) && Player.position.y < 1.5f)
        {
            Player.position += new Vector2(0, Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && Player.position.y > -1.5f)
        {
            Player.position += new Vector2(0, -Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && Player.position.x < 2.5f)
        {
            Player.position += new Vector2(Speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.A) && Player.position.x > -2.5f)
        {
            Player.position += new Vector2((-Speed * Time.deltaTime) * 0.7f, 0);
        }

        foreach (PwrUp_Bit Bit in Bits)
        {
            Bit.Movement();
        }
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if (shotPower < shotPwrMax)
            {
                shotPower += Time.deltaTime;
            }
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            GameObject Shot = Instantiate(Shots[0], Positions[1].position, Shots[0].transform.rotation);

            Shot.GetComponent<MainProjectile>().SetDamage(shotPower);

            Debug.Log("Pew!! - " + shotPower);

            shotPower = 0;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            GameObject Shot;

            if (Force.State == ForceEnum.ATTFRONT) //Main Power Forward Shoot
            {
                switch (CurrentPwr)
                {
                    case PwrEnum.BLUE:
                        Instantiate(Shots[1], Positions[1].position, Shots[0].transform.rotation);
                        Instantiate(Shots[2], Positions[1].position, Shots[0].transform.rotation);
                        Shot = Instantiate(Shots[2], Positions[1].position, Shots[0].transform.rotation);
                        Shot.GetComponent<BlueProjectile2>().FlipX();
                        Debug.Log("Blue Power");
                        break;

                    case PwrEnum.RED:
                        Debug.Log("Red Power");
                        break;

                    case PwrEnum.YELLOW:
                        Debug.Log("Yellow Power");
                        break;
                }
            }
            else if(Force.State == ForceEnum.ATTBACK) //Main Power Back Shoot
            {
                switch (CurrentPwr)
                {
                    case PwrEnum.BLUE:
                        Instantiate(Shots[1], Positions[2].position, Shots[0].transform.rotation);
                        Shot = Instantiate(Shots[2], Positions[2].position, Shots[0].transform.rotation);
                        Shot.GetComponent<BlueProjectile2>().FlipY();
                        Shot = Instantiate(Shots[2], Positions[1].position, Shots[0].transform.rotation);
                        Shot.GetComponent<BlueProjectile2>().FlipY();
                        Shot.GetComponent<BlueProjectile2>().FlipX();
                        break;

                    case PwrEnum.RED:
                        Debug.Log("Red Power");
                        break;

                    case PwrEnum.YELLOW:
                        Debug.Log("Yellow Power");
                        break;
                }
            }

            if (Missiles == true) //Shoot Missiles
            {
                //Missile Spawn
            }

            if (CurrentPwr == PwrEnum.RED) //Bit Shoot
            {
                float DistToFront = Vector2.Distance(Force.transform.position, new Vector2(Positions[1].transform.position.x, Positions[1].transform.position.y));

                float DistToBack = Vector2.Distance(Force.transform.position, new Vector2(Positions[2].transform.position.x, Positions[2].transform.position.y));

                Debug.Log("Distance to Force: " + DistToFront + ", " + DistToBack);

                if(DistToFront > DistToBack)
                {
                    foreach (PwrUp_Bit Bit in Bits)
                    {
                        Bit.ShootBack();
                    }
                }
                else
                {
                    foreach (PwrUp_Bit Bit in Bits)
                    {
                        Bit.ShootFront();
                    }
                }
            }

            Force.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if(Force.State != ForceEnum.DISABLED)
            {
                if (Force.State == ForceEnum.ADVANCE)
                {
                    Force.State = ForceEnum.RETREAT;
                }
                else if(Force.State == ForceEnum.RETREAT)
                {
                    Force.State = ForceEnum.ADVANCE;
                }
                else if (Force.State == ForceEnum.ATTFRONT)
                {
                    Force.State = ForceEnum.SHTFRONT;
                }
                else if (Force.State == ForceEnum.ATTBACK)
                {
                    Force.State = ForceEnum.SHTBACK;
                }
            }
        }
    }

    public void Death()
    {
        isDead = true;
        Debug.Log("THE PLAYER IS DEAD!!");
    }

    void DisabledForce()
    {
        if(Force.State == ForceEnum.DISABLED)
        {
            Force.transform.position = new Vector3(Player.position.x - 3.5f, Player.position.y, Force.transform.position.z);
        }
    }

    public void PowerUp(PwrEnum Power)
    {
        switch (Power)
        {
            case PwrEnum.SPEED:
                Speed += 0.1f;
                break;

            case PwrEnum.MISSILES:
                Missiles = true;
                break;

            case PwrEnum.BIT:
                break;

            default:
                if (Force.PowerLvl < 1)
                {
                    Force.PowerLvl += 1;
                    Force.SetVars(this);
                    Force.State = ForceEnum.ADVANCE;
                }
                else
                {
                    CurrentPwr = Power;

                    if (Force.PowerLvl < 3)
                    {
                        Force.PowerLvl += 1;
                    }
                }
                break;
        }

        Debug.Log("Power" + CurrentPwr + " - PowerLevel: " + Force.PowerLvl);
    }

    public void triggerCtrl()
    {
        enabledCtrl = !enabledCtrl;
    }
}