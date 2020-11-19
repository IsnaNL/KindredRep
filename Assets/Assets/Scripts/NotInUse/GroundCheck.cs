using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public LayerMask playerLayerMask;

    internal bool IsGrounded;
    // Start is called before the first frame update
    public void Init()
    {
        IsGrounded = false;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = collision != null &&  (collision.gameObject.layer == groundLayerMask);
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
    }
}
