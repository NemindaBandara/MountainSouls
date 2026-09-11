using UnityEngine;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

public class QuitGameOnKeypress : MonoBehaviour {

	public KeyCode key = KeyCode.Escape;

	void Update () {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
		if(WasKeyPressedThisFrame(key)) Application.Quit();
#else
		if(Input.GetKeyDown(key)) Application.Quit();
#endif
	}

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
	// When the project uses only the new Input System, the legacy Input class throws.
	// Map the KeyCode to the new Input System's Key by name (works for common keys such as Escape).
	static bool WasKeyPressedThisFrame (KeyCode keyCode) {
		var keyboard = Keyboard.current;
		if(keyboard == null) return false;
		if(System.Enum.TryParse<Key>(keyCode.ToString(), out var parsedKey)) {
			var control = keyboard[parsedKey];
			return control != null && control.wasPressedThisFrame;
		}
		return false;
	}
#endif
}
