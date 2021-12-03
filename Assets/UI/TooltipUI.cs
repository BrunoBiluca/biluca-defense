using Assets.UnityFoundation.Code;
using System;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour {
    public static TooltipUI Instance;

    public RectTransform canvasRectTransform;

    [SerializeField]
    private bool followMouse;

    private TextMeshProUGUI text;
    private RectTransform rectTransform;

    private TooltipTimer tooltipTimer;

    private void Awake() {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();

        text = transform.Find("text").GetComponent<TextMeshProUGUI>();

        tooltipTimer = new TooltipTimer();
        tooltipTimer.Timer = 2f;

        Hide();
    }

    private void Update() {
        if(followMouse) {
            var newAnchoredPosition = new Vector2(
                Input.mousePosition.x.Remap(0f, Screen.width, 0f, 1280f),
                Input.mousePosition.y.Remap(0f, Screen.height, 0f, 720f)
            );
            var tooltipSizeX = newAnchoredPosition.x + rectTransform.rect.width;
            if(tooltipSizeX > canvasRectTransform.rect.width) {
                newAnchoredPosition.x = canvasRectTransform.rect.width - rectTransform.rect.width;
            }
            var tooltipeSizeY = newAnchoredPosition.y + rectTransform.rect.height;
            if(tooltipeSizeY > canvasRectTransform.rect.height) {
                newAnchoredPosition.y = canvasRectTransform.rect.height - rectTransform.rect.height;
            }
            rectTransform.anchoredPosition = newAnchoredPosition;
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
        rectTransform.sizeDelta = text.GetRenderedValues(false) + padding;
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
