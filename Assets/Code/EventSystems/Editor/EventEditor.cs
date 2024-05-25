using EventSystems.SO;
using UnityEditor;
using UnityEngine;

namespace EventSystems.Editor
{
    [CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
    public class EventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GUI.backgroundColor = Color.green;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}