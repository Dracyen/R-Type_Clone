using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainProjectile : MonoBehaviour
{
    private Rigidbody2D Shot;

    private float Damage;

    private bool goBack;

    private float Speed;

    void Awake()
    {
        Shot = gameObject.GetComponent<Rigidbody2D>();

        Damage = 0.075f;

        goBack = false;

        Speed = 7;
    }

    void Update()
    {
        if(goBack == false)
        {
            Shot.position += new Vector2(Speed * Time.deltaTime, 0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            Shot.position += new Vector2(-Speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IKillable>().Death();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float Charge)
    {
        if(Charge > Damage)
        Damage = Charge;
    }

    public void Reverse()
    {
        goBack = true;
    }
}
