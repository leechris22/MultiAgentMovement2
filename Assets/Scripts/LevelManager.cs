using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    // Set prefabs
    [SerializeField]
    protected GameObject BoidPrefab;

    // Set necessary game objects
    [SerializeField]
    protected List<GameObject> Boids;
    [SerializeField]
    protected NPCController Player;

    // Set level loading material
    [SerializeField]
    protected GameObject BoidSpawner;
    [SerializeField]
    protected GameObject Path;

    // Show the spawn rectangle
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(BoidSpawner.transform.position, BoidSpawner.transform.localScale);
    }
}
