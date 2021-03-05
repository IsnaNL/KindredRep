using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointParticalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        EffectsManager EMRef = EffectsManager.e_Instance;
        EMRef.CreateEffect(collision.transform.position, EMRef.checkPointEffect, collision.transform);
    }
}
