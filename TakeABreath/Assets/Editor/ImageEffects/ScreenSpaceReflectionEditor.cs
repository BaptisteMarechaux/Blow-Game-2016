using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEditor;
using UnityEngine;

<<<<<<< HEAD
namespace UnityStandardAssets.CinematicEffects
{
    [CustomPropertyDrawer(typeof(ScreenSpaceReflection.SSRSettings.LayoutAttribute))]
    public class LayoutDrawer : PropertyDrawer
    {
        private const float kHeadingSpace = 22.0f;

=======
using LayoutAttribute = UnityStandardAssets.ImageEffects.ScreenSpaceReflection.SSRSettings.LayoutAttribute;

namespace UnityStandardAssets.ImageEffects
{
    [CustomEditor(typeof (ScreenSpaceReflection))]
    internal class ScreenSpaceReflectionEditor : Editor
    {
>>>>>>> MultiPart
        static Styles m_Styles;

        private class Styles
        {
            public readonly GUIStyle header = "ShurikenModuleTitle";

            internal Styles()
            {
                header.font = (new GUIStyle("Label")).font;
                header.border = new RectOffset(15, 7, 4, 4);
<<<<<<< HEAD
                header.fixedHeight = kHeadingSpace;
=======
                header.fixedHeight = 22;
>>>>>>> MultiPart
                header.contentOffset = new Vector2(20f, -2f);
            }
        }

<<<<<<< HEAD
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return kHeadingSpace;

            var count = property.CountInProperty();
            return EditorGUIUtility.singleLineHeight * count  + 15;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_Styles == null)
                m_Styles = new Styles();

            position.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = Header(position, property.displayName, property.isExpanded);
            position.y += kHeadingSpace;

            if (!property.isExpanded)
                return;

            foreach (SerializedProperty child in property)
            {
                EditorGUI.PropertyField(position, child);
                position.y += EditorGUIUtility.singleLineHeight;
            }
        }

