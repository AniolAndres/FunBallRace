using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offSet;
    public GameObject player;
    public float speed;

    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        transform.position = player.transform.position + offSet;
        transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerScript.GetFinish())
        {
            transform.position = player.transform.position + offSet;
        }
        else
        {
            transform.LookAt(player.transform.position);
            transform.Translate(speed * Vector3.right * Time.deltaTime);
        }
    }
}
