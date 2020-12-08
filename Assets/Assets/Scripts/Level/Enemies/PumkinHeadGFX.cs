using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinHeadGFX : MonoBehaviour
{
    public CharacterController2D player;
    public SpriteRenderer sr;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            sr.flipX = true;
           

        }
        else
        {
            sr.flipX = false;
           



        }
    }
}
