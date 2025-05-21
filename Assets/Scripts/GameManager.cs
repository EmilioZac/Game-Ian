using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI killText;
    public int kill;
    public TextMeshProUGUI CoinText;
    public int Coin;
    public TextMeshProUGUI GameoverText;
    
    public Button restartButton;
   
    
    public bool isGameActive = false; // Inicialmente el juego está inactivo
    public Button startButton;
    
    public GameObject titleScreen;
    public FireController fireController;
    // Start is called before the first frame update
    void Start()
    {
        killText.text = "kill: "+kill;
        CoinText.text = "Coin: "+Coin;
        startButton.onClick.AddListener(StartGame);
        isGameActive = false;
        restartButton.onClick.AddListener(RestartGame);
        // Obtener referencia al fireController  por nombre del objeto
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            fireController = playerObject.GetComponent<FireController>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto del FireController en la escena.");
        }
    }
    public void StartGame()
    {
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    
    public void GameOver()
    {
        

        if (fireController.currentHealth >= 6)
        {
            // Acciones adicionales cuando el jugador tiene 0 vidas
            GameoverText.gameObject.SetActive(true);
            isGameActive = false;
            restartButton.gameObject.SetActive(true);
        }
    }
    
    public void IncrementKillCount()
    {
        kill++; // Incrementa el contador de kills
        UpdateKillCount(); // Actualiza el texto en pantalla
    }

    void UpdateKillCount()
    {
        killText.text = "Kill: " + kill.ToString();
    }
    public void IncrementCoinCount()
    {
        Coin++;
        Coin++;
        Coin++;
        Coin++;
        Coin++; // Incrementa el contador de monedas
        UpdateCoinCount(); // Actualiza el texto en pantalla
    }

    void UpdateCoinCount()
    {
        CoinText.text = "Coin: " + Coin.ToString();
    
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
