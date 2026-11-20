using UnityEngine;

// Dödar Dracula när han rör objektet (spikar, laser, fladdermöss, dödskalle).
// Funkar med både PlayerController och kursens PlatformerMovement (+ PlayerDeath).
// Har Dracula komponenten Health tar han i stället 1 i skada.
public class KillOnTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) => Hit(other.gameObject);
    void OnTriggerStay2D(Collider2D other) => Hit(other.gameObject);
    void OnCollisionEnter2D(Collision2D other) => Hit(other.gameObject);

    void Hit(GameObject other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        PlayerDeath pd = other.GetComponent<PlayerDeath>();
        if (!pc && !pd) return;

        Health health = other.GetComponent<Health>();
        if (health) health.Damage(1);
        else if (pc) pc.Die();
        else pd.Die();
    }
}
