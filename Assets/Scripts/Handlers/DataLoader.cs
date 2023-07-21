using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private TMP_Text timeDisplay;
    [SerializeField] private TMP_Text scoreDisplay;

    private void Start()
    {
        timeDisplay.text = PlayerPrefs.GetString("Time");
        scoreDisplay.text = PlayerPrefs.GetInt("Score").ToString();
    }
}
