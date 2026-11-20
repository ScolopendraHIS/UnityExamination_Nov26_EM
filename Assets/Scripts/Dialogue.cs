using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Lägg på Igor. Stå nära och tryck E för att prata, E igen för nästa rad.
public class Dialogue : MonoBehaviour
{
    public string characterName = "Igor";
    [TextArea]
    public string[] lines =
    {
        "Mäster Dracula! Äntligen är ni vaken!",
        "Van Helsing har gömt ert vin längst in i slottet...",
        "...och fyllt korridorerna med fällor. Lita inte på golvet!",
        "Skynda er, herre. Om solen går upp innan ni druckit är allt förlorat!"
    };
    [TextArea]
    public string[] linesAfterDeath =   // sägs i stället om Dracula har dött minst en gång
    {
        "Ni dog igen, mäster... Van Helsings fällor är listiga.",
        "Kom ihåg: lita inte på golvet. Och inte på taket heller."
    };
    public GameObject panel;             // dialogrutan på Canvas
    public TMP_Text nameText;
    public TMP_Text lineText;
    public GameObject pressE;            // "[E] Prata"-texten
    public AudioSource talkSound;
    public UnityEvent onDialogueFinished;   // något händer i världen när samtalet är slut

    bool playerNear;
    int line = -1;

    void Start()
    {
        panel.SetActive(false);
        pressE.SetActive(false);
    }

    void Update()
    {
        if (!playerNear || !Keys.TalkPressed) return;

        // Dialogen VARIERAR beroende på vad som hänt i spelet
        string[] current = (GameManager.deaths > 0 && linesAfterDeath.Length > 0) ? linesAfterDeath : lines;

        line++;
        if (line < current.Length)
        {
            panel.SetActive(true);
            pressE.SetActive(false);
            nameText.text = characterName;
            lineText.text = current[line];
            if (talkSound) talkSound.Play();
        }
        else
        {
            panel.SetActive(false);
            line = -1;
            onDialogueFinished.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerNear = true;
        pressE.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerNear = false;
        line = -1;
        pressE.SetActive(false);
        panel.SetActive(false);
    }
}
