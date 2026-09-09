using UnityEngine;

public class CommandUIController : MonoBehaviour
{
    private MiloMovement milo;

    void Start()
    {
        GameObject miloObj = GameObject.Find("Milo");
        if (miloObj != null)
        {
            milo = miloObj.GetComponent<MiloMovement>();
        }
    }

    public void OnCommandWait()
    {
        if (milo != null) milo.SetState(MiloMovement.MovementState.Wait);
    }

    public void OnCommandStartMovement()
    {
        // "Start" resumes normal pace
        if (milo != null) milo.SetState(MiloMovement.MovementState.Normal);
    }

    public void OnCommandSlow()
    {
        if (milo != null) milo.SetState(MiloMovement.MovementState.Slow);
    }

    public void OnCommandFast()
    {
        if (milo != null) milo.SetState(MiloMovement.MovementState.Fast);
    }

    public void OnCommandPathSelection()
    {
        // Path selection placeholder trigger
        Debug.Log("Path Selection triggered. Evaluating trail options...");
    }
}