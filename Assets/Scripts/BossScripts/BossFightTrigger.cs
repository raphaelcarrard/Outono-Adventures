using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public BossCutsceneManager cutscene;
    bool activated = false;

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
        activated = true;
        cutscene.StartCutscene();
        gameObject.SetActive(false);
    }
}
