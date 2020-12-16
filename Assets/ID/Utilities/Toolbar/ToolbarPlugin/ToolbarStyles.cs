using UnityEngine;

namespace ID.Runtime.Utilities.Toolbar.ToolbarPlugin
{
    static class ToolbarStyles
    {
        public static readonly GUIStyle CommandButtonStyle;

        static ToolbarStyles()
        {
            CommandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
                stretchWidth = true
            };
        }
    }
}