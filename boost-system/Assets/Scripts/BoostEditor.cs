using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BoostEditor : EditorWindow
{
    public BoostList boostListEditor;
    private int viewIndex = 1;

    [MenuItem("Window/Boost Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(BoostEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            boostListEditor = AssetDatabase.LoadAssetAtPath(objectPath, typeof(BoostList)) as BoostList;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Boost Editor", EditorStyles.boldLabel);
        if (boostListEditor != null)
        {
            if (GUILayout.Button("Show Boost List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = boostListEditor;
            }
        }
        if (GUILayout.Button("Open Boost List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Boost List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = boostListEditor;
        }
        GUILayout.EndHorizontal();

        if (boostListEditor == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Boost List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Boost List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (boostListEditor != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < boostListEditor.boostList.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Boost", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Boost", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (boostListEditor.boostList == null)
                Debug.Log("Boost Editor is empty");
            if (boostListEditor.boostList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Boost", viewIndex, GUILayout.ExpandWidth(false)), 1, boostListEditor.boostList.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField("of   " + boostListEditor.boostList.Count.ToString() + "  boosts", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();
                boostListEditor.boostList[viewIndex - 1].boostName = EditorGUILayout.TextField("Boost Name", boostListEditor.boostList[viewIndex - 1].boostName as string);
                GUILayout.Space(10);
            }
            else
            {
                GUILayout.Label("This boost list is empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(boostListEditor);
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        boostListEditor = CreateBoostList.Create();
        if (boostListEditor)
        {
            boostListEditor.boostList = new List<Boost>();
            string relPath = AssetDatabase.GetAssetPath(boostListEditor);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select boost item list", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            boostListEditor = AssetDatabase.LoadAssetAtPath(relPath, typeof(BoostList)) as BoostList;
            if (boostListEditor.boostList == null)
                boostListEditor.boostList = new List<Boost>();
            if (boostListEditor)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        Boost newItem = new Boost();
        newItem.boostName = "New Boost";
        boostListEditor.boostList.Add(newItem);
        viewIndex = boostListEditor.boostList.Count;
    }

    void DeleteItem(int index)
    {
        boostListEditor.boostList.RemoveAt(index);
    }
}