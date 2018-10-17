using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Face the direction the player is moving
public class FaceForward : Align {
    // Define Output
    override public Steering Output(Kinematic target) {
        target.orientation = -Mathf.Atan2(player.data.velocity.x, player.data.velocity.y) * Mathf.Rad2Deg;
        return base.Output(target);
    }
}