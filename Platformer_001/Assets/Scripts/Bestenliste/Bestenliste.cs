using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class Bestenliste : MonoBehaviour
{
    private static string serverAdress = "pi";

    public VerticalLayoutGroup scoreList;
    public Text ownScore;
    public InputField nameInput;

    private int score;
    private string scoreConfirmation;

    private void Start()
    {
        score = PlayerPrefs.GetInt("maxEndlosweite", 0);
        scoreConfirmation = PlayerPrefs.GetString("maxEndlosweiteConfirmation", "");

        ownScore.text = "Ihr Ergebnis:\n" +
            score + " (#" + GetPlace() + ")";
    }

    public void Submit()
    {
        // TODO: Implement submitting score
        print("Submitting score: " + score + " (" + nameInput.text + ")");
    }

    private int GetPlace()
    {
        // TODO: Implementation of Rank
        return Math.Max(score * -1 + 2000, 1);
    }
}