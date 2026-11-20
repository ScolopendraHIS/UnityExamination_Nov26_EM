using UnityEngine;
using UnityEngine.Events;

// När Dracula går in i zonen händer det du väljer i Inspector (On Player Enter):
// t.ex. visa ett objekt, starta en animation, byta sprite, spela ljud, klara nivån.
public class PlayerTrigger : MonoBehaviour
{
    public bool onlyOnce = true;
    public UnityEvent onPlayerEnter;
    bool used;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (onlyOnce && used) return;
        used = true;
        onPlayerEnter.Invoke();
    }

    void OnDrawGizmos()
    {
        Collider2D c = GetComponent<Collider2D>();
        if (!c) return;
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.4f);
        Gizmos.DrawCube(c.bounds.center, c.bounds.size);
    }
}
