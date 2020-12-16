using ID.Runtime.Utilities.Toolbar.ToolbarPlugin;
using UnityEditor;
using UnityEngine;

namespace ID.Runtime.Utilities.Toolbar
{
    [InitializeOnLoad]
    public static class ToolbarEditor
    {
        static ToolbarEditor()
        {
            ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
            ToolbarExtender.RightToolbarGUI.Add(DrawRightGUI);
        }

        static void DrawLeftGUI()
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("Seleccionar escena");
        }
        
        static void DrawRightGUI()
        {
            OpenAddTermWindow();
            OpenLocalizationWindow();
        }

        static void OpenLocalizationWindow()
        {
            GUILayout.Space(2);
            var worldIcon = (Texture)AssetDatabase.LoadAssetAtPath ("Assets/ID/Textures/Icons/Common/Book.png", typeof(Texture));
            var guiContent = new GUIContent(worldIcon, "Opens the localization system.");
            var hasPressedLocalizationButton = GUILayout.Button(guiContent, ToolbarStyles.CommandButtonStyle);
            if (hasPressedLocalizationButton)
            {
                //LocalizationSystemWindow.OpenWindow();
            }
            GUILayout.Space(60);
        }
        
        private static void OpenAddTermWindow()
        {
            GUILayout.FlexibleSpace();
            var searchIcon = (Texture)AssetDatabase.LoadAssetAtPath ("Assets/ID/Textures/Icons/Navigation/Search.png", typeof(Texture));
            var guiContent = new GUIContent(searchIcon, "Opens the window for adding a new id and terms for all the languages.");
            var hasPressedAddTermButton = GUILayout.Button(guiContent, ToolbarStyles.CommandButtonStyle);
            if (hasPressedAddTermButton)
            {
                //TermsWindow.OpenWindow();
            }
        }
    }
}