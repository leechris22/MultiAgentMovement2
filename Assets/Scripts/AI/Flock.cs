using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Combines behaviors to create Flock behavior
[RequireComponent(typeof(Pursue))]
[RequireComponent(typeof(Separate), typeof(Arrive), typeof(VelocityMatch))]
public class Flock : AI {
    // Flock groups
    [HideInInspector]
    public List<NPCController> targets;

    // Behavior calculations
    [SerializeField]
    private float[] weights;
    [SerializeField]
    private AI[] ai;

    // Distance threshold for targets
    [SerializeField]
    private float threshold;

    // Just to show which behavior represents AI[(int)behavior]
    enum behaviors { Separate, Arrive, VelocityMatch, Pursue, Face, ConeCheck, CollisionPrediction};

    // For collision prevention toggle
    [HideInInspector]
    public bool addCone = false;
    [HideInInspector]
    public bool addPrediction = false;

    // Combine behaviors to create flock behavior
    override public Steering Output(Kinematic lead) {
        // Define variables
        Steering[] strengths = new Steering[weights.Length];
        for (int i = 0; i < strengths.Length; i++) {
            strengths[i] = new Steering();
        }
        int count = 0;
        
        // Calculate the flocking behaviors for each flock
        foreach (NPCController target in targets) {
            if (Vector2.Distance(target.data.position, player.data.position) < threshold) {
                for (int i = 0; i < 3; i++) {
                    strengths[i] += ai[i].Output(target.data);
                }
                count++;
            }
        }
        strengths[1] /= Mathf.Max(count, 1);
        strengths[2] /= Mathf.Max(count, 1);

        // Calculate lead behaviors
        for (int i = 3; i < weights.Length; i++) {
            if ((i != 5 || addCone) && (i != 6 || addPrediction)) {
                strengths[i] = ai[i].Output(lead);
            }
        }

        // Add the strengths and return the result
        Steering steering = new Steering();
        for (int i = 0; i < weights.Length; i++) {
            steering += strengths[i] * weights[i];
        }
        return steering;
    }
}
