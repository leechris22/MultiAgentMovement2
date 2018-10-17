using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager1 : MonoBehaviour {
    // Set prefabs
    [SerializeField]
    private GameObject BoidPrefab;

    // Set necessary game objects
    [SerializeField]
    private List<GameObject> Boids;
    [SerializeField]
    private NPCController Player;

    // Set level loading material
    [SerializeField]
    private GameObject BoidSpawner;

    // On initialization
    void Start() {
        // Spawn 20 Boids
        Boids = new List<GameObject>();
        for (int i = 0; i < 20; i++) {
            SpawnBoids();
        }
    }

    // Update is called once per frame
    void Update() {
        MovetoScene();
    }

    // Spawn a boid and set necessary targets
    private void SpawnBoids() {
        Vector3 size = BoidSpawner.transform.localScale;
        Vector3 position = BoidSpawner.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        GameObject temp = Instantiate(BoidPrefab, position, Quaternion.identity);

        temp.GetComponent<NPCController>().target = Player;
        foreach (GameObject Boid in Boids) {
            Boid.GetComponent<Flock>().targets.Add(temp.GetComponent<NPCController>());
            temp.GetComponent<Flock>().targets.Add(Boid.GetComponent<NPCController>());
        }
        Boids.Add(temp);
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

    // Show the spawn rectangle
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(BoidSpawner.transform.position, BoidSpawner.transform.localScale);
    }
}
