using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLockX : MonoBehaviour
{
    private Vector2 startPos;

    private void Awake()
    {
        startPos = gameObject.transform.position;
    }

    void Update()
    {
        gameObject.transform.position = new Vector2(startPos.x, gameObject.transform.position.y);
    }
}