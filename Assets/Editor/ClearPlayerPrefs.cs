using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class PlayerPrefsControl
    {
        [MenuItem("Game/Clear Player Prefs")]
        private static void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

    }
}