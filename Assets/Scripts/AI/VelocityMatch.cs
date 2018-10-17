using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Match the player's velocity with the target's velocity
public class VelocityMatch : AI {
    // Initialize necessary variables
    [SerializeField]
    private float timeToTarget;

    // Define Output
    override public Steering Output(Kinematic target) {
        // Create the structure to hold our output
        Steering steering = new Steering((target.velocity - player.data.velocity) / timeToTarget, 0);

        // Bound the acceleration
        steering.linear = Vector2.ClampMagnitude(steering.linear, player.maxAccelerationL);

        // Return acceleration
        return steering;
    }
}
