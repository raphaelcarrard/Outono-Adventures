using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ObjectiveText : MonoBehaviour
{
    public int bindingIndex;
    public TextMeshProUGUI messageText;
    public InputActionReference kickAction;

    void Update()
    {
        string key = kickAction.action.GetBindingDisplayString(bindingIndex);
        if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
        {
             messageText.text = "Defeat all enemies by using the Throw Ball button to shoot circus balls at them, and grab the key after completing the entire circuit to win the level.";
        }
        else if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
        {
             messageText.text = "Defeat all enemies by using the " + key + " key to shoot circus balls at them, and grab the key after completing the entire circuit to win the level.";
        }
    }
}
