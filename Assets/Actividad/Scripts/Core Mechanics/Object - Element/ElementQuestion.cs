using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementQuestion : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField, TextArea] private string questionText;
    [SerializeField, TextArea] private string correctAnswerText;
    [SerializeField, TextArea] private string incorrectAnswerText;

    [Header("Components")]
    [SerializeField] private GameObject questionObject;

    [Header("Parameters")]
    [SerializeField] private bool isAnswerA;

    [Header("States")]
    [SerializeField] private bool isAnswered;
    [SerializeField] private bool isQuestioning;

    [Header("Other Components")]
    [SerializeField] private ElementMonologue monologue;

    [Header("Reward")]
    [SerializeField] private GameObject rewardPrefab;

    private void Start()
    {
        SetQuestion();
    }

    private void SetQuestion()
    {
        isAnswered = false;

        if(questionObject != null) questionObject.SetActive(false);
    }

    public void StartQuestion()
    {
        if (isAnswered)
        {
            monologue.EndTalking();
            return;
        }

        if (questionObject == null || isQuestioning) return;

        isQuestioning = true;
        questionObject.SetActive(true);

        SpatialBridge.coreGUIService.DisplayToastMessage(questionText);
    }

    public void Answer(bool isAnswerA)
    {
        if (!isQuestioning) return;

        bool correctAnswer = this.isAnswerA == isAnswerA;

        if (correctAnswer)
        {
            isAnswered = true;
            Reward();
        }

        SpatialBridge.coreGUIService.DisplayToastMessage(correctAnswer ? correctAnswerText : incorrectAnswerText);

        monologue.EndTalking();
        questionObject.SetActive(false);
        isQuestioning = false;
    }

    public void Reward()
    {
        GameObject reward = Instantiate(rewardPrefab);
    }

    public bool GetQuestioning()
    {
        return isQuestioning;
    }
}
