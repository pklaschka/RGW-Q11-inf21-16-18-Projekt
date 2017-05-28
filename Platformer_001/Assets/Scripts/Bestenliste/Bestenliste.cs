using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Bestenliste : MonoBehaviour
{
    public static string serverAdress = "http://bestenliste:meineGemeineBestenliste@pi/inf21platformer";

    private static string best5Adress = "getBestFive.php";
    private static string rankAdress = "getRank.php?score=";
    private static string submitAdress = "submitScore.php";

    public Text bestenliste;
    public Text ownScore;
    public InputField nameInput;

    private int score;
    private string scoreConfirmation;

    private void Start()
    {
        score = PlayerPrefs.GetInt("maxEndlosweite", 0);
        scoreConfirmation = PlayerPrefs.GetString("maxEndlosweiteConfirmation", "");
        
        SetupBestenliste();

        StartCoroutine(GetRank());
    }

    public void Submit()
    {
        // TODO: Implement submitting score
        print("Submitting score: " + score + " (" + nameInput.text + ")");
    }

    private IEnumerator GetRank()
    {
        // TODO: Implementation of Rank
        var myWr = UnityWebRequest.Get(serverAdress + "/" + rankAdress + score);
        //myWr.SetRequestHeader();
        yield return myWr.Send();
        
        if(myWr.isError) {
            Debug.LogError(myWr.error);
        }
        else {
            ownScore.text = "Ihr Ergebnis:\n" +
                            score + " (#" + myWr.downloadHandler.text + ")";
            
        }
    }

    private void SetupBestenliste()
    {
        bestenliste.text = "<b>Die Besten:</b>\n" +
                           "1. Karl (51683)\n" +
                           "2. Karl (31555)\n" +
                           "3. Karl (3013)\n" +
                           "4. Karl (687)\n" +
                           "5. Karl (311)\n";
    }

    public void OpenBestenliste()
    {
        Application.OpenURL(serverAdress + "/bestenliste.php");
    }
}