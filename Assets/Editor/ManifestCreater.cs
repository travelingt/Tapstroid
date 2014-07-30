using UnityEngine;
using System.Collections;
using UnityEditor;


public class ManifestCreater : EditorWindow
{
    [MenuItem("Window/ManifestCreater")]
	// Use this for initialization
	
	public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ManifestCreater));
    }
	// Update is called once per frame
	
    void OnGUI()
    {

    }
}
