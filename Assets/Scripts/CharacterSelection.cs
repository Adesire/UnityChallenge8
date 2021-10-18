using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;

    private int _selectedCharacter = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ch in characters)
        {
            ch.SetActive(false);
        }

        characters[_selectedCharacter].SetActive(true);
    }

    public void ChangeCharacter(int newCharacter)
    {
        characters[_selectedCharacter].SetActive(false);
        characters[newCharacter].SetActive(true);
        _selectedCharacter = newCharacter;
    }
}