using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCamera : MonoBehaviour {
    [SerializeField]
    private float speed;

    // Move the player
    void Update() {
        transform.Translate(new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0));
    }
}
