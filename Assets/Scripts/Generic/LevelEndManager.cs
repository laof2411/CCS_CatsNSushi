using TMPro;
using UnityEngine;

public class LevelEndManager : MonoBehaviour
{

    public int ordersDelivered = 0;
    public int angryCostumers = 0;
    public int nekoinsGained = 0;
    public int foodThrown = 0;

    public int catsEntered = 0;

    public int cooked_times = 0;

    public int octopus_cut = 0;
    public int salmon_cut = 0;
    public int shrimp_cut = 0;

    public int cleaned_plates = 0;
    public int dirty_plates = 0;

    public int placed_pickables = 0;
    public int picked_pickables = 0;


    public bool wonLevel = false;

    public int levelCalification = 0;

    [SerializeField] private GameObject endUI;

    public void WinLevel()
    {

        UploadGM();
        wonLevel = true;
        CalculateLevelCalification();
        endUI.SetActive(true);
        endUI.GetComponent<Animator>().Play("EndingAnimation");

        if (levelCalification >= 10)
        {

            endUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Prrufect!";

        }
        else if (levelCalification < 10 && levelCalification >= 5)
        {

            endUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Grrreat";

        }
        else if (levelCalification < 5)
        {

            endUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Good";

        }

        endUI.transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ordersDelivered.ToString() + " Delivered Orders";
        endUI.transform.GetChild(6).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = angryCostumers.ToString() + " Angry Costumers";
        endUI.transform.GetChild(6).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = nekoinsGained.ToString() + " Nekoins Gained";
        endUI.transform.GetChild(6).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = foodThrown.ToString() + " Food Thrown";

    }

    public void LoseLevel()
    {

        endUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Bad";

        endUI.transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ordersDelivered.ToString() + " Delivered Orders";
        endUI.transform.GetChild(6).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = angryCostumers.ToString() + " Angry Costumers";
        endUI.transform.GetChild(6).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = nekoinsGained.ToString() + " Nekoins Gained";
        endUI.transform.GetChild(6).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = foodThrown.ToString() + " Food Thrown";

    }

    private void UploadGM()
    {

        GameManager.instance.nekoins += nekoinsGained;

    }

    public void GainNekoins(int newNekoins)
    {

        nekoinsGained += newNekoins;
        FindAnyObjectByType<LevelCanvas>().UpdateCoinsUI(nekoinsGained);

    }

    public void IncreaseOrdersDelivered()
    {

        ordersDelivered += 1;

    }

    public void IncreaseAngryCostumers()
    {

        angryCostumers += 1;

    }

    public void IncreaseFoodThrown()
    {

        foodThrown += 1;

    }

    private void CalculateLevelCalification()
    {

        levelCalification += nekoinsGained;
        levelCalification += ordersDelivered * 2;
        levelCalification -= angryCostumers;
        levelCalification -= foodThrown;

    }


}
