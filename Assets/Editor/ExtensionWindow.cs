using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtensionsWindow: EditorWindow {
    //Go to Tools and you will find this
    //Learn more at https://learn.unity.com/tutorial/editor-scripting
    [MenuItem("Tools/Reload Tools")]
    public static void ShowWindow() {
        var window = GetWindow<ExtensionsWindow>();
        window.titleContent = new GUIContent("Reload");
        window.Show();
    }

    private bool isDomainReloadDisabled => EditorApplication.isCompiling;
    private bool isSceneReloadDisabled => !EditorApplication.isPlaying;

    private void OnGUI() {
        Disableable(DomainReloadButton, isDomainReloadDisabled);
        Disableable(SceneReloadButton, isSceneReloadDisabled);
    }

    private void DomainReloadButton() {
        if (GUILayout.Button("Reload Domain")) {
            EditorUtility.RequestScriptReload();
        }
    }

    [MenuItem("Tools/Reload/Domain _F1")]
    public static void ReloadDomain() {
        EditorUtility.RequestScriptReload();
    }
    private void SceneReloadButton() {
        if (GUILayout.Button("Reload Scene")) {
            var scene = SceneManager.GetActiveScene();
            if (scene != null) {
                var opts = new LoadSceneParameters { };
                EditorSceneManager.LoadSceneInPlayMode(scene.path, opts);
            }
        }
    }
    [MenuItem("Tools/Reload/Scene _F2")]
    public static void ReloadScene() {
        var scene = SceneManager.GetActiveScene();
        if (scene != null) {
            var opts = new LoadSceneParameters { };
            EditorSceneManager.LoadSceneInPlayMode(scene.path, opts);
        }
    }
    private void Disableable(Action renderer, bool disabled) {
        EditorGUI.BeginDisabledGroup(disabled);
        renderer();
        EditorGUI.EndDisabledGroup();
    }
}