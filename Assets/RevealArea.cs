using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealArea : MonoBehaviour
{
    bool isRevealed;
    Sprite[] spritesToReveal;
    void Start()
    {
        isRevealed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Revealcoro());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Hidecoro());
        }
    }


    private IEnumerator Revealcoro()
    {
        yield return null;
    }
    private IEnumerator Hidecoro()
    {
        yield return null;
    }
}
