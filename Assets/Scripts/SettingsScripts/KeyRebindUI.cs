using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class KeyRebindUI : MonoBehaviour
{

    public InputActionReference actionReference;
    public int bindingIndex;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI warningText;
    InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Start()
    {
        LoadBinding();
        UpdateKeyText();
    }

    public void RefreshUI()
    {
        UpdateKeyText();
        warningText.text = "";
    }

    public void StartRebind()
    {
        warningText.text = "";
        keyText.text = "Press a key...";
        string oldBinding = actionReference.action.bindings[bindingIndex].effectivePath;
        rebindingOperation = actionReference.action.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(operation =>
            {
                operation.Dispose();
                if (CheckDuplicateKey())
                {
                    warningText.text = "Key already in use! choose another key...";
                    actionReference.action.ApplyBindingOverride(bindingIndex, oldBinding);
                }
                else
                {
                    SaveBinding();
                }
                UpdateKeyText();
            })
            .Start();
    }

    bool CheckDuplicateKey()
    {
        var actionMap = actionReference.action.actionMap;
        string newBinding = actionReference.action.bindings[bindingIndex].effectivePath;
        foreach (var action in actionMap.actions)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (action == actionReference.action && i == bindingIndex)
                {
                    continue;
                }
                if (action.bindings[i].effectivePath == newBinding)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void UpdateKeyText()
    {
        keyText.text = actionReference.action.GetBindingDisplayString(bindingIndex);
    }

    void SaveBinding()
    {
        string rebinds = actionReference.action.actionMap.asset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        PlayerPrefs.Save();
    }

    public void LoadBinding()
    {
        if (PlayerPrefs.HasKey("rebinds"))
        {
            string rebinds = PlayerPrefs.GetString("rebinds");
            actionReference.action.actionMap.asset.LoadBindingOverridesFromJson(rebinds);
        }
        UpdateKeyText();
    }
}
