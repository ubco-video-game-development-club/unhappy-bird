using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    private bool isAlive;
    private bool hasStarted;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
        hasStarted = false;
        isAlive = true;
    }

    void Update()
    {
        // Face current velocity direction
        Vector2 direction = rb2D.velocity.normalized;
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction);

        // Don't allow input if the player is dead
        if (!isAlive) {
            return;
        }

        // If the user clicked/tapped or pressed spacebar
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) {
            // Do stuff the first time the user taps
            if (!hasStarted) {
                rb2D.gravityScale = 1;
                hasStarted = true;
            }

            // Add jump force
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

        // Constant forward velocity
        if (hasStarted) {
            rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
        }
    }

    public void Die() {
        isAlive = false;
    }

    public void Freeze() {
        rb2D.isKinematic = true;
    }
}
