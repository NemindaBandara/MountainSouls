using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class StoryManager : MonoBehaviour
{
  [Header("Ink Asset")]
  [SerializeField] private TextAsset inkJSONAsset;

  [Header("UI References")]
  [SerializeField] private DialogueUIController dialogueUI;
  [SerializeField] private Transform choicesContainer;
  [SerializeField] private GameObject choiceButtonPrefab;

  private Story story;

  void Start()
  {
    if (inkJSONAsset != null)
    {
      StartStory(inkJSONAsset);
    }
  }

  public void StartStory(TextAsset inkJSON)
  {
    story = new Story(inkJSON.text);
    ContinueStory();
  }

  public void ContinueStory()
  {
    ClearChoices();

    if (story.canContinue)
    {
      string rawLine = story.Continue().Trim();

      // Format: "Speaker: Line content"
      string speaker = "Milo";
      string content = rawLine;

      if (rawLine.Contains(":"))
      {
        string[] parts = rawLine.Split(new char[] { ':' }, 2);
        speaker = parts[0].Trim();
        content = parts[1].Trim();
      }

      dialogueUI.ShowDialogue(speaker, content);

      // If there are branching choices available, render them
      if (story.currentChoices.Count > 0)
      {
        DisplayChoices();
      }
    }
    else if (story.currentChoices.Count > 0)
    {
      DisplayChoices();
    }
    else
    {
      // End of knot / sequence
      dialogueUI.HideDialogue();
    }
  }

  private void DisplayChoices()
  {
    ClearChoices();

    foreach (Choice choice in story.currentChoices)
    {
      GameObject btnObj = Instantiate(choiceButtonPrefab, choicesContainer);
      btnObj.SetActive(true);

      TextMeshProUGUI btnText = btnObj.GetComponentInChildren<TextMeshProUGUI>();
      if (btnText != null)
      {
        btnText.text = choice.text.Trim();
      }

      int choiceIndex = choice.index;
      Button btn = btnObj.GetComponent<Button>();
      btn.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
    }
  }

  private void OnChoiceSelected(int index)
  {
    story.ChooseChoiceIndex(index);
    ContinueStory();
  }

  private void ClearChoices()
  {
    foreach (Transform child in choicesContainer)
    {
      // Do not destroy the inactive template prefab itself if it lives here
      if (child.gameObject == choiceButtonPrefab) continue;
      Destroy(child.gameObject);
    }
  }
}