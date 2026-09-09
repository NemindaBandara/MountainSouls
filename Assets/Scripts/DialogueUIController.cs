using UnityEngine;
using TMPro;

public class DialogueUIController : MonoBehaviour
{
  [Header("UI Elements")]
  public GameObject panelRoot;
  public TextMeshProUGUI speakerText;
  public TextMeshProUGUI contentText;
  public Transform choicesContainer;
  public GameObject choiceButtonTemplate;

  void Awake()
  {
    // Speaker: Milo
    // Action/Internal POV: Physical canine cues + clear directive to the hikers
    HideDialogue();
    //ShowDialogue("Milo", "*Turns back toward the hikers and gives a sharp, urgent bark.* [Obstacle ahead: Loose shale on the incline.]");

  }

  public void ShowDialogue(string speaker, string content)
  {
    panelRoot.SetActive(true);
    speakerText.text = speaker;
    contentText.text = content;
  }

  public void HideDialogue()
  {
    panelRoot.SetActive(false);
  }
}