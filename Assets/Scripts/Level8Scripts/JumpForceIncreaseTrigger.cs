using UnityEngine;

public class JumpForceIncreaseTrigger : MonoBehaviour
{
     public PlayerController player;
     public GameObject targetObject;

     void OnTriggerEnter(Collider other)
     {
        if(other.CompareTag("Player"))
        {
            player.jumpForce = 1f;
            BoxCollider box = targetObject.GetComponent<BoxCollider>();
            if(box != null)
            {
               box.size = Vector3.zero;
               box.center = Vector3.zero;
            }
        }
     }
}
