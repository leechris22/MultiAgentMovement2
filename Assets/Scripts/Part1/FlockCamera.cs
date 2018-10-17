using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for the camera
public class FlockCamera : MonoBehaviour {
    // The camera to move to the player
    [SerializeField]
    private GameObject player;
    
    // Move camera on the x axis with the player
    void LateUpdate() {
        transform.position = new Vector3(player.transform.position.x, 0, transform.position.z);
    }
}
