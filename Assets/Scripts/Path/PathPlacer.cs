using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a line from the path using the linerenderer
[RequireComponent(typeof(LineRenderer))]
public class PathPlacer : MonoBehaviour {
    // Declare variables
    public float spacing = 1f;
    public float resolution = 1;

    // Hold the linerenderer and point prefab
    private LineRenderer line;
    [HideInInspector]
    public Kinematic[] path;

    // On start, create points
    void Awake() {
        line = GetComponent<LineRenderer>();

        Vector2[] points = GetComponent<PathCreator>().path.CalculateEvenlySpacedPoints(spacing, resolution);
        line.positionCount = points.Length;
        path = new Kinematic[points.Length];
        
        // Add points to linerenderer and path array
        for (int i = 0; i < points.Length; i++) {
            line.SetPosition(i, points[i]);
            path[i] = new Kinematic(points[i], 0, Vector2.zero, 0);
        }
    }
}