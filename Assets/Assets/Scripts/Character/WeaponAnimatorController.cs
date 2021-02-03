using System.Collections.Generic;
using UnityEngine;


public class WeaponAnimatorController : MonoBehaviour
{
    public List<RuntimeAnimatorController> weaponAnimationControllers;
    public List<Sprite> DefaultSpriteRendererSprites;
    public CharacterController2D player;
    private SpriteRenderer sr;
    public void Init()
    {
      
        sr = GetComponent<SpriteRenderer>();
    }
   public void SetWeapon(int weaponNum)
    {
        player.animator.runtimeAnimatorController = weaponAnimationControllers[weaponNum];
        sr.sprite = DefaultSpriteRendererSprites[weaponNum];
    }

  
}
