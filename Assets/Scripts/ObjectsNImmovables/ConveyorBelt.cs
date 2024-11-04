using System.Collections;
using UnityEngine;

public class ConveyorBelt : Interactable
{

    [SerializeField] ConveyorBelt nextConveyor;

    public float conveyorSpeed = 0.01f;

    public void Update()
    {

        if(currentSonInteractable != null)
        {

            currentSonInteractable.transform.position = Vector3.MoveTowards(currentSonInteractable.transform.position, nextConveyor.slot.position, conveyorSpeed * Time.deltaTime);

            if (currentSonInteractable.transform.position == nextConveyor.slot.position)
            {

                nextConveyor.currentSonInteractable = currentSonInteractable;
                currentSonInteractable = null;

            }

        }

    }

    public override void Interact()
    {

        //Do Nothing

    }

}
