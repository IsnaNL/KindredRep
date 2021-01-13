using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadowDown : MonoBehaviour
{
    public Transform shadowGFX;
    public LayerMask groundlayer;
    // Start is called before the first frame update

    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, groundlayer);
        float distance = Vector2.Distance(transform.position, hit.point);
        if (hit)
        {
            shadowGFX.position = hit.point;
        }
        if (distance > 2)
        { 
            shadowGFX.gameObject.SetActive(false);
        }
        else if (!shadowGFX.gameObject.activeInHierarchy)
        {
            shadowGFX.gameObject.SetActive(true);
        }
    }
}
