using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    private bool isButtonsActive;
    public Transform[] buttons;
    public float leanSpeed;


    // Start is called before the first frame update
   

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        isButtonsActive = false;
        Time.timeScale = 1f;

    }
    public void CombatTestScene()
    {
        SceneManager.LoadScene(2);
        

    }
    public void MovementTestScene()
    {
        SceneManager.LoadScene(3);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isButtonsActive = true;
            Time.timeScale = 0.1f;
    
        }
        if (isButtonsActive)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(buttons[i].transform.localPosition.x <= -300)
                {
                    buttons[i].transform.Translate(new Vector2(leanSpeed* Time.deltaTime, 0f));

                }
            }   
            //if (Input.GetKeyDown(KeyCode.Escape)){
            //    isButtonsActive = false;
            //}
        }
        if (!isButtonsActive)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].transform.localPosition.x >= -600)
                {
                    buttons[i].transform.Translate(new Vector2(-leanSpeed * Time.deltaTime, 0f));

                }
                //buttons[i].transform.localPosition = Vector2.Lerp(new Vector2(-300, buttons[i].transform.localPosition.y), new Vector2(-600, buttons[i].transform.localPosition.y), 2);
            }

        }
    }
}
