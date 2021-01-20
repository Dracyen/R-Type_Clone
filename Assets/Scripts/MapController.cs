using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    private float Speed;

    public Vector3 StartPoint { get; private set; }

    public List<Checkpoint> Checkpoints { get; private set; }

    private List<SpriteRenderer> Backgrounds;

    private void Awake()
    {
        Speed = 0.5f;

        Checkpoints = new List<Checkpoint>();

        StartPoint = new Vector3(45, 0, 0);

        Backgrounds = new List<SpriteRenderer>();

        foreach (Checkpoint item in gameObject.GetComponentsInChildren<Checkpoint>())
        {
            Debug.Log(item.gameObject.name);
            Checkpoints.Add(item);
        }

        foreach (SpriteRenderer item in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (item.gameObject.name.Contains("BG_"))
                Backgrounds.Add(item);
                Debug.Log(item.gameObject.name);
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - Speed * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void reverseBckgnd()
    {
        if(Checkpoints[0].enableCP == true)
        {
            if (Backgrounds[0].color.a > 0)
            {
                Backgrounds[0].color = new Color(Backgrounds[0].color.r, Backgrounds[0].color.g, Backgrounds[0].color.b, Backgrounds[0].color.a - Time.deltaTime * 0.5f);
                Backgrounds[1].color = new Color(Backgrounds[1].color.r, Backgrounds[1].color.g, Backgrounds[1].color.b, Backgrounds[1].color.a + Time.deltaTime * 0.5f);
            }
        }
    }
}
