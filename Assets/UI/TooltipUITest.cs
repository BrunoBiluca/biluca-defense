using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipUITest : MonoBehaviour {

    [SerializeField]
    private string text;

    void Start() {
        text = "Hello TooltipUI";
        TooltipUI.Instance.Show(text);
    }

    // Update is called once per frame
    void Update() {
        TooltipUI.Instance.Show(text);
    }
}
