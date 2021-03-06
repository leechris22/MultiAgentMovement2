﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores data for an object used in AI calculations
public class NPCController : MonoBehaviour {
    // Store variables for objects
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Kinematic data;
    public NPCController target;
    public AI ai;

    // Bounds linear changes
    public float maxSpeedL;
    public float maxAccelerationL;

    // Bounds angular changes
    public float maxSpeedA;
    public float maxAccelerationA;

    // On initialization
    protected void Awake() {
        rb = GetComponent<Rigidbody2D>();
        updateData();
    }

    // Update the movement
    private void FixedUpdate() {
        updateData();
        updateMovement(ai.Output(target.data));
    }

    // Update the kinematic data
    protected void updateData() {
        data = new Kinematic(rb.position, rb.rotation, rb.velocity, rb.angularVelocity);
    }
    
    // Update the rigidbody accelerations
    protected void updateMovement(Steering steering) {
        // Bound the acceleration
        steering.linear = Vector2.ClampMagnitude(steering.linear, maxAccelerationL);
        float angularAcceleration = Mathf.Abs(steering.angular);
        if (angularAcceleration > maxAccelerationA) {
            steering.angular /= angularAcceleration;
            steering.angular *= maxAccelerationA;
        }

        // Bound the velocity
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeedL);
        if (rb.angularVelocity > maxSpeedA) {
            rb.angularVelocity = maxSpeedA;
        }

        // Update the position and rotation
        rb.AddForce(steering.linear * rb.mass);
        rb.angularVelocity += steering.angular;
    }
}