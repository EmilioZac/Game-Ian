using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTime : MonoBehaviour
{
    public float tiempo = 1f;

    void Start()
    {
        Destroy(gameObject, tiempo);
    }
    
}
