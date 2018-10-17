using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move the player away from the target
public class Separate : AI {
    // Initialize necessary variables
    [SerializeField]
    private float decayCoefficient;
    
    // Define Output
    override public Steering Output(Kinematic target) {
        // Get direction
        Vector2 direction = target.position - player.data.position;
        
        // Calculate the separation strength
        float strength = Mathf.Min(decayCoefficient / (direction.magnitude * direction.magnitude), player.maxAccelerationL);

        // Return acceleration
        return new Steering(-direction.normalized * strength, 0);
    }
}
