using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    CharacterController2D player;
    public float lookUpAmount;
    public float lookDownAmount;
    public float lookSpeed;
    public void Init()
    {
        Debug.Log("caminit");
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponentInParent<CharacterController2D>();
    }
    public void FixedUpdate()
    {
        if (!player.isGrounded)
        {
            return;
        }

        if (player.verInput < 0)
        {
            if (transform.localPosition.x < 0)
            {
                transform.localPosition = Vector3.zero;
            }
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.MoveTowards(transform.localPosition.y, -lookDownAmount, Time.deltaTime* lookSpeed));

        }
        if (player.verInput < 0)
        {
            if (transform.localPosition.x < 0)
            {
                transform.localPosition = Vector3.zero;
            }
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.MoveTowards(transform.localPosition.y, -lookDownAmount, Time.deltaTime * lookSpeed));

        }

        else if(player.verInput > 0)
        {
            if (transform.localPosition.x > 0)
            {
                transform.localPosition = Vector3.zero;
            }
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.MoveTowards(transform.localPosition.y, lookUpAmount, Time.deltaTime * lookSpeed));
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.MoveTowards(transform.localPosition.y, 0, Time.deltaTime * lookSpeed));
        }
    }

}
