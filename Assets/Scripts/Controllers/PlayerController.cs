using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for the player
public class PlayerController : NPCController {
    private Vector2 move;

    // Move the player
    private void FixedUpdate() {
        // Take Mouse Input if left button is down
        if (Input.GetMouseButton(0)) {
            move = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            move = Vector2.ClampMagnitude((move - rb.position) * 2, maxAccelerationL);
        } else {
            move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            move = Vector2.ClampMagnitude(move * 20, maxAccelerationL);
        }

        // Add movement and bind the speed
        rb.AddForce(move);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeedL);
        updateMovement(ai.Output(target.data));
        updateData();
    }

    // On collision with Boid, kill the Boid
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Boid")) {
            GameObject manager = GameObject.FindWithTag("Manager");
            if (manager.GetComponent<ScalableManager>()) {
                manager.GetComponent<ScalableManager>().Reorganize(collision.gameObject);
            }
            if (manager.GetComponent<EmergentManager>()) {
                manager.GetComponent<EmergentManager>().Reorganize(collision.gameObject);
            }
            if (manager.GetComponent<TwoLevelManager>()) {
                manager.GetComponent<TwoLevelManager>().Reorganize(collision.gameObject);
            }
        }
    }
}