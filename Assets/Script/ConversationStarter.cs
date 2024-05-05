using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

// uso l'asset DialogueEditor, si gestisce tutto da unity
public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation _firstconversation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
                ConversationManager.Instance.StartConversation(_firstconversation);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
                ConversationManager.Instance.EndConversation();
        }

    }
}
