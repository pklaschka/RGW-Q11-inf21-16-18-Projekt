using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Bestenliste : MonoBehaviour
{
    public static string serverAdress = "http://bestenliste:meineGemeineBestenliste@inf21platformer.pabloklaschka.de";

    private static string best5Adress = "getBestFive.php";
    private static string rankAdress = "getRank.php?score=";
    private static string submitAdress = "submitScore.php";

    public Text bestenliste;
    public Text ownScore;
    public InputField nameInput;
    public Button btnSubmit;

    private int score;
    private string scoreConfirmation;

    private void Start()
    {
        btnSubmit = GetComponent<Button>();
        
        score = PlayerPrefs.GetInt("maxEndlosweite", 0);
        scoreConfirmation = PlayerPrefs.GetString("maxEndlosweiteConfirmation", "");
        
        StartCoroutine(SetupBestenliste());

        StartCoroutine(GetRank());
    }

    public IEnumerator Submit()
    {
        // TODO: Implement submitting score
        print("Submitting score: " + score + " (" + nameInput.text + ")");
        WWWForm form = new WWWForm();
        form.AddField("name", nameInput.text);
        form.AddField("score", score);
        form.AddField("scoreConfirmationString", scoreConfirmation);
        var myWr = UnityWebRequest.Post(serverAdress + "/" + submitAdress, form);

        yield return myWr.Send();

        if (myWr.isError)
        {
            Debug.LogError("Submit has failed.");
        }
        else
        {
            PlayerPrefs.SetInt("maxSubmitedEndlosweite", score);
            CheckSubmitable();
        }
    }

    private IEnumerator GetRank()
    {
        var myWr = UnityWebRequest.Get(serverAdress + "/" + rankAdress + score);
        yield return myWr.Send();
        
        if(myWr.isError) {
            Debug.LogError(myWr.error);
        }
        else {
            ownScore.text = "Ihr Ergebnis:\n" +
                            score + " (#" + myWr.downloadHandler.text + ")";
            
        }
    }

    private IEnumerator SetupBestenliste()
    {
        bestenliste.text = "<b>Die Besten:</b>\n";
        var myWr = UnityWebRequest.Get(serverAdress + "/" + best5Adress);
        yield return myWr.Send();
        
        if(myWr.isError) {
            Debug.LogError(myWr.error);
        }
        else {
            bestenliste.text = "<b>Die Besten:</b>\n" + myWr.downloadHandler.text;
        }
    }

    public void CheckSubmitable()
    {
        var confirmationString = "confirming" + score + "-1-75_13+" + (score * score) % (score * 3 / 2);

        if (confirmationString == scoreConfirmation)
        {
            if (nameInput.text.Length > 0 && nameInput.text.Length <= 100)
            {
                if (score > PlayerPrefs.GetInt("maxSubmitedEndlosweite", 0))
                {
                    btnSubmit.GetComponentInChildren<Text>().text = "Veröffentlichen";
                    btnSubmit.interactable = true;
                }
                else
                {
                    btnSubmit.interactable = false;
                    btnSubmit.GetComponentInChildren<Text>().text = "Bereits veröffentlicht!";
                }
            }
            else
            {
                btnSubmit.interactable = false;
                btnSubmit.GetComponentInChildren<Text>().text = "Ungültiger Name!";
            }
        }
        else
        {
            btnSubmit.interactable = false;
            btnSubmit.GetComponentInChildren<Text>().text = "Cheater!";
        }
    }

    public void OpenBestenliste()
    {
        Application.OpenURL(serverAdress + "/bestenliste.php");
    }
}