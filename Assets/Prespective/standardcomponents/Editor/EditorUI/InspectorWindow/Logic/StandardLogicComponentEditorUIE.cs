using u040.prespective.prelogic.editor.editorui.inspectorwindow.component;
using u040.prespective.standardcomponents.logic;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.logic
{
    [CustomEditor(typeof(StandardLogicComponent), true, isFallback = true)]
    internal class StandardLogicComponentEditorUIE : PreLogicComponentEditorUIE<StandardLogicComponent>
    {
        protected override VisualElement createInheritingTypeEditorUI()
        {
            return UIUtility.InitializeField
            (
                _field: new ObjectField(ObjectNames.NicifyVariableName(component.TargetType.Name)),
                _targetObject: component,
                _getValue: () => component.TargetComponent,
                _onValueChanged: _e => component.TargetComponent = _e.newValue as Component,
                _objectType: component.TargetType
            );
        }
    }
}
