using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed = 20f; // Velocidad del proyectil
    public GameObject proyectilePrefab;
    public float fireCooldown = 0.5f; // Tiempo entre disparos
    private float fireTimer = 0f;

    public Transform firePoint; // Punto desde donde se dispara
    public Animator animator;   // Animator del personaje

    [Tooltip("Retraso en segundos entre el inicio de la animación y el disparo")]
    public float fireDelay = 0.3f; // Este valor lo ajustas en el Inspector

    void Update()
    {
        // Disparo al presionar la tecla E
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= fireTimer)
        {
            ShootWithDelay();
            fireTimer = Time.time + fireCooldown;

            // Activar la animación de ataque
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    void ShootWithDelay()
    {
        // Llama al método ShootAfterDelay después de 'fireDelay' segundos
        Invoke("Shoot", fireDelay);
    }

    void Shoot()
    {
        // Verificar que el firePoint esté asignado
        if (firePoint == null)
        {
            Debug.LogError("FirePoint no asignado.");
            return;
        }

        // Obtener la dirección local del firePoint
        Vector3 shootDirection = firePoint.TransformDirection(Vector3.forward);

        // Posición del disparo
        Vector3 spawnPosition = firePoint.position;

        // Instanciar el proyectil y hacer que rote según la dirección del firePoint
        GameObject projectile = Instantiate(proyectilePrefab, spawnPosition, Quaternion.LookRotation(shootDirection, Vector3.up));

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * speed;
        }
    }
}