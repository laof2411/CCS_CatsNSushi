using UnityEditor;

///#IGNORE
//  This namespace is the base to all the editor classes of VRG packages
namespace VrGamesDev.Editor
{
    public class VRG_Editor_CORE_Examples : VRG_Editor
    {
        public new static string m_Prefabs = "Tools/CORE/Prefabs/";
        





        [MenuItem("Tools/Vr Games Dev/Examples/CORE/00 Demo", false, 99931)]
        public static void Example_1020() => LoadScene("CORE/Examples/Scenes/" + "00 Demo");
        
        [MenuItem("Tools/Vr Games Dev/Examples/CORE/00 UI", false, 99932)]
        public static void Example_1021() => LoadScene("CORE/Examples/Scenes/" + "01 UI");

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/02 Graphical Numbers", false, 99933)]
        public static void Example_1022() => LoadScene("CORE/Examples/Scenes/" + "02 Graphical Numbers");

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/03 Camera And Fader", false, 99934)]
        public static void Example_1023() => LoadScene("CORE/Examples/Scenes/" + "03 Camera And Fader");

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/04 Scene Managment", false, 99935)]
        public static void Example_1024()
        {
            LoadScene("CORE/Examples/Scenes/" + "04 Scene Managment");

            AddScenesToBuildSettings(new string[]
            {
                "CORE/Examples/Scenes/04 Scene Managment/" + "04 Scenes managment 1",
                "CORE/Examples/Scenes/04 Scene Managment/" + "04 Scenes managment 2"
            }, false);
        }

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/05 Sounds and music", false, 99961)]
        public static void Example_1025() => LoadScene("CORE/Examples/Scenes/" + "05 Sounds and music");

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/06 PopUp and Exit", false, 99962)]
        public static void Example_1026() => LoadScene("CORE/Examples/Scenes/" + "06 PopUp and Exit");





        [MenuItem("Tools/Vr Games Dev/Examples/CORE/07 VRG_SessionData", false, 99991)]
        public static void Example_3021() => LoadScene("CORE/Examples/Scenes/" + "07 VRG_SessionData");

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/08 VRG_SessionData UI", false, 99992)]
        public static void Example_3022() => LoadScene("CORE/Examples/Scenes/" + "08 VRG_SessionData UI");

        [MenuItem("Tools/Vr Games Dev/Examples/CORE/09 Skins", false, 100021)]
        public static void Example_4020() => LoadScene("CORE/Examples/Scenes/" + "09 Skins");





        [MenuItem("Tools/Vr Games Dev/Examples/CORE/10 Bhel", false, 100022)]
        public static void Example_5020() => LoadScene("CORE/Examples/Scenes/" + "10 Bhel");
        

    }
}