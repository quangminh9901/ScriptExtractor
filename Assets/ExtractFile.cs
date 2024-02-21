using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ExtractFile : MonoBehaviour
{
    public string finalPath;
    public string fileName;
    public string content;
    public TextMeshProUGUI scriptName;
    void onLoad()
    {
        content = "";
    }

    // Function to load a single file
    public void LoadFile(string filePath)
    {
        StartCoroutine(LoadScript(filePath));
    }

    // Coroutine to load script from file
    IEnumerator LoadScript(string path)
    {
        if (path != null)
        {
            string scriptText = File.ReadAllText(path);
            // Debug.Log("Script Content: " + scriptText);
            content = content + "\n" + fileName + "\n" + scriptText;
        }
        yield return null;
    }

    // Function to select a directory and load files from its subdirectories
    public void SelectAndLoadFiles()
    {
#if UNITY_EDITOR
        string selectedPath = EditorUtility.OpenFolderPanel("Select Folder", "", "");
        if (!string.IsNullOrEmpty(selectedPath))
        {
            LoadFilesFromDirectory(selectedPath);
        }
#else
        Debug.LogError("Folder selection is not supported outside the Unity Editor.");
#endif
    }

    // Function to load files from a directory and its subdirectories
    void LoadFilesFromDirectory(string directoryPath)
    {
        // Get all files in the directory
        string[] files = Directory.GetFiles(directoryPath, "*.cs");

        // Load each file
        foreach (string filePath in files)
        {
            LoadFile(filePath);
            fileName = Path.GetFileName(filePath);
        }

        // Get all subdirectories
        string[] subdirectories = Directory.GetDirectories(directoryPath);

        // Recursively load files from subdirectories
        foreach (string subdirectory in subdirectories)
        {
            LoadFilesFromDirectory(subdirectory);
        }
    }
    public void logContent()
    {
        // Debug.Log("Content: " + content);
        // in ra file world
        if (scriptName == null)
        {
            Debug.Log("Script name is null");
            return;
        }
        string path = "E:/Cocos/TXT/" + scriptName.text + ".txt";
        File.WriteAllText(path, content);
        Debug.Log("Write file World.txt success");
        Debug.Log("File: " + scriptName.text);
    }

    public void clearContent()
    {
        content = "";
        Debug.Log("Clear content");
    }
}
