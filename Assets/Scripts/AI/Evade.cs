using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move away from the target's future position
public class Evade : Flee {
    // Initialize necessary variables
    [SerializeField]
    private float maxPrediction;

    // Define Output
    override public Steering Output(Kinematic target) {
        // Calculate prediction scalar based on current speed and target distance
        float distance = (target.position - player.data.position).magnitude;
        float speed = target.velocity.magnitude;

        // Change target to delegate to seek
        target.position += target.velocity * (speed <= distance / maxPrediction ? maxPrediction : distance / speed);

        // Return acceleration
        return base.Output(target);
    }
}
