using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffectHandler : MonoBehaviour
{
    public Transform origin;
    public void HandleStep()
    {
        if (GameManager.instance.Player.isGrounded)
        {
            origin.rotation = Quaternion.Euler(GameManager.instance.Player.velocity.normalized);

            EffectsManager.instance.CreateEffect(effects.alyx_step, origin);
        }
    }
}
