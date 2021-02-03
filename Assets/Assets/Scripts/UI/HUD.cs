using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    private CharacterController2D playerRef;
    
    [Header("Refs")]
    public Image[] InventoryItems;
    [SerializeField] private Image fill;
    public GameObject[] BigWeapons;
    [Space]
    [Header("Params")]
    [SerializeField] private float InventoryChangeSpeed;
    [SerializeField] private float FillLerpSpeed;

    private void Start()
    {
        playerRef = FindObjectOfType<CharacterController2D>();
    }
    void Update()
    {
        fill.fillAmount = Mathf.MoveTowards(fill.fillAmount, playerRef.health * 0.01f, Time.deltaTime * FillLerpSpeed);
        for (int i = 0; i < InventoryItems.Length; i++)
        {
                InventoryItems[i].color = new Color(1f, 1f, 1f, Mathf.MoveTowards(InventoryItems[i].color.a,
                    playerRef.inventory.weaponCheck == i ? 1 : 0, Time.deltaTime * InventoryChangeSpeed));
        }
    }
    public void setWeapon(int currWeapon)
    {
        for (int i = 0; i < BigWeapons.Length; i++)
        {
            if (currWeapon == i)
            {
                BigWeapons[i].SetActive(true);
            }
            else
            {
                BigWeapons[i].SetActive(false);
            }
        }
    }
}
