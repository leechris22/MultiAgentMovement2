using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move the player away from the target
public class Separate : AI {
    // Initialize necessary variables
    [HideInInspector]
    public List<NPCController> targets;
    [SerializeField]
    private float threshold;
    [SerializeField]
    private float decayCoefficient;

    // On initialization
    override protected void Awake() {
        player = GetComponent<NPCController>();
        targets = new List<NPCController>();
    }

    // Define Output
    override public Steering Output(Kinematic lead) {
        Steering steering = new Steering();

        // Loop through each target
        foreach (NPCController target in targets) {
            // Get direction
            Vector2 direction = target.data.position - player.data.position;

            if (direction.magnitude < threshold) {
                // Calculate the separation strength
                float strength = Mathf.Min(decayCoefficient / (direction.magnitude * direction.magnitude), player.maxAccelerationL);
                steering.linear += -direction.normalized * strength;
            }
        }

        // Return acceleration
        return steering;
    }
}
