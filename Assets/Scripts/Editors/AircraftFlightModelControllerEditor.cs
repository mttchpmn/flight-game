using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(AircraftFlightModelController))]
    public class AircraftFlightModelControllerEditor : Editor
    {
        private AircraftFlightModelController input;

        private void OnEnable()
        {
            input = (AircraftFlightModelController) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var data = GetData();

            GUILayout.Space(10);

            GUILayout.Label("Flight Model Values");
            EditorGUILayout.TextArea(data, GUILayout.Height(100));

            GUILayout.Space(20);

            Repaint();
        }

        private string GetData()
        {
            var result = new StringBuilder();

            result.AppendLine($"Velocity:\t\t{Math.Round(input.VelocityKnots)} kts");

            return result.ToString();
        }
    }
}