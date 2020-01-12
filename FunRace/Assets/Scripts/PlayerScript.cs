using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float maxForce = 1000.0f;
    public float oscilationSpeed = 1.0f;
    public Vector3 firingDirection;
    public Rigidbody rigid;
    public Material mat;
    public float trueForce = 0.0f;
    public float sleepDuration = 2.0f;
    public float resetHeight = -50.0f;
    public float endDuration = 5.0f;

    private bool shotDone = false;
    private bool chargingShot = false;
    private float alpha = 0.0f;
    private bool isAsleep = true;
    private float sleepTimer = 0.0f;
    private float endTimer = 0.0f;
    private Vector3 initialPosition;
    private GameObject[] platforms;
    private bool finishGame = false;

    public bool GetFinish() { return finishGame; }

    private void ResetForces()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    //we will use this function to reset movement
    public void PlatformHit()
    {
        ResetForces();
        alpha = 0.0f;
        mat.color = Color.white;
        isAsleep = true;
        shotDone = false;
        chargingShot = false;
    }

    public void FinishGame()
    {
        finishGame = true;
    }

    private void HandleInput()
    { 
        if(!shotDone && Input.GetKeyDown(KeyCode.Space))
        {
            chargingShot = true;

            shotDone = true;
        }

        if(chargingShot)
        {
            alpha += oscilationSpeed* Time.deltaTime;

            if(alpha > Mathf.PI* 2.0f)
            {
                alpha -= Mathf.PI* 2.0f;
            }

            trueForce = Mathf.Abs(maxForce* Mathf.Sin(alpha));

            //this will make sure that the ball wont get stuck in a platform when it has already been shot
            if(trueForce < maxForce * 0.15f)
            {
                trueForce = maxForce * 0.15f;
            }

            float lambda = trueForce / maxForce;
            mat.color = Color.Lerp(Color.white, Color.blue, lambda);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Vector3 force = firingDirection * trueForce;

                rigid.AddForce(force);

                chargingShot = false;
            }
        }
    }

    private void ResetPosition()
    {
        foreach(GameObject plat in platforms)
        {
            plat.GetComponent<PlatformScript>().ResetPlatform();
        }

        transform.position = initialPosition;
        PlatformHit();
    }

    // Start is called before the first frame update
    void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");

        mat.color = Color.white;
        initialPosition = transform.position;
        firingDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if(!finishGame)
        {
            if (!isAsleep)
            {
                HandleInput();
            }
            else
            {
                if (sleepTimer > sleepDuration)
                {
                    isAsleep = false;
                    sleepTimer = 0.0f;
                }
                else
                {
                    sleepTimer += Time.deltaTime;
                }
            }

            //if the ball trespasses ceertain height game will reset
            if (transform.position.y < resetHeight)
            {
                ResetPosition();
            }
        }
        else
        {
            if (endTimer > endDuration)
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                endTimer += Time.deltaTime;


                //camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z + ORBIT_VEL);

            }
        }

    }
}
