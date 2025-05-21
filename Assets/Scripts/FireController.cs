using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del objeto
    public GameObject proyectilePrefab;
    public float HorizontalInput;
    public float fireCooldown = 2.0f; // Tiempo de enfriamiento en segundos
    private float fireTimer = 0.0f; // Tiempo transcurrido desde el último lanzamiento
    public int currentHealth = 6;
    private bool isCooldown = false; // Indica si está en tiempo de espera (cooldown)
    public Transform modelTransform; 
    public int GetHealth()
    {
        return currentHealth;
    }


    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isCooldown)
        {
            if (currentHealth > 0)
            {
                currentHealth++;
                Debug.Log("El jugador ha sido golpeado por un enemigo. Vida restante: " + currentHealth);

                StartCoroutine(StartCooldown(1f)); // Inicia el tiempo de espera (cooldown) de un segundo
            }
            else
            {
                Debug.Log("El jugador está fuera de vida. Fin del juego.");
                GameOverManager.gameOverManager.CallGameOver();
            }
        }
    }

    private IEnumerator StartCooldown(float cooldownTime)
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
      

    // Update is called once per frame
    
    void Update()
    {
        fireTimer += Time.deltaTime; 

        if (Input.GetKeyDown(KeyCode.E) && fireTimer >= fireCooldown) // Agregado el control de cooldown
        {
            // Dirección hacia donde está mirando el modelo del personaje
            Vector3 shootDirection = modelTransform.forward;

            // Posición elevada y ligeramente al frente del personaje
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f + shootDirection * 1.0f;

            // Instanciar proyectil
            GameObject proyectil = Instantiate(proyectilePrefab, spawnPosition, Quaternion.identity);

            // Aplicar velocidad si tiene Rigidbody
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * speed;
            }

            fireTimer = 0.0f;
        }


        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*Time.deltaTime*speed*HorizontalInput);
        
    }
}
