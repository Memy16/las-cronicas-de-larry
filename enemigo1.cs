using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicEnemy : MonoBehavior
{
    public float speed = 2.0f;
    public float attackRange = 1.0f;
    public float attackDamage = 10.0f;
    private Transform player;
    private Animator animator;
    void Start()
    {
        player = GameObjet.FindGameObjetWithTag("Player")
        animator = GetComponent<Animator>();
    }
    void Uptade()
    {
        //Esto calcula la distancia entre el enemigo y Larry.
        float distance = Vector3.Distance(transform.position, player.position);
        //Si el jugador esta dentro del alcance de Larry lo ataca.
        if (distance <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
        //Si no lo sigue.
        else
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //Si el enemigo choca con Larry, lo daña.
        if (collision.gameObjet.tag == "Player")
        {
            collision.gameObjet.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}