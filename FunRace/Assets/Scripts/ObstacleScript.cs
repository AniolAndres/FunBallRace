using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float oscilationSpeed;
    public float oscilationAmplitude;
    public Vector3 oscilationDirection;

    private Vector3 initialPosition;
    private float alpha = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        oscilationDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition + oscilationDirection * (oscilationAmplitude * Mathf.Sin(alpha));

        alpha += Time.deltaTime * oscilationSpeed;

        if (alpha > Mathf.PI * 2.0f)
        {
            alpha -= Mathf.PI * 2.0f;
        }
    }
}
