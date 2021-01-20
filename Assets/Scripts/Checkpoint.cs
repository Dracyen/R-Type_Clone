using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool enableCP;

    private void Awake()
    {
        enableCP = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Checkpoint");
        if (collision.gameObject.tag == "Player")
        {
            enableCP = true;
            Debug.Log("Checkpoint Set");
        }
    }
}
