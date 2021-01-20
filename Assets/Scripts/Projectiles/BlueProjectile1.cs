using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueProjectile1 : MonoBehaviour
{
    private Rigidbody2D Shot;

    private bool goBack;

    private float Speed;

    void Awake()
    {
        Shot = gameObject.GetComponent<Rigidbody2D>();

        goBack = false;

        Speed = 7;
    }

    void Update()
    {
        if (goBack == false)
        {
            Shot.position += new Vector2(Speed * Time.deltaTime, 0);
        }
        else
        {
            Shot.position += new Vector2(-Speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IKillable>().Death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Reverse();
        }
    }

    public void Reverse()
    {
        goBack = !goBack;
    }
}
