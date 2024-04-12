using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        Screen.SetResolution(1080, 1920, true);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        gameObject.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y +2, -10);
    }
}
