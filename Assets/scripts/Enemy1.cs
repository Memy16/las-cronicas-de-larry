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