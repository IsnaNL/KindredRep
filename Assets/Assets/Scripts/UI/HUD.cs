using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    public Sprite[] heartSprites;
    public Sprite[] weaponSprites;
    public Image HeartUI;
    public Image WeaponUI;
    public CharacterController2D player;
    public Weapon weapon;
    private int playerHealthUITranslation;
    private int weaponNumberUiTranslation;
    public Slider hpSlider;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
     
        if(player.health >= 80)
        {
            playerHealthUITranslation = 0;
        }else if(player.health >= 60)
        {
            playerHealthUITranslation = 1;
        }else if (player.health >= 40)
        {
            playerHealthUITranslation = 2;
        }else if (player.health >= 20)
        {
            playerHealthUITranslation = 3;
        }else
        {
            playerHealthUITranslation = 4;
            SceneManager.LoadScene(0);
        }
        if (weapon.weaponCheck == 0)
        {
            weaponNumberUiTranslation = 0;
        }else if (weapon.weaponCheck == 1)
        {
            weaponNumberUiTranslation = 1;
        }else if (weapon.weaponCheck == 2)
        {
            weaponNumberUiTranslation = 2;
        }
        WeaponUI.sprite = weaponSprites[weaponNumberUiTranslation];
        HeartUI.sprite = heartSprites[playerHealthUITranslation];
        hpSlider.value = player.health;
    }
}
