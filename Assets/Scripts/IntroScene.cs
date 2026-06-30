using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    public GameObject RaphaLogo, PresentsText, OCText, PikaVaniPFP, PikaVaniText;

    void Start()
    {
        StartCoroutine(LoadProgress());
    }

    IEnumerator LoadProgress()
    {
        yield return new WaitForSeconds(1f);
        RaphaLogo.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        PresentsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        RaphaLogo.gameObject.SetActive(false);
        PresentsText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        OCText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        PikaVaniPFP.gameObject.SetActive(true);
        PikaVaniText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        OCText.gameObject.SetActive(false);
        PikaVaniPFP.gameObject.SetActive(false);
        PikaVaniText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title");
    }
}
