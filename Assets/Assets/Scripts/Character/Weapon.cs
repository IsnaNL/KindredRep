using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponHandler weaponHandler;
    public float runningCooldown;
    public float Cooldown;
    public Transform weaponCollider;
    public CharacterController2D player;
    protected KeyCode attack = KeyCode.Z;
    protected KeyCode mobilityAbility = KeyCode.X;

    public virtual void Init()
    {
        Debug.Log("weaponinited");
        weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    void Update()
    {
              
      GetInput();
      Attack();
    


    }
    void FixedUpdate()
    {
        MobilityAbility();
    }
    public abstract void Attack();
    public abstract void MobilityAbility();
    public abstract void GetInput();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
    }
    protected virtual IEnumerator TakePlayerControl(float time)
    {
        player.animator.speed = 1;
        player.canMove = false;
        yield return new WaitForSeconds(time);
        player.canMove = true;
    }
}
