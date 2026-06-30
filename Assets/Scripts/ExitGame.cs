using System.Collections;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("timeToExitGame");
    }

    IEnumerator timeToExitGame()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Software Terminated");
        Application.Quit();
    }
}
