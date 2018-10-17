using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    // Initialize necessary variables
    protected NPCController player;

    // On initialization
    virtual protected void Awake() {
        player = GetComponent<NPCController>();
    }

    // Define steering output to override and call
    virtual public Steering Output(Kinematic target) {
        return new Steering();
    }
}
