using UnityEngine;

public class BallerineChaser : MonoBehaviour
{

    [Header("Player")]
    public Transform player;

    [Header("Config")]
    public AudioSource audioSource;

    [Header("Movement")]
    public float speed = 3f;
    public float detectionRange = 10f;
    public float stopDistance = 1.5f;

    [Header("Components")]
    public Animator anim;


    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > detectionRange)
        {
            anim.SetBool("isChasing", false);
            return;
        }
        anim.SetBool("isChasing", true);
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        if (lookPos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
        if (distance > stopDistance)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.isDead)
            {
                player.TakeDamage(5);
            }
        }
    }
}
