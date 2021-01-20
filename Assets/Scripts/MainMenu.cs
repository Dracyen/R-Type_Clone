using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PushStart;

    public GameObject Credits;

    private bool turnOff;

    private void Awake()
    {
        turnOff = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (turnOff == false)
            {
                Reverse();
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level1");
        }
    }

    void Reverse()
    {
        GameObject current;

        foreach(Image item in PushStart.GetComponentsInChildren<Image>(true))
        {
            current = item.gameObject;
            current.SetActive(!current.activeSelf);
        }

        foreach (Image item in Credits.GetComponentsInChildren<Image>(true))
        {
            current = item.gameObject;
            current.SetActive(!current.activeSelf);
        }

        turnOff = true;
    }
}
