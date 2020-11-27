using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //public GameObject Player;
    //[SerializeField]
    //private float xMax;
    //[SerializeField]
    //private float yMax;
    //[SerializeField]
    //private float xMin;
    //[SerializeField]
    //private float yMin;
    //public Transform target;
    //public Texture2D cursorgfx;

   
   public void Init()
    {
        Debug.Log("caminit");
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //Cursor.SetCursor(cursorgfx, Vector3.zero, CursorMode.ForceSoftware);
        //Cursor.visible = true;
    }
    private void Update()
    {
      

    }



}
