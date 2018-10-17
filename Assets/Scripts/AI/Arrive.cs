using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : AI {
    // Initialize necessary variables
    [SerializeField]
    private float slowRadiusL;
    [SerializeField]
    private float stopRadiusL;
    [SerializeField]
    private float timeToTarget;

    // Define Output
    override public Steering Output(Kinematic target) {
        // Get the direction to the target
        Vector2 direction = target.position - player.data.position;
        float distance = direction.magnitude;

        // Check if we are there, return no steering
        if (distance < stopRadiusL) {
            return new Steering();
        }

        // Calculate a scaled speed if player is inside the slowRadius
        float targetSpeed = (distance > slowRadiusL ? player.maxSpeedL : player.maxSpeedL * distance / slowRadiusL);

        // Create the structure to hold our output and bound acceleration
        Steering steering = new Steering((direction.normalized * targetSpeed - player.data.velocity) / timeToTarget, 0);
        steering.linear = Vector2.ClampMagnitude(steering.linear, player.maxAccelerationL);

        // Return acceleration
        return steering;
    }
}
