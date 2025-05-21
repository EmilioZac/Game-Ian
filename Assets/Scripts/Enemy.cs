using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private GameObject player;
    
    void Update ()
    {
        player = GameObject.Find("Player");
        if(player != null)
        {
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
