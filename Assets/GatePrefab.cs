using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GatePrefab : MonoBehaviour
{
    private enum GateMode
    {
        OnTriggerEnter, OnTriggerExit, OnLivebodyKilled
    }
    private Animator myAnimator;
    [SerializeField]private bool IsOpen;
    private bool executeLiveBodyCheck;
    [SerializeField] private GateMode OpenMode;
    [SerializeField] private GateMode CloseMode;
    public GameObject liveBodyRef;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        executeLiveBodyCheck = liveBodyRef != null && 
            (OpenMode == GateMode.OnLivebodyKilled || CloseMode == GateMode.OnLivebodyKilled);

        myAnimator.SetBool("IsOpen", IsOpen);
    }
    private void Update()
    {
        if (executeLiveBodyCheck)
        {
            if (!liveBodyRef.activeInHierarchy)
            {
                TriggerMode(GateMode.OnLivebodyKilled);
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerMode(GateMode.OnTriggerEnter);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerMode(GateMode.OnTriggerExit);
        }
    }
    private void Open()
    {
        if (IsOpen)
        {
            return;
        }
        IsOpen = true;
        myAnimator.SetBool("IsOpen", IsOpen);
    }
    private void Close()
    {
        if (!IsOpen)
        {
            return;
        }
        IsOpen = false;
        myAnimator.SetBool("IsOpen", IsOpen);
    }
    private void TriggerMode(GateMode Context)
    {
        if (OpenMode == Context)
        {
            Open();
        }
        else if (CloseMode == Context)
        {
            Close();
        }
    }
}
