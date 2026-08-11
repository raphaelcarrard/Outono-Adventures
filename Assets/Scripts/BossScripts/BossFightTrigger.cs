using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public BossCutsceneManager cutscene;
    bool activated = false;
    public MonoBehaviour pauseManagerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            return;
        }
        if (!other.CompareTag("Player"))
        {
            return;
        }
        CursorController.instance.ShowCursor();
        pauseManagerScript.enabled = false;
        activated = true;
        cutscene.StartCutscene();
        gameObject.SetActive(false);
    }
}
