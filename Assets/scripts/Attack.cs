using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform Attackcheck;

    [SerializeField] private float hitrange;

    [SerializeField] private float hitdamage;

    [SerializeField] private float timeforattack;

    [SerializeField] private float timefornextattack;

    private void Update()
    {
        if (timefornextattack > 0)
        {
            timefornextattack -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && timefornextattack <= 0)
        {
            Golpe();
            timefornextattack = timeforattack;

        }
    }

    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(Attackcheck.position, hitrange);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {


            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackcheck.position, hitrange);
    }

}
