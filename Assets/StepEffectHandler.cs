using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffectHandler : MonoBehaviour
{
    public Transform origin;
    public void HandleStep()
    {
        if (!GameManager.instance.Player.isGrounded)
        {
            return;
        }
        EffectsManager EMRef = EffectsManager.e_Instance;
        Instantiate(EMRef.StepEffect, origin.position, Quaternion.Euler(GameManager.instance.Player.rb.velocity));
        EMRef.CreateEffect(origin.position, EMRef.StepEffect, EMRef.transform, !GameManager.instance.Player.islookingright);
    }
}
