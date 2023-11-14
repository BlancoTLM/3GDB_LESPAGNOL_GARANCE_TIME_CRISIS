using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CustomEditor(typeof(OnKeyEvents))]
public class OnKeyEventsEditor : Editor
{
    OnKeyEvents t;
    SerializedObject serializedTarget;
    SerializedProperty EventList;

    List<bool> foldedState = new List<bool>();

    void OnEnable()
    {
        t = (OnKeyEvents)target;
        serializedTarget = new SerializedObject(target);
        EventList = serializedTarget.FindProperty("keyEvents"); // Find the List in our script and create a refrence of it
        for (int i = 0; i < EventList.arraySize; i++)
        {
            foldedState.Add(true);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedTarget.Update();

        for (int i = 0; i < EventList.arraySize; i++)
        {
            SerializedProperty item = EventList.GetArrayElementAtIndex(i);
            var key = item.FindPropertyRelative("key");
            var eventPressed = item.FindPropertyRelative("EventKeyPressed");
            var eventReleased = item.FindPropertyRelative("EventKeyReleased");

            GUILayout.BeginHorizontal();
            string foldButtonText = foldedState[i] ? ">" : "<";
            if (GUILayout.Button(foldButtonText, GUILayout.MaxWidth(20))) foldedState[i] = !foldedState[i];

            Color bgColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
            {
                t.keyEvents.RemoveAt(i);
                foldedState.RemoveAt(i);
                break;
            }
            GUI.backgroundColor = bgColor;

            if (foldedState[i])
            {
                GUILayout.Label(key.enumDisplayNames[key.enumValueIndex].ToString());
            }else
            {
                GUILayout.BeginVertical();
                EditorGUILayout.PropertyField(key);
                EditorGUILayout.PropertyField(eventPressed);
                EditorGUILayout.PropertyField(eventReleased);
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            

            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        // display add button
        if(GUILayout.Button("+"))
        {
            t.keyEvents.Add(new KeyEvent());
            foldedState.Add(true);
        }



        //Apply the changes to our list
        serializedTarget.ApplyModifiedProperties();
    }
}
