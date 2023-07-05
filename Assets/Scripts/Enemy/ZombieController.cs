using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Zombie is attacking " + collision.gameObject.name + "!");
            animator.SetTrigger("Attack");

            // Force player to fall back
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * 10.0f, ForceMode2D.Impulse);
        }

        if (collision.gameObject.tag == "Flame") {
            Debug.Log("Zombie burned to death...");
            animator.SetTrigger("isDead");
        }
    }
}
