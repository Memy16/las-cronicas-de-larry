using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float stopDistance = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private int currentPatrolPoint = 0;
    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Patrol());
    }

    void Update()
    {
        if (!isChasing)
        {
            Patrol();
            LookForPlayer();
        }
        else
        {
            ChaseAndShoot();
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            Vector3 target = patrolPoints[currentPatrolPoint].position;
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
                yield return null;
            }

            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void LookForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < stopDistance)
        {
            isChasing = true;
        }
    }

    void ChaseAndShoot()
    {
        if (Vector3.Distance(transform.position, player.position) < stopDistance)
        {
            isChasing = false; // Deja de perseguir si el jugador está demasiado cerca
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Dispara solo si no hay proyectiles existentes
        if (!IsShooting())
        {
            
            Shoot();
        }
    }

    void Shoot()
{
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    bullet.transform.parent = transform; // Establece el objeto padre del proyectil al objeto que dispara
    Destroy(bullet, 2.0f); // Destruye el proyectil después de 2 segundos
}

    bool IsShooting()
    {
        // Verifica si hay proyectiles hijos en el transform del enemigo
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }
    
}