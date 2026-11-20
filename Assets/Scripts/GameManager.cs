using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// En per nivå. Räknar dödsfall, krossade lådor och tiden tills solen går upp.
// Tiden slut = GAME OVER. R = starta om nivån.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int deaths;
    public static int crates;
    static string lastLevel = "";
    static float savedTime;

    public float timeLimit = 150f;
    public string nextScene = "Level2";
    public TMP_Text deathsText;
    public TMP_Text timerText;
    public TMP_Text cratesText;

    float timeLeft;
    bool busy;

    void Awake()
    {
        Instance = this;
        string level = SceneManager.GetActiveScene().name;
        if (level != lastLevel) { lastLevel = level; savedTime = timeLimit; }   // ny nivå
        timeLeft = savedTime;                                                  // tiden fortsätter efter död
    }

    void Update()
    {
        if (busy) return;
        timeLeft -= Time.deltaTime;
        savedTime = timeLeft;

        if (timeLeft <= 0) { busy = true; lastLevel = ""; SceneManager.LoadScene("GameOver"); }
        if (Keys.RestartPressed) PlayerDied();

        if (deathsText) deathsText.text = "Dödsfall: " + deaths;
        if (cratesText) cratesText.text = "Lådor krossade: " + crates;
        if (timerText) timerText.text = "Solen går upp om: " + Mathf.CeilToInt(timeLeft);
    }

    public void PlayerDied()
    {
        if (busy) return;
        busy = true;
        deaths++;
        StartCoroutine(Load(SceneManager.GetActiveScene().name, 0.6f));
    }

    public void CompleteLevel()          // anropas av vinet (Player Trigger-event)
    {
        if (busy) return;
        busy = true;
        lastLevel = "";
        StartCoroutine(Load(nextScene, 1f));
    }

    public void AddCrate() => crates++;

    IEnumerator Load(string scene, float wait)
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(scene);
    }
}
