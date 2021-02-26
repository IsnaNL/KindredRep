using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOverlayScript : MonoBehaviour
{
    public const float PressAnimTime = .5f;
    public const float MenuLoadAnimTime = .25f;

    private bool isPaused;

    public GameObject PauseMenu;
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
        yield return new WaitForSeconds(MenuLoadAnimTime);
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
}
