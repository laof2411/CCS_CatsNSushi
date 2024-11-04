using UnityEditor;

using UnityEngine;

///#IGNORE
//  This namespace is the base to all the editor classes of VRG packages
namespace VrGamesDev.Editor
{
    public class VRG_Editor_AboutUs : VRG_Editor
    {
        public new static string m_Prefabs = "Tools/CORE/Prefabs/";






        [MenuItem("Tools/Vr Games Dev/Examples/Instructions to use examples", false, 99000)]
        public static void Example_99900()
        {
            if (EditorUtility.DisplayDialog("Vr Games Dev Examples", "May edit or add scenes to the build settings, and/or move some assets into the addressables groups.\n\n Save your current work before loading any example.", "Yes, I got it", "No"))
            {
            }
        }

        [MenuItem("Tools/Vr Games Dev/About/Installation path", false, 99001)]
        public static void Installation_path() => print("The installation path of Vr Games Dev Packages is: " + CalculateInstallationPath());

        [MenuItem("Tools/Vr Games Dev/About/Us and this software", false, 99002)]
        public static void About()
        {
            // Get existing open window or if none, make a new one:
            VRG_WindowStatus window = (VRG_WindowStatus)EditorWindow.GetWindow(typeof(VRG_WindowStatus), false, "About VrGamesDev");

            //window.maxSize = new Vector2(325f, 450f);
            //window.minSize = window.maxSize;

            window.Show();
        }
        



    }
}