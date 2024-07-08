using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [SerializeField] Transform targetDestination;
    Rigidbody2D rb;
    public bool test;
    private Coroutine damageCoroutine;
    private int countTargets;

    public Animator animator;
    public AudioSource attackClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetDestination = FindObjectOfType<Building>()?.transform;
    }

    private void Start()
    {
        if (test) return;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
        transform.rotation = spawnPoint[randomSpawnPoint].transform.rotation;
    }

    private void FixedUpdate()
    {
        //print(transform.forward + "forward");
        if (targetDestination != null)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 destination = Vector3.zero;
        //Vector3 destination = transform.forward + -transform.position;


        if (transform.rotation.z == 0)
        {

            destination.x = transform.forward.z;
        }
        else
        {
            destination.y = -transform.position.y + transform.forward.z;
        }

       
        // Normaliza la dirección para asegurar que tiene una magnitud de 1
       
        destination = destination.normalized;

        // Ajusta la velocidad del Rigidbody hacia la dirección seleccionada
        rb.velocity = destination * enemyData.MoveSpeed;
     

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //attackClip.Play();

            animator.SetTrigger("Attack");
            countTargets++;
            PlayerStats target = collision.gameObject.GetComponent<PlayerStats>();
            target.SetDamage(enemyData.Damage);
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(target));
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            //attackClip.Play();

            animator.SetTrigger("Attack");
            countTargets++;
            Building target = collision.gameObject.GetComponent<Building>();
            target.SetDamage(enemyData.Damage);
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(target));
        }
        if (collision.gameObject.CompareTag("Barrier"))
        {
            countTargets++;
            DefenceBarrera target = collision.gameObject.GetComponent<DefenceBarrera>();
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(target));
        }
     
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            PlayerStats target = collision.gameObject.GetComponent<PlayerStats>();
            target.SetDamage(-enemyData.Damage);
            countTargets--;
            if (damageCoroutine != null)
            {

                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

        }
        if (collision.gameObject.CompareTag("Building"))
        {
            //AudioManager.instance.Stop("Chainsaw");
            Building target = collision.gameObject.GetComponent<Building>();
            target.SetDamage(-enemyData.Damage);
            countTargets--;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
        if (collision.gameObject.CompareTag("Barrier"))
        {
            countTargets--;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }

    }

    IEnumerator ApplyDamageOverTime(IDamageable target)
    {
        while (countTargets > 0)
        {
            if (countTargets == 0) yield break;
            target.TakeDamage();
            yield return new WaitForSeconds(1.5f);

        }
    }


}
