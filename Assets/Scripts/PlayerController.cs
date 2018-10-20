using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for the player
public class PlayerController : NPCController {
    private Vector2 move;

    // Move the player
    protected void FixedUpdate() {
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
        updateMovement(ai.Output(target.data), Time.deltaTime);
        updateData();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Boid")) {
            //GameObject.FindWithTag("Manager").GetComponent<ScalableManager>().Reorganize(collision.gameObject);
            GameObject.FindWithTag("Manager").GetComponent<EmergentManager>().Reorganize(collision.gameObject);
        }
    }
}