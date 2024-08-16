using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMonologue : MonoBehaviour
{
    [System.Serializable]
    public struct Message
    {
        [TextArea] public string message;
        public float duration;
    }

    [SerializeField] private Message[] monologue;

    [Header("States")]
    [SerializeField] private bool isTalking;
    [SerializeField] private int actualMessageNumber;
    [SerializeField] private float messageTimer;

    [Header("Other Components")]
    [SerializeField] private ElementQuestion question;

    private void Update()
    {
        if (!isTalking || question.GetQuestioning()) return;

        if(messageTimer <= 0) ReadNextMessage();
        else messageTimer -= Time.deltaTime;
    }

    public void StartMonologue()
    {
         if(monologue == null) return;

        if (monologue.Length <= 0) return;

        isTalking = true;
    }

    private void EndMonologue()
    {
        actualMessageNumber = 0;

        question.StartQuestion();
    }

    private void ReadNextMessage()
    {
        if(actualMessageNumber >= monologue.Length)
        {
            EndMonologue();
            return;
        }

        Message actualMessage = monologue[actualMessageNumber];

        SpatialBridge.coreGUIService.DisplayToastMessage(actualMessage.message, actualMessage.duration);
        messageTimer = actualMessage.duration;

        actualMessageNumber++;
    }

    public void EndTalking()
    {
        isTalking = false;
    }
}
