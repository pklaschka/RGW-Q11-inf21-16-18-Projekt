using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class Bestenliste : MonoBehaviour
{
    private static string serverAdress = "pi";

    public VerticalLayoutGroup scoreList;
    public Text ownScore;

    private int score;
    private string scoreConfirmation;

    private void Start()
    {
        score = PlayerPrefs.GetInt("maxEndlosweite", 0);
        scoreConfirmation = PlayerPrefs.GetString("maxEndlosweiteConfirmation", "");
    }

    public void Submit(string playerName)
    {
        // TODO: Implement submitting score
        print("Submitting score: " + score + " (" + playerName + ")");
    }
}