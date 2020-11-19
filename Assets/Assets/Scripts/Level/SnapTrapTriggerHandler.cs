using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTrapTriggerHandler : MonoBehaviour
{
    public int TriggerCount;
    public bool IsTrapReady;
    public bool SnapAttackTrigger;
    public CharacterController2D playerRef;
    public int playerLayer;


    // Start is called before the first frame update
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            TriggerCount++;
            if (TriggerCount == 1)
            {
                IsTrapReady = true;
            }
            if (TriggerCount == 2 && IsTrapReady)
            {
                SnapAttackTrigger = true;
            }
            playerRef = collision.GetComponent<CharacterController2D>();
        }
     
        
    }
}
