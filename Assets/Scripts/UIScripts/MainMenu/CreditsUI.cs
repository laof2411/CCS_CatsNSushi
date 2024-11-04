using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{

    [SerializeField] private GameObject startingMenuUIObject;
    [SerializeField] private GameObject CreditsUIObject;
    
    public void DeactivateCredits()
    {

        startingMenuUIObject.SetActive(true);
        CreditsUIObject.SetActive(false);

    }

}
