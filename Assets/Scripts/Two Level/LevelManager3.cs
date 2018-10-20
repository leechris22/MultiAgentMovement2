using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager3 : MonoBehaviour {
    // Set lead
    [SerializeField]
    private GameObject Lead;

    // Set level loading material
    [SerializeField]
    private GameObject Path;

    // On initialization
    void Start() {
        // Set leads to follow path
        Lead.GetComponent<PathFollow>().path = Path.GetComponent<PathPlacer>().path;
    }

    // Update is called once per frame
    void Update() {
        MovetoScene();
    }

    // Move to the next scene
    private void MovetoScene() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SceneManager.LoadScene(2);
        }
    }

}
