using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class XAPIWrapperTools : EditorWindow 
{
    // Icon for our window
    private static Texture2D icon;

    // Various foldout settings
    private bool showNew = false;
    private bool showEdit = false;
    private Vector2 scrollView = Vector2.zero;

    [MenuItem ("Window/XAPI/Wrapper Tools")]
    public static void OpenCreateObjectWindow ()
    {
        // Make the window
        var window = EditorWindow.GetWindow (typeof(XAPIWrapperTools));

        // Get our icon
        XAPIWrapperTools.icon = Resources.Load("lrs-editor-icon") as Texture2D;

        // Clean it up a little
        window.titleContent = new GUIContent("Wrapper Tools", XAPIWrapperTools.icon);
    }

    void OnGUI ()
    {
        // Start our scrolling from the start
        this.scrollView = EditorGUILayout.BeginScrollView(this.scrollView);

        // Content will start here, it looks a little weird if text is immediate to space it out
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("About the Tools:");

        // Inform the user as to what this window even does.
        EditorGUILayout.HelpBox(
            string.Format(
                "{0}\n\n{1}",
                "These tools offer users an automated way to configure communication with their LRS.  ",
                "For help with the terminology, a window with definitions is available through: \n\n  Window > XAPI > XAPI Wrapper Help"
            ), MessageType.Info
        );
        
        if (this.showNew = EditorGUILayout.BeginToggleGroup("New LRS Configuration", this.showNew))
        {
            EditorGUI.indentLevel = 2;

            EditorGUILayout.HelpBox(
                string.Format(
                    "{0}\n\n{1}",
                    "If you aren't familiar with ScriptableObjects within Unity, then you can think " +
                    "of them as a way of storing things that are similar in structure while containing different values. ",
                    "For example in Hearthstone, each card has a ScriptableObject representing its data."
                ), MessageType.Info
            );
            
            
            EditorGUI.indentLevel = 0;
        }
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndScrollView();
    }
}