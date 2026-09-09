using UnityEngine;
using UnityEngine.UI;

public class SensoryManager : MonoBehaviour
{
  public enum SenseType { None, Smell, Sound, Vision }

  [Header("Current Active Sense")]
  public SenseType activeSense = SenseType.None;

  [Header("UI Feedback")]
  public Image sensoryVignette;
  public Color smellColor = new Color(0.2f, 0.8f, 0.2f, 0.25f);   // Translucent green
  public Color soundColor = new Color(0.2f, 0.5f, 1.0f, 0.25f);   // Translucent blue
  public Color visionColor = new Color(1.0f, 0.9f, 0.3f, 0.25f);  // Translucent amber

  void Start()
  {
    ClearSense();
  }

  public void ToggleSmell()
  {
    if (activeSense == SenseType.Smell) ClearSense();
    else ActivateSense(SenseType.Smell, smellColor);
  }

  public void ToggleSound()
  {
    if (activeSense == SenseType.Sound) ClearSense();
    else ActivateSense(SenseType.Sound, soundColor);
  }

  public void ToggleVision()
  {
    if (activeSense == SenseType.Vision) ClearSense();
    else ActivateSense(SenseType.Vision, visionColor);
  }

  private void ActivateSense(SenseType sense, Color tint)
  {
    activeSense = sense;
    if (sensoryVignette != null)
    {
      sensoryVignette.color = tint;
    }
    Debug.Log($"Milo activated sense: {sense}");
  }

  public void ClearSense()
  {
    activeSense = SenseType.None;
    if (sensoryVignette != null)
    {
      sensoryVignette.color = new Color(0, 0, 0, 0);
    }
  }
}