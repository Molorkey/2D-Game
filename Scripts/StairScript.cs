using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D bc2D;
    public int LevelIndex;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bc2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        KillCounter.OnTrigger_Open += ShowObject;
    }

    private void ShowObject()
    {
        sr.enabled = true;
        bc2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("I AM WORKING");
            SceneManager.LoadScene(LevelIndex);

        }
    }
}
