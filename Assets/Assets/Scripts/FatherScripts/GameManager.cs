
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    
    public List<Benny> BennyList;
    public List<TrapCol> trapsList;
    public CharacterController2D Player;
    public CameraFollow PlayerCam;
    public LevelGenerator levelgenerator;
  
    //public EffectsManager effects_Instance;
    
    
    // Start is called before the first frame update
    void OnEnable()
    {
        //Application.targetFrameRate = 30;
        //QualitySettings.vSyncCount = 0;

        Player.Init();
        levelgenerator.Init();
        BennyList = new List<Benny>();
        trapsList = new List<TrapCol>();
        BennyList.AddRange(FindObjectsOfType<Benny>());
        trapsList.AddRange(FindObjectsOfType<TrapCol>());
       
        print(BennyList.Count);
        foreach (Benny b in BennyList)
        {
            b.Init();
          
        }
        foreach(TrapCol t in trapsList)
        {
            t.Init();
        }
      
        PlayerCam.Init();

        Time.timeScale = 1f;
    }

        //WeaponStruct weaponOne = new WeaponStruct(1,23.5f,"Nadav");
        //Debug.LogError(weaponOne.id + " " + weaponOne.damage +" "+ weaponOne.naming);
        //WeaponStruct WeaponTwo = new WeaponStruct(2, 35.5f, "Vadav");
        //Debug.LogError(WeaponTwo.id + " " + WeaponTwo.damage + " " + WeaponTwo.naming);
        //WeaponStruct weaponThree = new WeaponStruct(3, 60, "Dadav");
        //Debug.LogError(weaponThree.id + " " + weaponThree.damage + " " + weaponThree.naming);
        //effects_Instance.Init();
}
