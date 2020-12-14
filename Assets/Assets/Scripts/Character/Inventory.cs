using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  class Inventory : MonoBehaviour
{
    public int weaponCheck = 0;
    public List<Weapon> weaponList = new List<Weapon>();
    public Pickaxe pickaxe;
    public Shotgun shotgun;
    public JetSword sword;
  //public CharacterController2D player;


    public void Init()
    {
        weaponList.Clear();
        sword = GetComponentInChildren<JetSword>();
        weaponList.Add(sword);
        shotgun = GetComponentInChildren<Shotgun>();
        weaponList.Add(shotgun);
        pickaxe = GetComponentInChildren<Pickaxe>();
        weaponList.Add(pickaxe);
        GetCurrentWeapon(weaponCheck);
       // foreach (Weapon w in weaponList)
   //     {
          //  w.Init();
      //  }
    }

    private void SwapWeapon()
    {
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponCheck--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponCheck++;
        }
        //  if (Input.GetKeyDown(KeyCode.C))
        if (weaponCheck < 0)
        {
            weaponCheck = weaponList.Count-1;
        }
        if (weaponCheck > weaponList.Count-1)
        {
            weaponCheck = 0;
        }
       

      // }
        
            GetCurrentWeapon(weaponCheck);

    }
    void GetCurrentWeapon(int weaponIndex)
    {
      
        
        foreach (Weapon w in weaponList)
        {
            w.enabled = false;
            if (weaponList.IndexOf(w) == weaponIndex)
            {
                w.enabled = true;
            }
          
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            SwapWeapon();
        }
    }
}