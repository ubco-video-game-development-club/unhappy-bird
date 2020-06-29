using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public float maxAngle = 15f;
    public float rotationSpeed = 0.2f;

    private bool isAlive;
    private bool isFrozen;
    private bool hasStarted;
    private Rigidbody2D rb2D;
    private Animator animator;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb2D.gravityScale = 0;
        hasStarted = false;
        isAlive = true;
        isFrozen = true;
        animator.SetBool("IsActive", false);
    }

    void Update() {
        // Don't allow input if the player is dead
        if (!isAlive) {
            return;
        }

        // If the user clicked/tapped or pressed spacebar
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) {
            // Start the game on the first tap
            if (!hasStarted) {
                GameController.instance.StartGame();
                rb2D.gravityScale = 1;
                hasStarted = true;
                isFrozen = false;
                animator.SetBool("IsActive", true);
            }

            // Add jump force
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

        // Constant forward velocity
        if (hasStarted) {
            rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
        }
    }

    void FixedUpdate() {
        // Don't allow re-orientation if player is frozen
        if (isFrozen) {
            return;
        }

        // Face current velocity direction
        Vector2 lookDirection = rb2D.velocity.normalized;
        if (rb2D.velocity.y > -jumpForce) {
            Quaternion maxRotation = Quaternion.AngleAxis(maxAngle, Vector3.forward);
            lookDirection = maxRotation * Vector2.right;
        }
        Quaternion targetRotation = Quaternion.FromToRotation(Vector2.right, lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
    }

    public void Die() {
        if (isAlive) {
            GameController.instance.EndGame();
            isAlive = false;
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    public void Freeze() {
        isFrozen = true;
        animator.SetBool("IsActive", false);
        rb2D.isKinematic = true;
        rb2D.velocity = Vector2.zero;
    }
}
