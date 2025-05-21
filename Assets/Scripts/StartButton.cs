using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    private GameManager gameManager;
    public GameObject TitleScreen;
    
    public Button startButton; // Referencia al componente Button del objeto del botón

    private void Start()
    {
        // Asocia la función OnStartButtonClick() al evento onClick del botón
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        // Coloca aquí el código para inicializar el juego
        Debug.Log("Funcionando");

        // Desactiva el objeto del botón
        TitleScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
