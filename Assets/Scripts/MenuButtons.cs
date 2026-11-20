using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Lägg på Canvas i Menu, GameOver och Win. Koppla knapparnas On Click hit.
public class MenuButtons : MonoBehaviour
{
    public TMP_Text statsText;           // valfri: visar dödsfall och krossade lådor

    void Start()
    {
        if (statsText) statsText.text = "Dödsfall: " + GameManager.deaths + "\nLådor krossade: " + GameManager.crates;
    }

    public void StartLevel(string sceneName)     // från menyn: nollställer statistiken
    {
        GameManager.deaths = 0;
        GameManager.crates = 0;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
