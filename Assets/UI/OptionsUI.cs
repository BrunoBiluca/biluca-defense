using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    Transform musicSubmenu;
    Transform soundSubmenu;

    [SerializeField]
    private AudioSource musicManager;

    void Awake() {
        musicSubmenu = transform.Find("optionsPanel").Find("musicSubmenu");
        musicSubmenu.Find("musicVolumePlus").GetComponent<Button>().onClick.AddListener(() => {
            var currentValue = musicManager.volume += .1f;
            musicSubmenu
                .Find("musicCurrentValue")
                .GetComponent<TMP_Text>().text = ((int)(currentValue*10)).ToString();
        });
        musicSubmenu.Find("musicVolumeMinus").GetComponent<Button>().onClick.AddListener(() => {
            var currentValue = musicManager.volume -= .1f;
            musicSubmenu
                .Find("musicCurrentValue")
                .GetComponent<TMP_Text>().text = ((int)(currentValue*10)).ToString();
        });

        soundSubmenu = transform.Find("optionsPanel").Find("soundSubmenu");
        soundSubmenu.Find("volumePlus").GetComponent<Button>().onClick.AddListener(() => {
            var currentValue = SoundManager.Instance.IncreaseVolume(.1f);
            soundSubmenu
                .Find("currentValue")
                .GetComponent<TMP_Text>().text = ((int)(currentValue*10)).ToString();
        });
        soundSubmenu.Find("volumeMinus").GetComponent<Button>().onClick.AddListener(() => {
            var currentValue = SoundManager.Instance.DecreaseVolume(.1f);
            soundSubmenu
                .Find("currentValue")
                .GetComponent<TMP_Text>().text = ((int)(currentValue*10)).ToString();
        });
    }

    public void ToggleDisplay() {
        gameObject.SetActive(!gameObject.activeSelf);

        if(gameObject.activeSelf) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }
}
