using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonNavigationManager : MonoBehaviour
{
    public Button StartButton;
    public Button ExitButton;
    private Button selectedButton;

    void Start()
    {
        // Initially, set the selected button to the startButton
        selectedButton = StartButton;

        // Set the start button as the first selected object
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
    }

    void Update()
    {
        // Check for keyboard/gamepad navigation
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }

        // Check for "w" key to navigate up
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectPreviousButton();
        }

        // Check for "s" key to navigate down
        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectNextButton();
        }

        // Check for "Enter" key to click the selected button
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ClickSelectedButton();
        }
    }

    void SelectPreviousButton()
    {
        selectedButton = GetPreviousButton(selectedButton);
    }

    void SelectNextButton()
    {
        selectedButton = GetNextButton(selectedButton);
    }

    Button GetNextButton(Button currentButton)
    {
        if (currentButton == StartButton)
        {
            return ExitButton;
        }
        else
        {
            return StartButton;
        }
    }

    Button GetPreviousButton(Button currentButton)
    {
        if (currentButton == StartButton)
        {
            return ExitButton;
        }
        else
        {
            return StartButton;
        }
    }

    void ClickSelectedButton()
    {
        if (selectedButton == StartButton)
        {
            Debug.Log("Start button clicked!");
            // Add your logic for the Start button action here.
        }
        else if (selectedButton == ExitButton)
        {
            Debug.Log("Exit button clicked!");
            // Add your logic for the Exit button action here.
            QuitGame();
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
