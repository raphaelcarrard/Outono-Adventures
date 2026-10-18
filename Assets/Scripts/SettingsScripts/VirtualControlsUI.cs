using UnityEngine;

public class VirtualControlsUI : MonoBehaviour
{
    public GameObject virtualControls;

    
    void Update()
    {
        if (PlayerPrefs.GetInt("ShowVirtualControls", 0) == 0)
        {
            virtualControls.SetActive(false);
            return;
        }
        virtualControls.SetActive(true);
    }
}
