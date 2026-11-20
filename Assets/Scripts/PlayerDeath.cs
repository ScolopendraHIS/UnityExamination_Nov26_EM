using UnityEngine;

// Lägg på Dracula om du använder kursens PlatformerMovement i stället för PlayerController.
// Sköter döden: partiklar, Dracula försvinner, nivån startar om.
public class PlayerDeath : MonoBehaviour
{
    public GameObject deathEffect;       // prefab med partiklar (+ dödsljud)
    bool dead;

    public void Die()
    {
        if (dead) return;
        dead = true;
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        GameManager.Instance.PlayerDied();
    }
}
