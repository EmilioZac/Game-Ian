using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject player;
    public float distanceToDestroy = 3.0f;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
{
    // Comprueba la distancia entre el jugador y el enemigo
    float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

    // Si el jugador está lo suficientemente cerca, destruye el enemigo
    if (distanceToPlayer <= distanceToDestroy)
    {
        Destroy(gameObject);
        gameManagerScript.IncrementKillCount();
        gameManagerScript.IncrementCoinCount();
    }
}
}
