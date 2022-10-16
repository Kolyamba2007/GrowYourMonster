using UnityEditor;
using System.Collections.Generic;

namespace Contexts.MainContext
{
    [CustomEditor(typeof(InfrastructureView), true)]
    public class InfrastructureViewGUI : Editor
    {
        int _selected;
        private List<string> _selection;

        private SerializedProperty _id;

        private void OnEnable()
        {
            var gameConfig = GameConfig.Load();

            _id = serializedObject.FindProperty("type");

            _selection = new List<string>(gameConfig.GetInfrastructureConfig.InfrastructureData.Keys);

            _selected = _selection.FindIndex(x => x == _id.stringValue);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            _selected = EditorGUILayout.Popup("Infrastructure Type", _selected, _selection.ToArray());

            _id.stringValue = _selection[_selected];

            serializedObject.ApplyModifiedProperties();
        }
    }
}
