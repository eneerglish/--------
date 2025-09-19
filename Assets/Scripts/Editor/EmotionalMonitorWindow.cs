using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EmotionMonitorWindow : EditorWindow
{
    private Worker selectedWorker;

    // Unityのメニューバーにウィンドウを追加
    [MenuItem("Tools/Worker/Emotion Monitor")]
    public static void ShowWindow()
    {
        GetWindow<EmotionMonitorWindow>("Emotion Monitor");
    }

    // 選択オブジェクトが変更されたときに呼び出される
    private void OnSelectionChange()
    {
        if (Selection.activeGameObject != null)
        {
            selectedWorker = Selection.activeGameObject.GetComponent<Worker>();
        }
        else
        {
            selectedWorker = null;
        }
        Repaint(); // ウィンドウを再描画
    }

    // ウィンドウのGUIを描画
    private void OnGUI()
    {
        GUILayout.Label("Worker Emotion Monitor", EditorStyles.boldLabel);
        
        // 選択されたWorkerオブジェクトをフィールドに表示
        selectedWorker = (Worker)EditorGUILayout.ObjectField("Selected Worker", selectedWorker, typeof(Worker), true);
        
        if (selectedWorker == null)
        {
            EditorGUILayout.HelpBox("シーンからWorkerオブジェクトを選択してください。", MessageType.Info);
            return;
        }

        // GUIを無効化して、ゲーム実行中のみ値を表示（編集不可にする）
        GUI.enabled = Application.isPlaying;
        
        // ここから感情の表示
        GUILayout.Space(10);
        GUILayout.Label("Current Emotions", EditorStyles.boldLabel);

        // WorkerオブジェクトからEmotionsクラスのリストを取得
        var emotionList = GetEmotionList(selectedWorker);

        if (emotionList != null)
        {
            foreach (var emotion in emotionList)
            {
                // 感情の名前と値を表示
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(emotion.emotion.ToString(), GUILayout.Width(80));
                EditorGUILayout.Slider(emotion.value, 0f, 100f);
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("WorkerオブジェクトにEmotionsクラスのインスタンスが見つかりません。", MessageType.Warning);
        }
        
        GUI.enabled = true;
    }
    
    // リフレクションを使って非公開のemotionListフィールドを取得
    private List<WorkerEmotion> GetEmotionList(Worker worker)
    {
        if (worker == null)
            return null;
        
        // Emotionsクラスのインスタンスを取得
        var emotionsField = typeof(Worker).GetField("emotions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var emotionsInstance = emotionsField?.GetValue(worker);
        
        if (emotionsInstance == null)
            return null;
        
        // emotionListフィールドを取得
        var emotionListField = emotionsInstance.GetType().GetField("emotionList", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        
        return (List<WorkerEmotion>)emotionListField?.GetValue(emotionsInstance);
    }
}