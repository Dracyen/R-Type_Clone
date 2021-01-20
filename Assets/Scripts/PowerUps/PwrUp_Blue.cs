using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwrUp_Blue : MonoBehaviour
{
    private PwrEnum Power;

    private void Awake()
    {
        Power = PwrEnum.BLUE;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().PowerUp(Power);
            Destroy(gameObject);
        }
    }
}