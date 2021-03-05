using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GatePrefab : MonoBehaviour
{
    public enum GateMode
    {
        OnTriggerEnter, OnTriggerExit, OnLivebodyKilled
    }
    private Animator myAnimator;
    [SerializeField]private bool IsOpen;
    private bool executeLiveBodyCheck;
    public GateMode OpenCondition;
    public bool DoesRequireAllToOpenTheGate;
    public GateMode CloseCondition;
    public bool DoesRequireAllToCloseTheGate;
    public GameObject liveBodyRef;
    void Start()
    {
        IsOpen = true;
        myAnimator = GetComponent<Animator>();
        executeLiveBodyCheck = liveBodyRef != null && 
            (OpenCondition == GateMode.OnLivebodyKilled || CloseCondition == GateMode.OnLivebodyKilled);

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
        if (OpenCondition == Context)
        {
            Open();
        }
        if (CloseCondition == Context)
        {
            Close();
        }
    }
}
