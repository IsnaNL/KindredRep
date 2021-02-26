using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointParticalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        EffectsManager.e_Instance.CheckPointEffect(collision.transform.position,collision.transform);
    }
}
