using UnityEngine;

public class Billboard : MonoBehaviour
{

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
