using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VrGamesDev;

public class GetCloudInfo : MonoBehaviour
{

    public float CatWaitingTime = 0;
    public float CookingTime = 0;
    public float CuttingTime = 0;
    public float FryingTime = 0;
    public float Level1Time = 0;
    public float Level2Time = 0;
    public float Level3Time = 0;
    public float Level4Time = 0;
    public float Level5Time = 0;
    public float Level6Time = 0;

    public float waitingTime = 0.5f;

    public void Awake()
    {

        Invoke(nameof(GetValues), waitingTime);

    }

    private void GetValues()
    {

        this.CatWaitingTime = VRG_Remote.GetFloat("CatWaitingTime");
        this.CookingTime = VRG_Remote.GetFloat("CookingTime");
        this.CuttingTime = VRG_Remote.GetFloat("CuttingTime");
        this.FryingTime = VRG_Remote.GetFloat("FryingTime");
        this.Level1Time = VRG_Remote.GetFloat("Level1Time");
        this.Level2Time = VRG_Remote.GetFloat("Level2Time");
        this.Level3Time = VRG_Remote.GetFloat("Level3Time");
        this.Level4Time = VRG_Remote.GetFloat("Level4Time");
        this.Level5Time = VRG_Remote.GetFloat("Level5Time");
        this.Level6Time = VRG_Remote.GetFloat("Level6Time");

    }

}
