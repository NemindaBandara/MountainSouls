using UnityEngine;
using System.Collections.Generic;

public class TutorialEventTrigger : MonoBehaviour
{
  public enum TutorialEventType
  {
    ShakySoil,
    MudPatch,
    FallingRocks,
    BullEncounter
  }

  [Header("Event Configuration")]
  public TutorialEventType eventType;

  [TextArea(2, 4)]
  public string canineAction;

  [TextArea(2, 4)]
  public string miloPrompt;

  [Header("Sensory Cue")]
  public SensoryManager.SenseType recommendedSense;

  private bool triggered = false;
  private DialogueUIController dialogueUI;
  private SensoryManager sensoryManager;

  // Tracks every character currently inside this trigger boundary
  private HashSet<GameObject> charactersInTrigger = new HashSet<GameObject>();

  void Start()
  {
    dialogueUI = FindFirstObjectByType<DialogueUIController>(FindObjectsInactive.Include);
    sensoryManager = FindFirstObjectByType<SensoryManager>(FindObjectsInactive.Include);
  }

  private bool IsPartyMember(GameObject obj)
  {
    return obj.name == "Milo" ||
           obj.name == "Hiker_Girl_1" ||
           obj.name == "Hiker_Girl_2" ||
           obj.CompareTag("Player");
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (!IsPartyMember(other.gameObject)) return;

    charactersInTrigger.Add(other.gameObject);

    // When Milo (the scout) enters first
    if (!triggered && (other.CompareTag("Player") || other.name == "Milo"))
    {
      triggered = true;

      MiloMovement milo = other.GetComponent<MiloMovement>();
      if (milo != null) milo.SetState(MiloMovement.MovementState.Wait);

      if (dialogueUI != null)
      {
        dialogueUI.ShowDialogue("Milo", $"{canineAction}\n{miloPrompt}");
      }

      Debug.Log($"<color=orange>[Hazard Hit]</color> {eventType}. Recommended Sense: {recommendedSense}");
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (!IsPartyMember(other.gameObject)) return;

    charactersInTrigger.Remove(other.gameObject);

    // Only dismiss when Milo and both trailing hikers have fully exited
    if (charactersInTrigger.Count == 0 && triggered)
    {
      if (dialogueUI != null)
      {
        dialogueUI.HideDialogue();
      }

      if (sensoryManager != null)
      {
        sensoryManager.ClearSense();
      }

      Debug.Log($"<color=green>[Party Cleared]</color> Everyone is past the {eventType}!");
    }
  }
}