using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public const float PressAnimTime = .5f;
    public const float MenuLoadAnimTime = .5f;
    public List<GameObject> Menus;
    private void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }
    public void Quit()
    {
        StartCoroutine(QuitCoro());
    }
    public IEnumerator QuitCoro()
    {
        yield return new WaitForSeconds(MenuLoadAnimTime);
        Application.Quit();
    }
    public void StartButton()
    {
        StartCoroutine(StartButtonCoro());
    }
    public IEnumerator StartButtonCoro()
    {
        yield return new WaitForSeconds(PressAnimTime);
        SceneManager.LoadScene(1);
    }
    public void SelectMenu(GameObject menuRef)
    {
        StartCoroutine(SelectMenuCoro(menuRef));
    }
    private IEnumerator SelectMenuCoro(GameObject menuRef)
    {
        int tempIndex = 0;
        if (Menus.Contains(menuRef))
        {
            yield return new WaitForSeconds(PressAnimTime);
            for (int i = 0; i < Menus.Count; i++)
            {
                Menus[i].SetActive(Menus[i].Equals(menuRef));
                if (Menus[i].Equals(menuRef))
                {
                    tempIndex = i;
                }
                Menus[i].GetComponent<Animator>().SetBool("Active", Menus[i].Equals(menuRef));
            }
            yield return new WaitForSeconds(MenuLoadAnimTime);
            Menus[tempIndex].GetComponentInChildren<Button>().Select();
        }
        else
        {
            Debug.LogError("{0} cant be selected as a menu", menuRef);
        }
        
    }
}
