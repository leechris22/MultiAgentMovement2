using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aligns the player's orientation to the target's orientation
public class Align : AI {
    // Initialize necessary variables
    [SerializeField]
    private float slowRadiusA;
    [SerializeField]
    private float stopRadiusA;
    [SerializeField]
    private float timeToTarget;

    // Define Output
    override public Steering Output(Kinematic target) {
        float rotation = target.orientation - player.data.orientation;

        // Map the result to the (-180, 180) interval
        while (rotation > 180) {
            rotation -= 360;
        }
        while (rotation < -180) {
            rotation += 360;
        }
        float rotationSize = Mathf.Abs(rotation);

        // Check if we are there, return no steering
        if (rotationSize < stopRadiusA) {
            return new Steering();
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? player.maxSpeedA : player.maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Create the structure to hold our output and bound acceleration
        Steering steering = new Steering(Vector2.zero, (targetRotation - player.data.rotation) / timeToTarget);
        if (Mathf.Abs(steering.angular) > player.maxAccelerationA) {
            steering.angular /= Mathf.Abs(steering.angular);
            steering.angular *= player.maxAccelerationA;
        }

        // Return angular acceleration
        return steering;
    }
}
