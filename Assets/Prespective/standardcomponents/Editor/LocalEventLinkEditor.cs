using System.Reflection;
using UnityEditor;
using u040.prespective.core.editor;

namespace u040.prespective.standardcomponents.editor
{
    [ObfuscationAttribute(Exclude = true, ApplyToMembers = true, StripAfterObfuscation = false)]
    [CustomEditor(typeof(LocalEventLink))]
    public class LocalEventLinkEditor : PrespectiveEditor
    {
        private LocalEventLink component;
        private SerializedObject soTarget;

        private void OnEnable()
        {
            component = (LocalEventLink)target;
            soTarget = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            if (licenseState == false)
            {
                EditorGUILayout.LabelField("No Prespective License Found", EditorStyles.boldLabel);
                return;
            }

            soTarget.Update();
            //DrawDefaultInspector();

            EditorGUILayout.ObjectField("Listener", component.Listener, typeof(LocalEventLink), false);

            base.OnInspectorGUI();
        }
    }
}
