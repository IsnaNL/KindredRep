using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class UIButtonHandler : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    private Button myButt;
    public AudioSource SelectedSound;
    public AudioSource PressedSound;

    private void Start()
    {
        myButt = GetComponent<Button>();
    }
    public void OnSelect(BaseEventData eventData)
    {
        SelectedSound.Play();
    }
    public void OnSubmit(BaseEventData eventData)
    {
        PressedSound.Play();
    }

}
