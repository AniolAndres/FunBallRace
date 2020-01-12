using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public Vector3 offset;
    public bool isLastPlatform = false;

    private bool alreadyHit = false;
    private GameObject particleGO;
    public void ResetPlatform()
    {
        alreadyHit = false;
    }

    private void Start()
    {
        if(isLastPlatform)
        {
            particleGO = GameObject.FindGameObjectWithTag("Particles");
            particleGO.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!alreadyHit)
        {
            collision.gameObject.transform.position = transform.position + offset;
            collision.gameObject.GetComponentInChildren<PlayerScript>().PlatformHit();

            if(isLastPlatform)
            {
                collision.gameObject.GetComponent<PlayerScript>().FinishGame();

                particleGO.SetActive(true);
            }
            alreadyHit = true;
        }
    }
}
