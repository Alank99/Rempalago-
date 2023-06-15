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
    [SerializeField] float limit_a; // The bullets will fall between the limits a (left) and b (right)
    [SerializeField] float limit_b;
    [SerializeField] float limit_c; // The bullets will fall from the limit c (top)

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
        Vector3 newPos = new Vector3(Random.Range(limit_a, limit_b), limit_c, 0);

        // Create a copy of the bullet prefab
        Instantiate(bullet, newPos, Quaternion.identity);
    }
}