/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy1 : MonoBehaviour
{
    public float speed = 20f;
    public float attackRange = 1.0f;
    public float attackDamage = 10.0f;
    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calcula la distancia entre el enemigo y el jugador.
        float distance = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del alcance de ataque, ataca.
        if (distance <= attackRange)
        {
            // Aquí puedes agregar la lógica para realizar un ataque.
        }
        // Si no, sigue al jugador.
        else
        {
            // Asegúrate de que el enemigo no esté mirando directamente al jugador.
            transform.LookAt(player);

            // Solo mueve al enemigo si la distancia es mayor que el rango de ataque.
            if (distance > attackRange)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrulla del enemigo
    public float moveSpeed = 3.0f; // Velocidad de movimiento
    public float chaseSpeed = 5.0f; // Velocidad de persecución
    public float detectionRange = 5.0f; // Rango de detección del jugador

    private Transform player; // Referencia al jugador
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador por etiqueta
        MoveToNextPatrolPoint();
    }

    void Update()
    {
        if (!isChasing)
        {
            Patrol();
            DetectPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        // Mueve al enemigo al siguiente punto de patrulla
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, moveSpeed * Time.deltaTime);

        // Si llegamos al punto de patrulla, cambia al siguiente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void Patrol()
    {
        MoveToNextPatrolPoint();
    }

    void DetectPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {
            isChasing = true;
        }
    }

    void ChasePlayer()
    {
        // Mueve al enemigo hacia la posición del jugador
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }
}



/*
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackRange;
    public float chaseSpeed;
    public float patrolSpeed;
    public float stopDistance;
    public bool isChasing;

    private UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
            isChasing = true;
        }
        else if (isChasing)
        {
            agent.speed = patrolSpeed;
            isChasing = false;
        }
    }
}

*/