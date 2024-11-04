using UnityEditor;

///#IGNORE
//  This namespace is the base to all the editor classes of VRG packages
namespace VrGamesDev.Editor
{
    public class VRG_Editor_VRG_Remote_Examples : VRG_Editor
    {
        public new static string m_Prefabs = "Tools/CORE/Prefabs/";
        


        [MenuItem("Tools/Vr Games Dev/Examples/Remote Config/01 UI Text", false, 99932)]
        public static void Example_99901() => LoadScene("Remote Config/Examples/Scenes/" + "01 UI Text");


        [MenuItem("Tools/Vr Games Dev/Examples/Remote Config/02 A simple combat", false, 99933)]
        public static void Example_99902() => LoadScene("Remote Config/Examples/Scenes/" + "02 A simple combat");


        [MenuItem("Tools/Vr Games Dev/Examples/Remote Config/03 VRG_Announcement", false, 99934)]
        public static void Example_99903() => LoadScene("Remote Config/Examples/Scenes/" + "03 VRG_Announcement");




    }
}