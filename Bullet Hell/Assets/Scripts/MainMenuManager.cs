#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start() => SceneManager.LoadScene("Game"); // one line functions can use "=>"

    public void QuitGame()
    {
        Application.Quit(); // quits game when in a standalone build version of the game

#if UNITY_EDITOR // Required: only runs the following line if in the Unity Editor
        UnityEditor.EditorApplication.ExitPlaymode(); // exits playmode when in the Unity Editor
#endif
    }
}

