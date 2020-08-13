using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class XAPIWrapperHelp : EditorWindow 
{
    // Icon for our window
    private static Texture2D icon;

    // Various foldout settings
    private bool showInfo = false;
    private bool showLRS = false;
    private bool showXAPI = false;
    private bool showNew = false;
    private bool showEdit = false;
    private Vector2 scrollView = Vector2.zero;

    [MenuItem ("Window/XAPI/Wrapper Help")]
    public static void OpenCreateObjectWindow ()
    {
        // Make the window
        var window = EditorWindow.GetWindow (typeof(XAPIWrapperHelp));

        // Get our icon
        XAPIWrapperHelp.icon = Resources.Load("lrs-editor-icon") as Texture2D;

        // Clean it up a little
        window.titleContent = new GUIContent("Wrapper Help", XAPIWrapperHelp.icon);

    }

    void OnGUI ()
    {
        // Start our scrolling from the start
        this.scrollView = EditorGUILayout.BeginScrollView(this.scrollView);

        // Content will start here, it looks a little weird if text is immediate to space it out
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("About the Wrapper:");

        // Inform the user as to what this window even does.
        EditorGUILayout.HelpBox(
            string.Format(
                "{0}\n\n{1}",
                "This wrapper provides functions to help Unity developers communicate with a Learning Record Store (LRS).",
                "This window provides a brief explanation on how to get started using it and how it works."
            ), MessageType.Info
        );


        if (this.showXAPI = EditorGUILayout.BeginToggleGroup("What is xAPI?", this.showXAPI))
        {
            EditorGUILayout.LabelField("From the spec:");
            EditorGUILayout.HelpBox(
                "The Experience API (xAPI) is a technical specification that aims to facilitate the documentation " +
                "and communication of learning experiences. " +
                "\n\nIt specifies a structure to describe learning experiences " +
                "and defines how these descriptions can be exchanged electronically."
                , MessageType.Info
            );
        }
        EditorGUILayout.EndToggleGroup();

        if (this.showLRS = EditorGUILayout.BeginToggleGroup("What is an LRS?", this.showLRS))
        {
            EditorGUILayout.HelpBox(
                "While the xAPI specification describes how an xAPI statement must be constructed, it also describes " +
                "how those statements must be received and stored by a Learning Record Store (LRS)." +
                "\n\nYou can think of an LRS as a web service sitting on a database, with rules governing " +
                "what information it is allowed to store and how it allows users to access that information." +
                "\n\nThe default LRS in use by this wrapper is hosted by the ADL Initiative."
                , MessageType.Info
            );
        }
        EditorGUILayout.EndToggleGroup();

        if (this.showInfo = EditorGUILayout.BeginToggleGroup("What are ScriptableObjects?", this.showInfo))
        {
            EditorGUILayout.HelpBox(
                string.Format(
                    "{0}\n\n{1}",
                    "If you aren't familiar with ScriptableObjects within Unity, then you can think " +
                    "of them as a way of storing things that are similar in structure while containing different values. ",
                    "For example in Hearthstone, each card has a ScriptableObject representing its data."
                ), MessageType.Info
            );
        }
        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.EndScrollView();
    }
}