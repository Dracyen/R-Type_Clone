﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwrUp_Missiles : MonoBehaviour
{
    private PwrEnum Power;

    private void Awake()
    {
        Power = PwrEnum.MISSILES;
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