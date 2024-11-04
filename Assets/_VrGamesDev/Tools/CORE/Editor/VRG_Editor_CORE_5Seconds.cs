using UnityEditor;

using UnityEditor.SceneManagement;

///#IGNORE
//  This namespace is the base to all the editor classes of VRG packages
namespace VrGamesDev.Editor
{
    public class VRG_Editor_CORE_5Seconds : VRG_Editor
    {
        public new static string m_Prefabs = "Tools/CORE/Prefabs/";

        [MenuItem("Tools/Vr Games Dev/Examples/5 Seconds/Game: Load", false, 100002)]
        public static void Example_100002()
        {
            AddScenesToBuildSettings(new string[]
            {
                "5 Seconds/Scenes/" + "Home",
                "5 Seconds/Scenes/" + "VRG_Managers",
                "5 Seconds/Scenes/" + "Campaign"
            });

            LoadScene("5 Seconds/Scenes/Home");
        }
        
        [MenuItem("Tools/Vr Games Dev/Examples/5 Seconds/Game: Remove", false, 100003)]
        public static void Example_100003()
        {
            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);

            RemoveScenesFromoBuildSettings(new string[]
            {
                "5 Seconds/Scenes/" + "Home",
                "5 Seconds/Scenes/" + "VRG_Managers",
                "5 Seconds/Scenes/" + "Campaign"
            });
        }

    }
}