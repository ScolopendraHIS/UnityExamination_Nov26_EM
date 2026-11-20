using UnityEngine;

// Styr Dracula: A/D = gå, Space/W = hoppa (upp), S = falla snabbt (ner).
// Skickar Speed / Grounded till Animatorn. Dör när KillOnTouch rör honom.
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.5f;
    public float jumpForce = 12f;
    public float fastFallSpeed = 18f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public AudioSource jumpSound;        // AudioSource med "Audio Random Container" = slumpat ljud
    public GameObject deathEffect;       // prefab med partiklar (+ dödsljud)

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    float moveInput;
    bool grounded, dead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dead) return;

        moveInput = Keys.Horizontal;
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Keys.JumpPressed && grounded)
        {
            rb.SetVel(new Vector2(rb.GetVel().x, jumpForce));
            if (jumpSound) jumpSound.Play();
        }
        if (Keys.JumpReleased && rb.GetVel().y > 0)
            rb.SetVel(new Vector2(rb.GetVel().x, rb.GetVel().y * 0.5f));

        if (moveInput > 0) sr.flipX = false;
        if (moveInput < 0) sr.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("Grounded", grounded);
    }

    void FixedUpdate()
    {
        if (dead) return;
        float y = rb.GetVel().y;
        if (Keys.DownHeld && !grounded) y = -fastFallSpeed;
        rb.SetVel(new Vector2(moveInput * moveSpeed, y));
    }

    public void Die()
    {
        if (dead) return;
        dead = true;
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        GameManager.Instance.PlayerDied();
    }
}