        private bool Header(Rect position, String title, bool display)
        {
            Rect rect = position;
            position.height = EditorGUIUtility.singleLineHeight;
            GUI.Box(rect, title, m_Styles.header);

            Rect toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (Event.current.type == EventType.Repaint)
                EditorStyles.foldout.Draw(toggleRect, false, false, display, false);

            Event e = Event.current;
            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }
            return display;
        }
    }

    [CustomEditor(typeof(ScreenSpaceReflection))]
    internal class ScreenSpaceReflectionEditor : Editor
    {
=======
        readonly Dictionary<FieldInfo, KeyValuePair<LayoutAttribute, SerializedProperty>> m_PropertyMap = new Dictionary<FieldInfo, KeyValuePair<LayoutAttribute, SerializedProperty>>();

        void PopulateMap(FieldInfo prefix, FieldInfo field)
        {
            var searchPath = prefix.Name + "." + field.Name;

            var attr = field.GetCustomAttributes(typeof (LayoutAttribute), false).FirstOrDefault() as LayoutAttribute;
            if (attr == null)
                attr = new LayoutAttribute(LayoutAttribute.Category.Undefined, 0);

            m_PropertyMap.Add(field, new KeyValuePair<LayoutAttribute, SerializedProperty>(attr, serializedObject.FindProperty(searchPath))); 
        }

        private static class StaticFieldFinder<T>
        {
            public static FieldInfo GetField<TValue>(Expression<Func<T, TValue>> selector)
            {
                Expression body = selector;
                if (body is LambdaExpression)
                {
                    body = ((LambdaExpression) body).Body;
                }
                switch (body.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        return (FieldInfo) ((MemberExpression) body).Member;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        [Serializable]
        private class CatFoldoutMap
        {
            public LayoutAttribute.Category category;
            public bool display; 

            public CatFoldoutMap(LayoutAttribute.Category category, bool display)
            {
                this.category = category;
                this.display = display;
            }
        }

        [SerializeField]
        private List<CatFoldoutMap> m_CategoriesToShow = new List<CatFoldoutMap>();

        [NonSerialized]
        private bool m_Initialized;

>>>>>>> MultiPart
        private enum SettingsMode
        {
            HighQuality,
            Default,
            Performance,
            Custom,
        }

<<<<<<< HEAD
        [NonSerialized]
        private List<SerializedProperty> m_Properties = new List<SerializedProperty>();

        void OnEnable()
        {
            var settings = FieldFinder<ScreenSpaceReflection>.GetField(x => x.settings);
            foreach (var setting in settings.FieldType.GetFields())
            {
                var prop = settings.Name + "." + setting.Name;
                m_Properties.Add(serializedObject.FindProperty(prop));
            }
=======
        private void Initialize()
        {
            m_Styles = new Styles();
            var categories = Enum.GetValues(typeof (LayoutAttribute.Category)).Cast<LayoutAttribute.Category>();
            foreach (var cat in categories)
            {
                if (m_CategoriesToShow.Any(x => x.category == cat))
                    continue;

                m_CategoriesToShow.Add(new CatFoldoutMap(cat, true));
            }

            var prefix =  StaticFieldFinder<ScreenSpaceReflection>.GetField(x => x.settings);
            foreach (var field in typeof (ScreenSpaceReflection.SSRSettings).GetFields(BindingFlags.Public | BindingFlags.Instance))
                PopulateMap(prefix, field); 

            m_Initialized = true;
>>>>>>> MultiPart
        }

        public override void OnInspectorGUI()
        {
<<<<<<< HEAD
            serializedObject.Update();

            EditorGUILayout.Space();
=======
            if (!m_Initialized)
                Initialize();
>>>>>>> MultiPart

            var currentState = ((ScreenSpaceReflection)target).settings;

            var settingsMode = SettingsMode.Custom;
            if (currentState.Equals(ScreenSpaceReflection.SSRSettings.performanceSettings))
                settingsMode = SettingsMode.Performance;
            else if (currentState.Equals(ScreenSpaceReflection.SSRSettings.defaultSettings))
                settingsMode = SettingsMode.Default;
            else if (currentState.Equals(ScreenSpaceReflection.SSRSettings.highQualitySettings))
                settingsMode = SettingsMode.HighQuality;

            EditorGUI.BeginChangeCheck();
<<<<<<< HEAD
            settingsMode = (SettingsMode)EditorGUILayout.EnumPopup("Preset", settingsMode);
            if (EditorGUI.EndChangeCheck())
                Apply(settingsMode);

            // move into the m_Settings fields...
            foreach (var property in m_Properties)
                EditorGUILayout.PropertyField(property);

            serializedObject.ApplyModifiedProperties();
=======
            settingsMode = (SettingsMode) EditorGUILayout.EnumPopup("Preset", settingsMode);
            if (EditorGUI.EndChangeCheck())
                Apply(settingsMode);
        
            DrawFields();
            serializedObject.ApplyModifiedProperties();
        }

        IEnumerable<SerializedProperty> GetProperties(LayoutAttribute.Category category)
        {
            return m_PropertyMap.Values.Where(x => x.Key.category == category).OrderBy(x => x.Key.priority).Select(x => x.Value);
        }

        private bool Header(String title, bool display)
        {
            Rect rect = GUILayoutUtility.GetRect(16f, 22f, m_Styles.header);
            GUI.Box(rect, title, m_Styles.header);

            Rect toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (Event.current.type == EventType.Repaint)
                EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
            
            Event e = Event.current;
            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }
            return display;
        }

        private void DrawFields()
        {
            foreach (var cat in m_CategoriesToShow)
            {
                var properties = GetProperties(cat.category);
                if (!properties.Any())
                    continue;
                
                GUILayout.Space(5);
                cat.display = Header(cat.category.ToString(), cat.display);

                if (!cat.display)
                    continue;
                
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUILayout.BeginVertical();
                GUILayout.Space(3);
                foreach (var field in GetProperties(cat.category))
                    EditorGUILayout.PropertyField(field);
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
>>>>>>> MultiPart
        }

        private void Apply(SettingsMode settingsMode)
        {
            switch (settingsMode)
            {
                case SettingsMode.Default:
                    Apply(ScreenSpaceReflection.SSRSettings.defaultSettings);
                    break;
                case SettingsMode.HighQuality:
                    Apply(ScreenSpaceReflection.SSRSettings.highQualitySettings);
                    break;
                case SettingsMode.Performance:
                    Apply(ScreenSpaceReflection.SSRSettings.performanceSettings);
                    break;
            }
        }

        private void Apply(ScreenSpaceReflection.SSRSettings settings)
        {
<<<<<<< HEAD
            var validTargets = targets.Where(x => x is ScreenSpaceReflection).Cast<ScreenSpaceReflection>().ToArray();

            Undo.RecordObjects(validTargets, "Apply SSR Settings");
            foreach (var validTarget in validTargets)
                validTarget.settings = settings;
=======
            foreach (var fieldKVP in m_PropertyMap)
            {
                var value = fieldKVP.Key.GetValue(settings);
                var fieldType = fieldKVP.Key.FieldType;

                if (fieldType == typeof (float))
                {
                    fieldKVP.Value.Value.floatValue = (float) value;
                }
                else if (fieldType == typeof (bool))
                {
                    fieldKVP.Value.Value.boolValue = (bool) value;
                }
                else if (fieldType == typeof (int))
                {
                    fieldKVP.Value.Value.intValue = (int) value;
                }
                else if (fieldType.IsEnum)
                {
                    fieldKVP.Value.Value.enumValueIndex = (int) value;
                }
                else
                {
                    Debug.LogErrorFormat("Encounted unexpected type {0} in application of settings", fieldKVP.Key.FieldType);
                }
            }
>>>>>>> MultiPart
        }
    }
}
