using System.Reflection;
using u040.prespective.prelogic.component.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents
{
    [CustomEditor(typeof(StandardLogicComponent), true, isFallback = true)]
    internal class StandardLogicComponentEditorUIE : PreLogicComponentEditorUIE<StandardLogicComponent>
    {
        protected override VisualElement createCustomEditorUI()
        {
            return UIUtility.InitializeField
            (
                _field: new ObjectField(ObjectNames.NicifyVariableName(component.TargetType.Name)),
                _getValue: () => component.TargetComponent,
                _onValueChanged: _e => component.TargetComponent = _e.newValue as Component,
                _objectType: component.TargetType
            );
        }
    }
}
