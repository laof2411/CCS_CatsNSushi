
public class Countertop : Interactable
{

    public override void Interact()
    {

        if(currentSonInteractable != null && currentSonInteractable.TryGetComponent<Interactable>(out Interactable _int)) 
        {

            currentSonInteractable.GetComponent<Interactable>().Interact();

        }
        
    }

}
