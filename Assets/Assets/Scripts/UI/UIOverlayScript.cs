using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIOverlayScript : MonoBehaviour
{
    public const float PressAnimTime = .5f;
    public const float MenuLoadAnimTime = .25f;

    private bool isPaused;

    public GameObject PauseMenu;
    public GameObject Main;
    public GameObject Controls;
    
    public GameObject HUD;
    private void Start()
    {
        Time.timeScale = 1;
        HUD.SetActive(true);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Pause());
        }
    }
    public IEnumerator Pause()
    {
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        StartCoroutine(SwapToPause());
        yield return null;
    }
    public void Resume()
    {
        StartCoroutine(ResumeCoro());
    }
    public IEnumerator ResumeCoro()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        yield return new WaitForSeconds(MenuLoadAnimTime);
        StartCoroutine(SwapToHUD());
    }
    public void MainMenuButt()
    {
        StartCoroutine(MainMenubuttCoro());
    }
    private IEnumerator MainMenubuttCoro()
    {
        PauseMenu.GetComponent<Animator>().SetBool("Active", false);
        Time.timeScale = 1;
        yield return new WaitForSeconds(MenuLoadAnimTime);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(0);
    }
    private IEnumerator SwapToHUD()
    {
        PauseMenu.SetActive(true);
        HUD.SetActive(true);
        PauseMenu.GetComponent<Animator>().SetBool("Active", false);
        HUD.GetComponent<Animator>().SetBool("Active", true);
        yield return new WaitForSeconds(MenuLoadAnimTime);
        PauseMenu.SetActive(false);
    }
    private IEnumerator SwapToPause()
    {
        PauseMenu.SetActive(true);
        HUD.SetActive(true);
        PauseMenu.GetComponent<Animator>().SetBool("Active", true);
        HUD.GetComponent<Animator>().SetBool("Active", false);
        yield return new WaitForSeconds(MenuLoadAnimTime);
        HUD.SetActive(false);
    }
    public void ControlsButt()
    {
        StartCoroutine(controlsButtCoro());
    }
    private IEnumerator controlsButtCoro()
    {
        Controls.SetActive(true);
        Main.SetActive(true);
        Main.GetComponent<Animator>().SetBool("Active", false);
        Controls.GetComponent<Animator>().SetBool("Active", true);
        yield return new WaitForSeconds(MenuLoadAnimTime);
        Controls.GetComponentInChildren<Button>().Select();
        Main.SetActive(false);
    }
    public void MainButt()
    {
        StartCoroutine(MainButtCoro());
    }
    private IEnumerator MainButtCoro()
    {
        Main.SetActive(true);
        Controls.SetActive(true);
        Main.GetComponent<Animator>().SetBool("Active", true);
        Controls.GetComponent<Animator>().SetBool("Active", false);
        yield return new WaitForSeconds(MenuLoadAnimTime);
        Controls.SetActive(false);
        Main.GetComponentInChildren<Button>().Select();
    }


}
