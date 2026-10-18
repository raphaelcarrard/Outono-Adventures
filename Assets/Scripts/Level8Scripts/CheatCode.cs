using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatCode : MonoBehaviour
{

    public Button btnCheck, btnClose;
    public TMP_InputField inputCode;
    public TextMeshProUGUI errorText;
    public GameObject textAnswer, btnResume, btnMenu, btnCheat;
    public string myText;
    public GameObject player;
    public Transform point;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnCheck.onClick.AddListener(Teletransport);
    }

    public void OpenCheatFunction()
    {
        btnResume.SetActive(false);
        btnMenu.SetActive(false);
        btnCheat.SetActive(false);
        textAnswer.SetActive(true);
        inputCode.gameObject.SetActive(true);
        btnCheck.gameObject.SetActive(true);
        btnClose.gameObject.SetActive(true);
        errorText.text = "";
    }
    
    public void CloseCheatFunction()
    {
        btnResume.SetActive(true);
        btnMenu.SetActive(true);
        btnCheat.SetActive(true);
        textAnswer.SetActive(false);
        inputCode.gameObject.SetActive(false);
        btnCheck.gameObject.SetActive(false);
        btnClose.gameObject.SetActive(false);
        errorText.text = "";
    }

    public void Teletransport()
    {
       myText = inputCode.text;
       if(myText == "tadc" || myText == "TADC"){
           btnResume.SetActive(true);
           btnMenu.SetActive(true);
           btnCheat.SetActive(true);
           textAnswer.SetActive(false);
           inputCode.gameObject.SetActive(false);
           btnCheck.gameObject.SetActive(false);
           btnClose.gameObject.SetActive(false);
           errorText.text = "";
           player.transform.position = point.position;
           PauseManager.instance.ResumeGame();
       }
       else
       {
           errorText.text = "Incorrect";
       }
    }
}
