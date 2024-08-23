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
    [SerializeField] private bool customBehaviourHydrogen;
    [SerializeField] private bool customBehaviourHelium;

    [Header("States")]
    [SerializeField] private bool customBehaviourTriggered;
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

        if (customBehaviourHydrogen)
        {
            EndTalking();
            if (customBehaviourTriggered) return;

            question.Reward();

            customBehaviourTriggered = true;
            return;
        }

        if (customBehaviourHelium)
        {
            EndTalking();
            List<ElectronMovement> electrons = new List<ElectronMovement>(FindObjectsOfType<ElectronMovement>());
            electrons.ForEach(e => e.SetMoveAroundObject(transform));

            return;
        }


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
