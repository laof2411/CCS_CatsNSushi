using UnityEngine;

public class BlackCanvasManager : MonoBehaviour
{

    private void Awake()
    {

        DecreaseSize();

    }

    public void DecreaseSize()
    {

        GetComponent<Animator>().SetTrigger("BigToSmallTrigger");

    }

    public void IncreaseSize()
    {

        GetComponent<Animator>().SetTrigger("SmallToBigTrigger");

    }


}
