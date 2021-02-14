using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {
    public static TooltipUI Instance;

    public RectTransform canvasRectTransform;

    [SerializeField]
    private bool followMouse;

    private TextMeshProUGUI text;
    private RectTransform rectTransform;
    private RectTransform background;

    private TooltipTimer tooltipTimer;

    private void Awake() {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();
        background = transform.Find("background").GetComponent<RectTransform>();

        text = transform.Find("text").GetComponent<TextMeshProUGUI>();

        Hide();
    }

    private void Update() {
        if(followMouse) {
            var anchoredPosition = Input.mousePosition;
            if(anchoredPosition.x + background.rect.width * 2 > canvasRectTransform.rect.width) {
                anchoredPosition.x = canvasRectTransform.rect.width - background.rect.width * 2;
            }
            if(anchoredPosition.y + background.rect.height > canvasRectTransform.rect.height) {
                anchoredPosition.y = canvasRectTransform.rect.height - background.rect.height;
            }
            rectTransform.anchoredPosition = anchoredPosition;
        }

        if(tooltipTimer != null) {
            tooltipTimer.currentTimer += Time.deltaTime;
            if(tooltipTimer.currentTimer > tooltipTimer.Timer) {
                tooltipTimer = null;
                Hide();
            }
        }
    }

    private void SetText(string value) {
        text.SetText(value);
        text.ForceMeshUpdate();

        var padding = new Vector2(8, 8);
        background.sizeDelta = text.GetRenderedValues(false) + padding;
    }

    public void Show(string tooltipText, TooltipTimer tooltipTimer = null) {
        gameObject.SetActive(true);
        SetText(tooltipText);

        this.tooltipTimer = tooltipTimer;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public class TooltipTimer {
        private float timer;
        public float Timer { 
            get { return timer; } 
            set {
                if(value <= 0) throw new ArgumentException();
                timer = value; 
            }
        }

        public float currentTimer = 0f;
    }
}
