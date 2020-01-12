using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    public float speed;
    public Vector3 rotationAxis;

    private void Start()
    {
        rotationAxis.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis * speed * Time.deltaTime);
    }
}
