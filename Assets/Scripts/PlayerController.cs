using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for the player
public class PlayerController : NPCController {
    private Vector2 move;

    // Move the player
    void FixedUpdate() {
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
        rb.MoveRotation(-Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg);
        updateData();
    }
}