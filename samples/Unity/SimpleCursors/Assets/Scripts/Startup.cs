using UnityEngine;
using System.Collections;
using Pluralinput.Sdk;

public class Startup : MonoBehaviour {

    public static InputManager InputManager;
    public GameObject cursorPrefab;

	// Use this for initialization
	void Start () {
#if !UNITY_EDITOR
        InputManager = new InputManager();

        foreach (var mouse in InputManager.DeviceEnumerator.EnumerateMice())
        {
            print(mouse.DeviceName);
            var cursor = Instantiate(cursorPrefab);
            cursor.GetComponent<CursorController>().mouse = mouse;
        }
#endif
    }

    // Update is called once per frame
    void Update () {
	
	}
}
