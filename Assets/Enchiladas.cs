using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VrGamesDev;

public class Enchiladas : MonoBehaviour
{
    public int enchiladas = 5;

    void Awake()
    {

        Invoke("GetValue", 0.25f);
        
    }

    private void GetValue()
    {


        this.enchiladas = VRG_Remote.GetInt("Enchiladas");

    }
}
