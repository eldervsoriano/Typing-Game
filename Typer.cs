using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public WordBank wordbank = null;
    public TMP_Text wordOutput = null;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    private bool gameFinished = false;
    private float exitTimer = 3f; // Time before auto exit in seconds
    private float currentExitTime = 0f;
    private bool autoExitStarted = false;

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordbank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void Update()
    {
        if (!gameFinished)
        {
            CheckInput();
            if (isWordComplete())
            {
                gameFinished = true;
                autoExitStarted = true;
            }
        }

        if (autoExitStarted)
        {
            currentExitTime += Time.deltaTime;
            if (currentExitTime >= exitTimer)
            {
                Debug.Log("Auto exiting game...");
                Application.Quit();
            }
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;

            if (keyPressed.Length == 1)
                EnterLetter(keyPressed);
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (isCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (isWordComplete())
                SetCurrentWord();
        }
    }

    private bool isCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool isWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
