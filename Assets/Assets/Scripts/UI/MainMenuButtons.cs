using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject frontPage;
    public GameObject settingsPage;
    // Start is called before the first frame update
   
   public void ExitButtonPressed()
    {
        Application.Quit();
    }
  public  void NewGameButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
    
    public void SettingsButtonPressed()
    {
        frontPage.SetActive(false);
        settingsPage.SetActive(true);
    }
    public void BackToMainMenuButtonPressed()
    {
        frontPage.SetActive(true);
        settingsPage.SetActive(false);
    }
    // Update is called once per frame
   
}
