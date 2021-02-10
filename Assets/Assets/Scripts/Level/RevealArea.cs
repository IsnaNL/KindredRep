using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealArea : MonoBehaviour
{
    bool isRevealed;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        isRevealed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRevealed = true;
            anim.SetBool("Revealed", isRevealed);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRevealed = false;
            anim.SetBool("Revealed", isRevealed);
        }
    }
}
