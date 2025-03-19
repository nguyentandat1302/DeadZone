using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent navAgent;


    public bool isDead;
    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            // Ngừng di chuyển
            navAgent.enabled = false; 

            // Chọn ngẫu nhiên animation chết
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }

            isDead = true;
            SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieDeath);

            // Bắt đầu Coroutine để chờ animation kết thúc
            StartCoroutine(DestroyAfterDeath());


        }
        else
        {
            animator.SetTrigger("DAMAGE");
            navAgent.enabled = false;

            SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieHurt);

        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        // Đợi thời gian dài nhất của animation chết (chỉnh lại thời gian phù hợp)
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f);
    }
}
