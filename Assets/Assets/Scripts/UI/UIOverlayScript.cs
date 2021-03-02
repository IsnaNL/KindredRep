using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIOverlayScript : MonoBehaviour
{
    public const float PressAnimTime = .5f;
    public const float MenuLoadAnimTime = .25f;

    public GameObject PauseMenu;
    public GameObject HUD;

    private bool isPaused;

    public GameObject PauseBackground;


    private GameObject activeMenu;
    public List<GameObject> Menus;
    public void Init()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        isPaused = false;
        activeMenu = HUD;
        ChangeToMenu(HUD);
        PauseBackground.GetComponent<Animator>().SetBool("Active", isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
    }
    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            TogglePause();
        }
    }
    public void MainMenuButt()
    {
        StartCoroutine(MainMenubuttCoro());
    }
    private IEnumerator MainMenubuttCoro()
    {
        PauseBackground.GetComponent<Animator>().SetBool("Active", true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(MenuLoadAnimTime);
        SceneManager.LoadScene(0);
    }
    public void ChangeToMenu(GameObject selectedMenu)
    {
        for (int i = 0; i < Menus.Count; i++)
        {
            if (Menus[i] == selectedMenu)
            {
                StartCoroutine(ChangeToMenuCoro(selectedMenu));
                return;
            }
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        PauseBackground.GetComponent<Animator>().SetBool("Active", isPaused); //set pause background by pause
        Cursor.lockState = isPaused ? CursorLockMode.Confined : CursorLockMode.Locked; //determine lockmode by pause
        Time.timeScale = isPaused ? 0 : 1;
        GameObject menuForCoro = isPaused ? PauseMenu : HUD;
        StartCoroutine(ChangeToMenuCoro(menuForCoro));
    }
    private IEnumerator ChangeToMenuCoro(GameObject selectedMenu)
    {
        for (int i = 0; i < Menus.Count; i++)
        {
            Menus[i].SetActive(Menus[i] == selectedMenu);
            Menus[i].GetComponent<Animator>().SetBool("Active", Menus[i] == selectedMenu);
        }

        yield return new WaitForSeconds(MenuLoadAnimTime);

        activeMenu = selectedMenu;
        activeMenu.GetComponent<Button>().Select();
    }
}
