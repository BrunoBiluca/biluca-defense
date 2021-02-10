using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour {
    private TextMeshProUGUI text;

    private void Awake() {
        text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        SetText("Outro Hello");
    }

    private void SetText(string value) {
        text.text = value;
    }
}
