using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayableCharacter : MonoBehaviour
{
    public bool isActiveCharacter;
    public CinemachineVirtualCamera assignedCamera;

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
        // Do nothing unless character is currently active
        if (!isActiveCharacter) {
            return;
        }

        float moveUp = Input.GetAxis("Vertical");
        float moveRight = Input.GetAxis("Horizontal");

        // If "moveRight" is negative, it means the player is going left, so flip sprite
        if (moveRight != 0) {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = moveRight < 0;
        }

        // Trigger walking animation when character is moving
        animator.SetBool("isWalking", moveRight != 0);

        Vector2 movementDirection = new Vector2(moveRight, 0);
        this.transform.Translate(movementDirection.normalized * speed * Time.deltaTime);

        // If no jump is in progress and player press "up", trigger new jump
        if (moveUp > 0 && !isJumping) {
            isJumping = true;
            animator.SetBool("isJumping", true);

            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    // Stop jumping when collide with floor
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJumping && (collision.collider.tag == "Surface" || collision.collider.tag == "Box")) {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    // Custom events to detect if the character has already reached the goal destination
    public delegate void CharacterReachedGoalEventHandler (PlayableCharacter character);
    public static event CharacterReachedGoalEventHandler OnGoalEnter;

    public delegate void CharacterLeftGoalEventHandler (PlayableCharacter character);
    public static event CharacterLeftGoalEventHandler OnGoalExit;

    private void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Goal") {
            Debug.Log(this.gameObject.name + " arrived to goal!");
            if (OnGoalEnter != null) {
                OnGoalEnter(this);
            }
        }
    }

    private void OnTriggerExit2D (Collider2D collider)
    {
        if (collider.gameObject.tag == "Goal") {
            Debug.Log(this.gameObject.name + " got far away from goal...");
            if (OnGoalExit != null) {
                OnGoalExit(this);
            }
        }
    }

}
