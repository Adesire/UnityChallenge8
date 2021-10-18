using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnClickEvents : MonoBehaviour
{
    public TextMeshProUGUI soundsText;
    // Start is called before the first frame update
    void Start()
    {
        soundsText.text = GameManager.mute ? "/" : "";
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ToggleMute()
    {
        if (GameManager.mute)
        {
            GameManager.mute = false;
            soundsText.text = "";
        }
        else
        {
            GameManager.mute = true;
            soundsText.text = "/";
        }
    }
}
