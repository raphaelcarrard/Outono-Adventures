using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ObjectiveText : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public InputActionReference kickAction;

    void Update()
    {
        string key = kickAction.action.GetBindingDisplayString();
        messageText.text = "Defeat all enemies by using the " + key + " key to shoot circus balls at them, and grab the key after completing the entire circuit to win the level.";
    }
}
