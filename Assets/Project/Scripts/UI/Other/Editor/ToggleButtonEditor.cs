using UnityEditor;

namespace UI.Other.Editor
{
    [CustomEditor(typeof(ToggleButton))]
    public class ToggleButtonEditor : UnityEditor.UI.ButtonEditor
    {
        private SerializedProperty _image;
        private SerializedProperty _activeSprite;
        private SerializedProperty _disableSprite;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_image);
            EditorGUILayout.PropertyField(_activeSprite);
            EditorGUILayout.PropertyField(_disableSprite);

            serializedObject.ApplyModifiedProperties();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _image = serializedObject.FindProperty("_image");
            _activeSprite = serializedObject.FindProperty("_activeSprite");
            _disableSprite = serializedObject.FindProperty("_disableSprite");
        }
    }
}