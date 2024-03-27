using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI lapText;
    private Dictionary<CarIdentity, int> carLaps = new Dictionary<CarIdentity, int>();

    // Update lap text with car laps dictionary
    public void UpdateLapText(string message)
    {
        lapText.text = message;
    }
}
