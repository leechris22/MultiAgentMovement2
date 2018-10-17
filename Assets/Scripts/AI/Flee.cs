using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move away from target
public class Flee : AI {
    // Define Output
    override public Steering Output(Kinematic target) {
        // Create the structure to hold our output and bound acceleration
        Steering steering = new Steering(player.data.position - target.position, 0);
        steering.linear = steering.linear.normalized * player.maxAccelerationL;

        // Return acceleration
        return steering;
    }
}
