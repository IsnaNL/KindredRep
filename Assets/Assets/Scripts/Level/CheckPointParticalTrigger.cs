using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointParticalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        EffectsManager.instance.CreateEffect(effects.alyx_checkpoint, GameManager.instance.Player.transform);
    }
}
