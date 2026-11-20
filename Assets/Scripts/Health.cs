using UnityEngine;
using UnityEngine.UI;

// VALFRI (om tentan har "hälsa"): Dracula tål flera träffar i stället för att dö direkt.
// Lägg på Dracula. Healthbar = en UI Slider på HUD:en. Kill On Touch tar då 1 hälsa per träff.
// Heal(1) kan anropas från en Player Trigger (t.ex. en blodflaska).
public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public Slider healthBar;
    public float invincibleTime = 1f;    // sekunder man är odödlig efter en träff

    int hp;
    float safeUntil;

    void Start()
    {
        hp = maxHealth;
        UpdateBar();
    }

    public void Damage(int amount)
    {
        if (Time.time < safeUntil) return;
        safeUntil = Time.time + invincibleTime;
        hp -= amount;
        UpdateBar();
        if (hp <= 0)
        {
            PlayerController pc = GetComponent<PlayerController>();
            if (pc) pc.Die();
            else GetComponent<PlayerDeath>().Die();
        }
    }

    public void Heal(int amount)
    {
        hp = Mathf.Min(maxHealth, hp + amount);
        UpdateBar();
    }

    void UpdateBar()
    {
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = hp;
        }
    }
}
