using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar02 : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;


    public GameOverManager gameOverManager;

    private int currentHealth;
    private FireController fireControllerScript;

void Start()
{
    fireControllerScript = GameObject.Find("Player").GetComponent<FireController>();
}

void Update()
{
    if (fireControllerScript != null)
    {
        currentHealth = fireControllerScript.GetHealth();
        object1.SetActive(currentHealth == 1);
        object2.SetActive(currentHealth == 2);
        object3.SetActive(currentHealth == 3);
        object4.SetActive(currentHealth == 4);
        object5.SetActive(currentHealth == 5);
        object6.SetActive(currentHealth == 6);
        if (currentHealth == 6)
            {
                gameOverManager.CallGameOver(); // Activa la pantalla de Game Over
                Debug.Log("Game Over activado!");
            }
        else{
            Debug.Log("No se esta llamando");
        }
    }
}
}
