using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwrUp_Bit : MonoBehaviour
{
    private PlayerController Player;

    private Transform Anchor;

    private bool Caught;

    private float Xmax;

    private float Ymax;

    public GameObject Shot;

    private void Awake()
    {
        Caught = false;

        Xmax = 0.15f;

        Ymax = 0.05f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Caught == false)
        {
            Player = collision.gameObject.GetComponent<PlayerController>();

            if (Player.Bits.Count < 2)
            {
                if (Player.Bits.Count == 0)
                {
                    Player.Bits.Add(this);
                    foreach(Transform Anchor in Player.Positions)
                    {
                        if (Anchor.gameObject.name == "BitPos1")
                            this.Anchor = Anchor;
                    }

                }
                else
                {
                    Player.Bits.Add(this);
                    foreach (Transform Anchor in Player.Positions)
                    {
                        if (Anchor.gameObject.name == "BitPos2")
                            this.Anchor = Anchor;
                    }
                }
            }

            Caught = true;    
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IKillable>().Death();
        }
        else if (collision.gameObject.tag == "EnemyShot")
        {
            Destroy(collision.gameObject);
        }
    }

    public void Movement()
    {
        if(gameObject.transform.position.x > Anchor.position.x + Xmax) //Limit distance to the Right
        {
            gameObject.transform.position = new Vector2(Anchor.position.x + Xmax, gameObject.transform.position.y);
        }

        if (gameObject.transform.position.x < Anchor.position.x - Xmax) //Limit distance to the Left
        {
            gameObject.transform.position = new Vector2(Anchor.position.x - Xmax, gameObject.transform.position.y);
        }

        if (gameObject.transform.position.y > Anchor.position.y + Ymax) //Limit distance Upwards
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, Anchor.position.y + Ymax);
        }

        if (gameObject.transform.position.y < Anchor.position.y - Ymax) //Limit distance Downwards
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, Anchor.position.y - Ymax);
        }
    }

    public void ShootFront()
    {
        GameObject Shot;

        Shot = Instantiate(this.Shot, gameObject.transform.position, this.Shot.transform.rotation);
    }

    public void ShootBack()
    {
        GameObject Shot;

        Shot = Instantiate(this.Shot, gameObject.transform.position, this.Shot.transform.rotation);

        Shot.GetComponent<MainProjectile>().Reverse();
    }
}
