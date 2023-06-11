/*
Create copies of a #bullet# object to fall on the game scene
The #bullets# fall at regular intervals from random positions above the screen

Gilberto Echeverría
2023-04-18

Modified by Andrea Barrón
2023-06-10
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class rainBullets : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float delay;
    [SerializeField] float limit;

    // Start is called before the first frame update
    void Start()
    {
        // Call the specified function at regular intervals
        InvokeRepeating("CreateBullet", delay, delay);
    }

    public void Stop()
    {
        // Cancel the repeated call of the function, so the bullets stop falling
        CancelInvoke("CreateBullet");
    }

    void CreateBullet()
    {
        // Generate a random position in X and over the view of the camera
        Vector3 newPos = new Vector3(Random.Range(-limit, limit), 6.5f, 0);

        // Create a copy of the bullet prefab
        Instantiate(bullet, newPos, Quaternion.identity);
    }
}
