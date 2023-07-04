using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    private Animator animator;

    bool isJumping;

    public float speed = 3.0f;
    public float jumpHeight = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveUp = Input.GetAxis("Vertical");
        float moveRight = Input.GetAxis("Horizontal");

        // If "moveRight" is negative, it means the player is going left, so flip sprite
        if (moveRight != 0) {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = moveRight < 0;
        }

        // Trigger walking animation when character is moving
        animator.SetBool("isWalking", moveRight != 0);
        // Debug.Log("is walking: " + (moveRight != 0));

        Vector2 movementDirection = new Vector2(moveRight, 0);
        this.transform.Translate(movementDirection.normalized * speed * Time.deltaTime);

        // If no jump is in progress and player press "up", trigger new jump
        if (moveUp > 0 && !isJumping) {
            isJumping = true;
            animator.SetBool("isJumping", true);

            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            Debug.Log("Start jump with force " + jumpForce);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Stop jumping when collide with floor
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping && collision.collider.tag == "Surface") {
            Debug.Log("stop jump");
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }
}
