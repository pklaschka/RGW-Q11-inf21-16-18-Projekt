using UnityEngine;
using UnityEngine.UI;

public enum NachrichtenArt {
    KEINE,
    DRUECKEN_ZUM_AUFHEBEN,
    FEHLER
}

public class UINachricht : MonoBehaviour {
    private float anzeigeDauer = 0.0f;
    private float anzeigeTimer = 0.0f;
    private string anzeigeText = "???";

    private NachrichtenArt art;

    private Text uiText;

    void Start() {
        uiText = GetComponent<Text>();
    }

    public void Anzeigen(NachrichtenArt art, string text, float dauer = 4.0f) {
        this.art = art;
        anzeigeText = text;
        anzeigeTimer = 0.0f;
        anzeigeDauer = dauer;
    }

    public void SofortAusblenden() {
        art = NachrichtenArt.KEINE;
        anzeigeText = "";
        anzeigeTimer = 0.0f;
        anzeigeDauer = 0.0f;
    }

    public NachrichtenArt ArtGeben() {
        return art;
    }

    void Update() {
        anzeigeTimer += Time.deltaTime;

        if (anzeigeTimer >= anzeigeDauer) art = NachrichtenArt.KEINE;

        if (uiText != null) {
            uiText.enabled = anzeigeTimer < anzeigeDauer;
            uiText.text = anzeigeText;
        }
    }
}
