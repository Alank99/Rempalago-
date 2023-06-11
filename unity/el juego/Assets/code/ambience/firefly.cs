using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firefly : MonoBehaviour
{
    private Vector3 startPos;

    public Vector2 minMaxMovement;
    public float area = 1;

    private Vector2 minMaxAreaX;
    private Vector2 minMaxAreaY;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetFloat("randSpeed", Random.Range(1f, 0.5f));
    }
}
