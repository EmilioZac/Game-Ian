using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
   private float topBound = 50;
    private float lowerBound = -50;
    public GameObject explosionPrefab;
    private GameManager gameManagerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z<lowerBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z>topBound)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Enemy")){
        Instantiate(explosionPrefab, other.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy (other.gameObject);
        gameManagerScript.IncrementKillCount();
        gameManagerScript.IncrementCoinCount();
        }
        
        
    }
}
