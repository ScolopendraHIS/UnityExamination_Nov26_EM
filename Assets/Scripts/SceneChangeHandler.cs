using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This component can be used to access methods for changing the scene, and quitting the game
// It can be used together with UnityEventOnTrigger, or UI-button-events, to decide when a scene should be changed or the game should be closed
public class SceneChangeHandler : MonoBehaviour
{
    // Kursens original: laddar alltid scenen "NewScene"
    public void ChangeScene()
    {
        // Load the scene named "NewScene"
        SceneManager.LoadSceneAsync("NewScene", LoadSceneMode.Single);
    }

    // NY: byt till valfri scen. Skriv scenens namn i knappens/eventets ruta,
    // t.ex. Level1, Story, Menu, Win eller GameOver.
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    // NY: starta om den scen man är i (t.ex. när Dracula dör).
    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // stoppar även Play-läget i Unity
#endif
    }
}
