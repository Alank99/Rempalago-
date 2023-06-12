using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField]
    bool isActive;
    [SerializeField]
    Vector2 spawnTime;
    [SerializeField]
    GameObject monsterToSpawn;

    [SerializeField]
    Vector2 spawnDistance;
    [SerializeField]
    Vector2 spawnDistanceOffset;
    [SerializeField]
    Vector2 spawnQuantityRange;
    [SerializeField]
    int maxMonstersInArea;

    List<GameObject> monstersInArea;

    private void Start() {
        monstersInArea = new List<GameObject>();
    }

    IEnumerator ActiveSpawning()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(Random.Range(spawnTime.x, spawnTime.y));
            for (int i = 0; i < Random.Range(spawnQuantityRange.x, spawnQuantityRange.y); i++)
            {
                // only make new monster if there is space
                if (monstersInArea.Count < maxMonstersInArea)
                    Instantiate(monsterToSpawn, transform.position 
                            + new Vector3(
                                Random.Range(-spawnDistance.x, spawnDistance.x), 
                                Random.Range(-spawnDistance.y, spawnDistance.y),
                                0) 
                            + (Vector3)spawnDistanceOffset, 
                        Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerRadius"){
            isActive = true;
            StartCoroutine(ActiveSpawning());
        }
        else if (other.tag == "Enemy"){
            monstersInArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "PlayerRadius"){
            StopAllCoroutines();
            isActive = false;
        }
        else if (other.tag == "Enemy"){
            monstersInArea.Remove(other.gameObject);
        }
    }

    /// <summary>
    /// Draw the lines that show the spawn area
    /// </summary>
    private void OnDrawGizmosSelected() {
        Vector2 pointA = new Vector2(transform.position.x - spawnDistance.x + spawnDistanceOffset.x, transform.position.y - spawnDistance.y + spawnDistanceOffset.y);
        Vector2 pointB = new Vector2(transform.position.x + spawnDistance.x + spawnDistanceOffset.x, transform.position.y - spawnDistance.y + spawnDistanceOffset.y);
        Vector2 pointC = new Vector2(transform.position.x - spawnDistance.x + spawnDistanceOffset.x, transform.position.y + spawnDistance.y + spawnDistanceOffset.y);
        Vector2 pointD = new Vector2(transform.position.x + spawnDistance.x + spawnDistanceOffset.x, transform.position.y + spawnDistance.y + spawnDistanceOffset.y);


        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pointA, pointB);
        Gizmos.DrawLine(pointD, pointB);
        Gizmos.DrawLine(pointC, pointD);
        Gizmos.DrawLine(pointA, pointC);
    }
}
