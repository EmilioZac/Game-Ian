using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed = 20f; // Velocidad del proyectil
    public GameObject proyectilePrefab;
    public float fireCooldown = 0.5f; // Tiempo entre disparos
    private float fireTimer = 0f;

    public Transform firePoint; // Punto desde donde se dispara (con rotación local correcta)

    void Update()
    {
        // Disparo al presionar la tecla E
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= fireTimer)
        {
            Shoot();
            fireTimer = Time.time + fireCooldown;
        }

        // Movimiento lateral (ejemplo básico)
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal * 5f * Time.deltaTime, Space.World);
    }

    void Shoot()
    {
        // Verificar que el firePoint esté asignado
        if (firePoint == null)
        {
            Debug.LogError("FirePoint no asignado.");
            return;
        }

        // Obtener la dirección local del firePoint (en lugar de .forward que es global)
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