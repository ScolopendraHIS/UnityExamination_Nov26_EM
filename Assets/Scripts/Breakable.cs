using UnityEngine;

// Trälåda som spärrar vägen. Krossas när något med Rigidbody 2D
// (Dracula, dödskallen) går in i den. Räknas i HUD:en.
public class Breakable : MonoBehaviour
{
    public GameObject breakEffect;       // prefab med partiklar (+ krossljud)

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.rigidbody == null) return;
        if (breakEffect) Instantiate(breakEffect, transform.position, Quaternion.identity);
        if (other.gameObject.CompareTag("Player"))     // räkna bara lådor som Dracula krossar
            GameManager.Instance.AddCrate();
        Destroy(gameObject);
    }
}
