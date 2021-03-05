
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instace;
    public List<Benny> BennyList;
    public List<TrapCol> trapsList;
    public CharacterController2D Player;
    public JetSword weaponInit;
    public CameraFollow PlayerFollow;
    public LevelGenerator levelgenerator;
    public Transform curCheckPoint;
    public UIOverlayScript uiOverlay;
    
    //public EffectsManager effects_Instance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 30;
        //QualitySettings.vSyncCount = 0;
        uiOverlay.gameObject.SetActive(true);
        uiOverlay.Init();
        instace = this;
        Player.Init();
        weaponInit = Player.GetComponentInChildren<JetSword>();
        BennyList = new List<Benny>();
        trapsList = new List<TrapCol>();
        BennyList.AddRange(FindObjectsOfType<Benny>());
        trapsList.AddRange(FindObjectsOfType<TrapCol>());
        weaponInit.Init();
        PlayerFollow.Init();
        foreach (Benny b in BennyList)
        {
            b.Init();
            b.player = Player;
          
        }
        foreach(TrapCol t in trapsList)
        {
            t.Init();
        }
       /* if (!Player.gameObject.activeInHierarchy && Player != null)
        {
            StartCoroutine(ReviveCharacter());
        }*/
       

        Time.timeScale = 1f;
    }
   public IEnumerator ReviveCharacter()
    {
        curCheckPoint.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Player.enabled = false;
        Player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Player.transform.position = curCheckPoint.position;
        yield return new WaitForSeconds(1f);
        Player.enabled = true;
        Player.health = 100;
        Player.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Player.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        curCheckPoint.gameObject.SetActive(false);

    }
    //WeaponStruct weaponOne = new WeaponStruct(1,23.5f,"Nadav");
    //Debug.LogError(weaponOne.id + " " + weaponOne.damage +" "+ weaponOne.naming);
    //WeaponStruct WeaponTwo = new WeaponStruct(2, 35.5f, "Vadav");
    //Debug.LogError(WeaponTwo.id + " " + WeaponTwo.damage + " " + WeaponTwo.naming);
    //WeaponStruct weaponThree = new WeaponStruct(3, 60, "Dadav");
    //Debug.LogError(weaponThree.id + " " + weaponThree.damage + " " + weaponThree.naming);
    //effects_Instance.Init();
}
