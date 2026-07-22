using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Character.Controller
{
    [CustomEditor(typeof(WeaponAim))]
    public class WeaponAimEditor : Editor
    {
        private Type[] _aimSourceTypes;
        private string[] _aimSourceTypeNames;
        private const string TypeNamePrefix = "AimSource";

        private void OnEnable()
        {
            _aimSourceTypes = TypeCache.GetTypesDerivedFrom<IAimSource>()
                .Where(t => typeof(Component).IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

            _aimSourceTypeNames = _aimSourceTypes
                .Select(t => t.Name.StartsWith(TypeNamePrefix) ? t.Name[TypeNamePrefix.Length..] : t.Name)
                .ToArray();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var weaponAim = (WeaponAim)target;
            GameObject go = weaponAim.gameObject;

            IAimSource current = go.GetComponent<IAimSource>();
            int currentIndex = Array.IndexOf(_aimSourceTypes, current?.GetType());

            EditorGUI.BeginChangeCheck();
            
            int newIndex = EditorGUILayout.Popup("Aim Source", currentIndex, _aimSourceTypeNames);
            if (EditorGUI.EndChangeCheck() && newIndex != currentIndex)
                SwapAimSource(go, current, _aimSourceTypes[newIndex]);

            EditorGUILayout.Space();
        }
        private void SwapAimSource(GameObject go, IAimSource current, Type newType)
        {
            Undo.SetCurrentGroupName("Change Aim Source");
            int group = Undo.GetCurrentGroup();

            if (current is Component currentComponent)
                Undo.DestroyObjectImmediate(currentComponent);

            Undo.AddComponent(go, newType);
            Undo.CollapseUndoOperations(group);
        }
    }
}