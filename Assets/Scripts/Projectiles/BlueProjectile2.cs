using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectile2 : MonoBehaviour
{
    private Rigidbody2D Shot;

    private float Speed;

    private float Damage;

    private BluePwr Direction;

    private void Awake()
    {
        Shot = gameObject.GetComponent<Rigidbody2D>();

        Speed = 1f;

        Damage = 0.075f;

        Direction = BluePwr.SOUTHEAST;
    }

    void Update()
    {
        Movement();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            float DistY = Mathf.Abs((collision.GetContact(0).point - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).y);
            float DistX = Mathf.Abs((collision.GetContact(0).point - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).x);

            if (DistY > DistX)
            {
                FlipX();
            }
            else
            {
                FlipY();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IKillable>().Death(Damage);
        }
    }

    private void Movement()
    {
        switch (Direction)
        {
            case BluePwr.NORTHEAST:
                Shot.position += new Vector2(Speed * Time.deltaTime, Speed * Time.deltaTime); //  North-East
                break;

            case BluePwr.NORTHWEST:
                Shot.position += new Vector2(-Speed * Time.deltaTime, Speed * Time.deltaTime); //  North-West
                break;

            case BluePwr.SOUTHEAST:
                Shot.position += new Vector2(Speed * Time.deltaTime, -Speed * Time.deltaTime); //  South-East
                break;

            case BluePwr.SOUTHWEST:
                Shot.position += new Vector2(-Speed * Time.deltaTime, -Speed * Time.deltaTime); //  South-West
                break;
        }
    }
    public void FlipX()
    {
        switch (Direction)
        {
            case BluePwr.NORTHEAST:
                Direction = BluePwr.SOUTHEAST;
                break;

            case BluePwr.NORTHWEST:
                Direction = BluePwr.SOUTHWEST;
                break;

            case BluePwr.SOUTHEAST:
                Direction = BluePwr.NORTHEAST;
                break;

            case BluePwr.SOUTHWEST:
                Direction = BluePwr.NORTHWEST;
                break;
        }
        Debug.Log("Hit Ground or Cealing");
    }

    public void FlipY()
    {
        switch (Direction)
        {
            case BluePwr.NORTHEAST:
                Shot.transform.Rotate(new Vector3(0, 0, 90));
                Shot.transform.position = new Vector3(Shot.transform.position.x, Shot.transform.position.y + 0.32f, Shot.transform.position.z);
                Direction = BluePwr.NORTHWEST;
                break;

            case BluePwr.NORTHWEST:
                Shot.transform.Rotate(new Vector3(0, 0, 90));
                Shot.transform.position = new Vector3(Shot.transform.position.x, Shot.transform.position.y + 0.32f, Shot.transform.position.z);
                Direction = BluePwr.NORTHEAST;
                break;

            case BluePwr.SOUTHEAST:
                Shot.transform.Rotate(new Vector3(0, 0, 90));
                Shot.transform.position = new Vector3(Shot.transform.position.x, Shot.transform.position.y - 0.32f, Shot.transform.position.z);
                Direction = BluePwr.SOUTHWEST;
                break;

            case BluePwr.SOUTHWEST:
                Shot.transform.Rotate(new Vector3(0, 0, 90));
                Shot.transform.position = new Vector3(Shot.transform.position.x, Shot.transform.position.y - 0.32f, Shot.transform.position.z);
                Direction = BluePwr.SOUTHEAST;
                break;
        }
        Debug.Log("Hit Walls");
    }
}
