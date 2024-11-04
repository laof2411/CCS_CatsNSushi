using System.Linq;
using TMPro;
using UnityEngine;

public class GachaponUI : MonoBehaviour
{

    [SerializeField] private GameObject UIObjectToReturn;
    [SerializeField] private GameObject GachaponUIObject;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI collectibleText;

    [SerializeField] private CollectibleData[] collectibleDataArrays;
    [SerializeField] public GameObject meshObject;
    [SerializeField] private GameObject spawnerSlot;

    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject playButton;

    [SerializeField] private GameObject closeButton;

    private void Awake()
    {
        
        UpdateCoins();

    }

    private void Update()
    {

        UpdateCoins();

    }

    public void DeactivateGachapon()
    {

        GachaponUIObject.SetActive(false);
        UIObjectToReturn.SetActive(true);
        
    }

    public void PlayGachapon()
    {

        //Agregar que si ya los tiene todos no lo deje jugar.
        if(GameManager.instance.nekoins < 10 || !GameManager.instance.collectiblesArray.Contains(false)) { return; }

        GameManager.instance.nekoins -= 10;

        bool gotValidDrop = false;
        int collectibleSelected = 0;

        while (!gotValidDrop)
        {

            collectibleSelected = Random.Range(0, collectibleDataArrays.Length);
            if (GameManager.instance.collectiblesArray[collectibleSelected] != true)
            {

                gotValidDrop = true;

            }

        }

        collectibleText.text = collectibleDataArrays[collectibleSelected].collectibleName;
        collectibleText.gameObject.SetActive(true);

       // meshObject.mesh = collectibleDataArrays[collectibleSelected].collectibleMesh;
        meshObject = Instantiate(collectibleDataArrays[collectibleSelected].collectibleMesh, spawnerSlot.transform);
        meshObject.transform.position = new Vector3(spawnerSlot.transform.position.x,meshObject.transform.position.y,spawnerSlot.transform.position.z); 
        closeButton.GetComponent<GachaponUI>().meshObject = meshObject;
        meshObject.SetActive(true);


        GameManager.instance.collectiblesArray[collectibleSelected] = true;

        backButton.SetActive(false);
        playButton.SetActive(false);
        closeButton.SetActive(true);

        UpdateCoins();

    }

    public void CloseObjectLooking()
    {

        collectibleText.gameObject.SetActive(false);

        backButton.SetActive(true);
        playButton.SetActive(true);

        closeButton.SetActive(false);

        Destroy(meshObject);

    }

    public void UpdateCoins()
    {

        coinsText.text = GameManager.instance.nekoins.ToString() + " Nekoins";

    }

}
