using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates the path and calls the editor
public class PathCreator : MonoBehaviour {

    // Define path and customization variables
    public Path path;
    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    // Creates the path
    public void CreatePath() {
        path = new Path(transform.position);
    }

    // Resets the path
    void Reset() {
        CreatePath();
    }
}