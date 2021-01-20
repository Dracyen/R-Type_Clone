using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, IKillable
{
    private bool enabledCtrl;

    public EnemyProjectile Shot;

    private Vector3 OrgnPos;

    private Transform Target;

    private float timer;

    private bool goUp;

    private float Speed;

    void Awake()
    {
        enabledCtrl = false;

        OrgnPos = gameObject.transform.localPosition;

        Target = FindObjectOfType<PlayerController>().transform;

        timer = 3f;

        goUp = false;
        
        Speed = 0.5f;
    }

    void Update()
    {
        if(enabledCtrl == true)
        {
            Movement();
            Shoot();
        }
    }

    void Movement()
    {
        if(gameObject.transform.localPosition.y < OrgnPos.y - 0.2f)
        {
            goUp = true;
        }

        if (gameObject.transform.localPosition.y > OrgnPos.y + 0.2f)
        {
            goUp = false;
        }

        if(goUp == true)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + Speed * Time.deltaTime, gameObject.transform.localPosition.z);
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - Speed * Time.deltaTime, gameObject.transform.localPosition.z);
        }

        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - (Speed * Time.deltaTime) * 0.5f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }

    void Shoot()
    {
        EnemyProjectile Shot;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Shot = Instantiate(this.Shot, transform.position, this.Shot.transform.rotation);
            Shot.setDirection((Target.position - transform.position).normalized);
            timer = 3f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            triggerCtrl();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            triggerCtrl();
        }
    }

    public void Death(float Damage)
    {
        Debug.Log("I'm Dead");
        Destroy(gameObject);
        //or go to Damage()
    }

    public void triggerCtrl()
    {
        enabledCtrl = !enabledCtrl;
    }
}