using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject GameOver;
    public static GameOverManager gameOverManager;
    // Start is called before the first frame update
    void Start()
    {
        gameOverManager = this;
        // Asegurar que GameOver está desactivado al inicio
        if (GameOver != null)
        {
            GameOver.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallGameOver(){
        if (GameOver != null)
        {
            GameOver.gameObject.SetActive(true); // Activa la pantalla de Game Over
            Debug.Log("Game Over activado");
        }
        else
        {
            Debug.LogError("GameOver no está asignado en el Inspector.");
        }

    }
}
