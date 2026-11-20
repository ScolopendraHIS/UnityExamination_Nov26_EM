using UnityEngine;

// VALFRI (om tentan har "byt fotstegsljud baserat på material"):
// Lägg på Dracula. Anropa PlayStep() från ett Animation Event i Dracula_Run.
// Står Dracula på en collider med Physics Material "woodMaterial" spelas träljud, annars stenljud.
public class SurfaceFootsteps : MonoBehaviour
{
    public AudioSource source;
    public AudioClip stoneStep;
    public AudioClip woodStep;
    public PhysicsMaterial2D woodMaterial;
    public LayerMask groundLayer;

    public void PlayStep()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);
        if (!hit) return;

        bool onWood = woodMaterial != null && hit.collider.sharedMaterial == woodMaterial;
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(onWood ? woodStep : stoneStep);
    }
}
