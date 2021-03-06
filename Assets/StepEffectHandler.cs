using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEffectHandler : MonoBehaviour
{
    public Transform origin;
    public void StepEffectHandle()
    {
        EffectsManager reference = EffectsManager.e_Instance;
        reference.CreateEffect(origin.position, reference.StepEffect, reference.transform, !GameManager.instace.Player.islookingright);
    }
}
