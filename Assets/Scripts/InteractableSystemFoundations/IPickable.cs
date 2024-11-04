using UnityEngine;


public interface IPickable
{

    public GameObject Pick(Transform playerSlot);

    public GameObject Place(GameObject parentInteractable);    

}
