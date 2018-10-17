using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Faces the player towards the target
public class Face : Align {
    // Define Output
    override public Steering Output(Kinematic target) {
        // Work out the direction to target
        Vector2 direction = target.position - player.data.position;
        
        // If on top of each other, return no angular acceleration
        if (direction.magnitude == 0) {
            return new Steering();
        }

        // Change target to delegate to align
        target.orientation = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        
        // Return angular acceleration
        return base.Output(target);
    }
}
