using UnityEditor;

using UnityEngine;

///#IGNORE
//  This namespace is the base to all the editor classes of VRG packages
namespace VrGamesDev.Editor
{
    public class VRG_Editor_VRG_Remote : VRG_Editor
    {
        public new static string m_Prefabs = "Tools/Remote Config/Prefabs/";





        [MenuItem("Tools/Vr Games Dev/Remote Config/Add VRG_Remote Prefab", false, 1011)]
        public static void Add_VRG_Remote() => CreatePrefab(m_Prefabs + "VRG_Remote", true);

        [MenuItem("Tools/Vr Games Dev/Remote Config/REMOTE_CONFIG_INSTALLED: Add", false, 1031)]
        public static void Add_VRG_Remote_precompiled()
        {
            VRG_DefineSymbols.Add("REMOTE_CONFIG_INSTALLED");
            print("REMOTE_CONFIG_INSTALLED: Added ... Recompiling");
        }

        [MenuItem("Tools/Vr Games Dev/Remote Config/REMOTE_CONFIG_INSTALLED: Remove", false, 1032)]
        public static void Remove_VRG_Remote_precompiled()
        {
            VRG_DefineSymbols.Remove("REMOTE_CONFIG_INSTALLED");
            print("REMOTE_CONFIG_INSTALLED: Removed ... Recompiling");
        }

        [MenuItem("Tools/Vr Games Dev/Remote Config/VRG_Announcement/Add preconfigured VRG_Remote", false, 1051)]
        public static void Add_VRG_Remote_VRG_Announcement()
        {
            VRG_Remote go_Remote = CreateRemote("VRG_Announcement");

            if (go_Remote != null)
            {
                go_Remote.AddInt("VRG_Announcement.repeat", 0);

                go_Remote.AddString("VRG_Announcement.date", "2000-01-01");
                go_Remote.AddString("VRG_Announcement.title", "Added from Menu");
                go_Remote.AddString("VRG_Announcement.body", "Edit the VRG_Remote object added to customize this local message.<br><br>Remember to create the server version when you publish your game");
            }
        }






        








        [MenuItem("Tools/Vr Games Dev/Remote Config/VRG_Announcement/UI: Icon", false, 1111)]
        public static void Announcement_News() => CreatePrefabInCanvas(m_Prefabs + "Announcement/" + "VRG_Announcement - Icon");

        [MenuItem("Tools/Vr Games Dev/Remote Config/VRG_Announcement/UI: PopUp window", false, 1112)]
        public static void VRG_Announcement()
        {
            CreateVRG_EventSystem();

            CreateVRG_SkinPool();

            VRG_Announcement inScene_VRG_Announcement = GameObject.FindObjectOfType<VRG_Announcement>();

            if (inScene_VRG_Announcement == null)
            {
                 CreatePrefab(m_Prefabs + "Announcement/" + "VRG_Announcement", true);
                 inScene_VRG_Announcement = GameObject.FindObjectOfType<VRG_Announcement>();
            }
             else
            {
                 Debug.Log("<color=red>ERROR: </color> There is already a VRG_Announcement object in the scene");
            }

            VRG_Editor_VRG_Remote.Add_VRG_Remote_VRG_Announcement();            
        }




        public static VRG_Remote CreateRemote(string valueLocal)
        {
            bool IsNew = true;
            VRG_Remote go_Return = null;
            VRG_Remote[] go_Returns = GameObject.FindObjectsOfType<VRG_Remote>();

            foreach (VRG_Remote child in go_Returns)
            {
                if (child.id == valueLocal)
                {
                    IsNew = false;
                    break;
                }
            }

            if (IsNew)
            {
                GameObject go_InScene = CreatePrefab(m_Prefabs + "VRG_Remote", true);
                go_InScene.GetComponent<VRG_Remote>().id = valueLocal;

                go_Returns = GameObject.FindObjectsOfType<VRG_Remote>();
                foreach (VRG_Remote child in go_Returns)
                {
                    if (child.id == valueLocal)
                    {
                        go_Return = child;
                        break;
                    }
                }
            }
            else
            {
                print("<color=red>ERROR: </color> There is already a VRG_Remote - (" + valueLocal + ") object in the scene");
            }

            return go_Return;
        }

    }
}