using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public Transform escapePanel;
    public Transform sensetivityNumberText;
    public Slider slider;
    public bool isOpened = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isOpened = !isOpened;
            escapePanel.gameObject.SetActive(isOpened);
            CharacterAbilities.SetState(!isOpened);
            CharacterAbilities.ChangeCursor(isOpened);
        }
        sensetivityNumberText.GetComponent<TMP_Text>().text = slider.value.ToString();
    }
}
