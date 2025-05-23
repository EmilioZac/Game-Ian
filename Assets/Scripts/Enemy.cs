using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Configuración del Enemigo")]
    public float speed = 3.0f;
    public float attackRange = 2.0f;
    public float visionRange = 10.0f;
    public float fieldOfViewAngle = 90f;
    public int totalAttackAnimations = 2;
    public float attackDuration = 1.5f; // Duración del ataque (segundos)

    private GameObject player;
    private Transform playerTransform;
    private Animator animator;
    private bool isPlayerInVision = false;
    private bool isAttacking = false;

    private const int ANIM_IDLE = 0;
    private const int ANIM_WALK = 1;
    private const int ANIM_ATTACK = 2;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
            playerTransform = player.transform;

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("No se encontró un Animator en el enemigo.");

        SetAnimation(ANIM_IDLE);
    }

    void Update()
    {
        if (isAttacking) return; // No hacer nada mientras está atacando

        if (player == null) return;

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Verificar si el jugador está dentro del rango de visión
        if (distanceToPlayer <= visionRange)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer.normalized);

            if (angle < fieldOfViewAngle / 2f)
            {
                isPlayerInVision = true;
                transform.LookAt(playerTransform);

                if (distanceToPlayer <= attackRange)
                {
                    // Iniciar ataque
                    int attackAnim = Random.Range(1, totalAttackAnimations + 1);
                    StartCoroutine(Attack(attackAnim));
                    return;
                }
                else
                {
                    SetAnimation(ANIM_WALK);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
        }
        else
        {
            isPlayerInVision = false;
            SetAnimation(ANIM_IDLE);
        }
    }

    IEnumerator Attack(int attackIndex)
    {
        isAttacking = true;

        // Activar animación de ataque
        SetAnimation(ANIM_ATTACK, attackIndex);

        // Aquí puedes disparar daño, sonido, etc.
        Debug.Log("¡Enemigo atacando!");

        // Pausa por la duración del ataque
        yield return new WaitForSeconds(attackDuration);

        // Volver a revisar si el jugador sigue en visión
        if (isPlayerInVision && player != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer > attackRange)
            {
                SetAnimation(ANIM_WALK);
            }
            else
            {
                SetAnimation(ANIM_IDLE);
            }
        }
        else
        {
            SetAnimation(ANIM_IDLE);
        }

        isAttacking = false;
    }

    void SetAnimation(int state, int attackIndex = 0)
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);

        switch (state)
        {
            case ANIM_IDLE:
                animator.SetBool("isIdle", true);
                break;
            case ANIM_WALK:
                animator.SetBool("isWalking", true);
                break;
            case ANIM_ATTACK:
                animator.SetInteger("AttackIndex", attackIndex);
                animator.SetBool("isAttacking", true);

                break;
        }
    }

    // ====== GIZMOS PARA VISUALIZAR CAMPO DE VISION Y RANGO DE ATAQUE ======
    void OnDrawGizmos()
    {
        // Solo dibuja si estamos en modo edición
        if (!Application.isPlaying) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 viewAngleLine1 = Quaternion.AngleAxis(fieldOfViewAngle / 2, Vector3.up) * transform.forward * visionRange;
        Vector3 viewAngleLine2 = Quaternion.AngleAxis(-fieldOfViewAngle / 2, Vector3.up) * transform.forward * visionRange;

        Gizmos.DrawLine(transform.position, transform.position + viewAngleLine1);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleLine2);

        DrawFieldOfViewArc(transform.position, transform.forward, visionRange, fieldOfViewAngle);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void DrawFieldOfViewArc(Vector3 center, Vector3 direction, float radius, float angle)
    {
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(angle / 2, Vector3.up));
        Vector3 from = rotationMatrix.MultiplyPoint(direction);
        from = from.normalized * radius + center;

        int segments = 20;
        float currentAngle = 0f;
        float deltaAngle = angle / segments;
        Vector3 lastPoint = from;

        for (int i = 0; i <= segments; i++)
        {
            Matrix4x4 m = Matrix4x4.Rotate(Quaternion.AngleAxis(currentAngle, Vector3.up));
            Vector3 nextPoint = m.MultiplyPoint(from - center) + center;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
            currentAngle += deltaAngle;
        }
    }
}