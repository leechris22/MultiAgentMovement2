using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calls multiple behaviors
public class MultiBehavior : AI {
    // Behavior calculations
    public float[] weights;
    public AI[] ai;

    // Output multiple behaviors
    override public Steering Output(Kinematic target) {
        // Calculate behaviors, add weights, and output the result
        Steering steering = new Steering();
        for (int i = 0; i < ai.Length; i++) {
            steering += ai[i].Output(target) * weights[i];
        }

        return steering;
    }
}
