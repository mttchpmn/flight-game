using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AircraftInputHandler))]
public class AircraftInputHandlerEditor : Editor
{
    private AircraftInputHandler input;

    private void OnEnable()
    {
        input = (AircraftInputHandler) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var data = GetData();

        GUILayout.Space(10);

        GUILayout.Label("Input Values");
        EditorGUILayout.TextArea(data, GUILayout.Height(100));

        GUILayout.Space(20);

        Repaint();
    }

    private string GetData()
    {
        var result = new StringBuilder();

        result.AppendLine($"Pitch:\t\t{input.Pitch}");
        result.AppendLine($"Roll:\t\t{input.Roll}");
        result.AppendLine($"Yaw:\t\t{input.Yaw}");
        result.AppendLine($"Throttle:\t\t{input.ThrottleSetting}");
        result.AppendLine($"Flaps:\t\t{input.Flaps}");
        result.AppendLine($"Brake:\t\t{input.Brake}");

        return result.ToString();
    }
}