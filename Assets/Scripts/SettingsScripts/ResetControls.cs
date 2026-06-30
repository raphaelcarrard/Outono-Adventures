using UnityEngine;
using UnityEngine.InputSystem;

public class ResetControls : MonoBehaviour
{

    public InputActionAsset inputActions;

    public void ResetAllBindings()
    {
        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                action.RemoveAllBindingOverrides();
            }
        }
        PlayerPrefs.DeleteKey("rebinds");
        KeyRebindUI[] rebindingUI = FindObjectsOfType<KeyRebindUI>();
        foreach (var ui in rebindingUI)
        {
            ui.RefreshUI();
        }
    }
}
