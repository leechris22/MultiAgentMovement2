using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all formations
// In this case, a circle
public class Formation : MonoBehaviour {
    // Initialize necessary variables
    [SerializeField]
    private GameObject PointPrefab;
    [HideInInspector]
    public float radius = 0;

    // Define base formation to create
    public GameObject[] CreateFormation(int size, float spacing, bool leader) {
        if (leader) {
            // Initialize array
            GameObject[] formation = new GameObject[size];

            // Create a regular polygon with 'size' vertices and 'spacing' side length
            radius = spacing / (2 * Mathf.Sin(Mathf.PI / size));
            float orientation = 0;
            for (int i = 0; i < size; i++) {
                float x = Mathf.Sin(Mathf.Deg2Rad * orientation) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * orientation) * radius;
                formation[i] = Instantiate(PointPrefab, new Vector2(x, y), Quaternion.Euler(0, 0, -orientation + 360));
                orientation += 360 / size;
            }
            return formation;
        } else {
            // Initialize array
            GameObject[] formation = new GameObject[size-1];

            // Create a regular polygon with 'size' vertices and 'spacing' side length
            radius = spacing / (2 * Mathf.Sin(Mathf.PI / size));
            float orientation = 0;
            for (int i = 0; i < size-1; i++) {
                orientation += 360 / size;
                float x = Mathf.Sin(Mathf.Deg2Rad * orientation) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * orientation) * radius - radius;
                formation[i] = Instantiate(PointPrefab, new Vector2(x, y), Quaternion.Euler(0,0, -orientation+360));
            }
            return formation;
        }
    }
}
