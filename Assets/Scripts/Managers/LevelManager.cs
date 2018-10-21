using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    // Set prefabs
    [SerializeField]
    protected GameObject BoidPrefab;

    // Set necessary game objects
    protected List<GameObject> Boids;
    [SerializeField]
    protected NPCController Player;

    // Set level loading material
    [SerializeField]
    protected GameObject BoidSpawner;
    [SerializeField]
    protected GameObject Path;

    // Update is called once per frame
    protected void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SceneManager.LoadScene(2);
        }
    }

    // Show the spawn rectangle
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(BoidSpawner.transform.position, BoidSpawner.transform.localScale);
    }
}
