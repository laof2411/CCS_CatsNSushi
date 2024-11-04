using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform UIParent;

    [SerializeField] private int catAmount;

    [SerializeField] private GameObject catObject;
    [SerializeField] private GameObject UIObject;
    [SerializeField] private GameObject currentCat;
    [SerializeField] private Material[] catTextures;
    [SerializeField] private int catAccesoriesAmount;

    [SerializeField] private float timeBetweenSpawns;

    public void CallSpawnerCoroutine()
    {
        
        StartCoroutine(SpawnerCoroutine());

    }

    private IEnumerator SpawnerCoroutine()
    {

        while (true)
        {

            SpawnCat();
            yield return new WaitForSeconds(timeBetweenSpawns);
            
        }

    }

    public void SpawnCat()
    {

        if (GameManager.instance.currentLevelData.plateAmount == catAmount) 
        {
            Debug.Log("Return");
            return; 
        }
        catAmount++;
        currentCat = Instantiate(catObject, spawnLocation.position, spawnLocation.rotation);
        GameObject temporalUI = Instantiate(UIObject, UIParent);
        currentCat.GetComponent<CatAI>().foodUI = temporalUI;
        temporalUI.GetComponent<LookAtCamera>().targetToFollow = currentCat.transform;

        ChangeCatTextures();
        ActivateCatAccesory();

        FindAnyObjectByType<LevelEndManager>().catsEntered++;

    }

    public void DespawnCat(GameObject cat,GameObject UI)
    {

        Destroy(cat);
        Destroy(UI);
        catAmount--;

    }

    private void ChangeCatTextures()
    {
        Material catMaterial = catTextures[Random.Range(0, catTextures.Length)];
        //cola
        currentCat.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = catMaterial;
        //left oreja
        currentCat.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = catMaterial;
        //right oreja
        currentCat.transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = catMaterial;
        //left brazo
        currentCat.transform.GetChild(0).transform.GetChild(5).transform.GetChild(1).GetComponent<MeshRenderer>().material = catMaterial;
        //right brazo
        currentCat.transform.GetChild(0).transform.GetChild(5).transform.GetChild(0).GetComponent<MeshRenderer>().material = catMaterial;
        //left pie
        currentCat.transform.GetChild(0).transform.GetChild(6).transform.GetChild(0).GetComponent<MeshRenderer>().material = catMaterial;
        //right pie
        currentCat.transform.GetChild(0).transform.GetChild(6).transform.GetChild(1).GetComponent<MeshRenderer>().material = catMaterial;
        //torso
        currentCat.transform.GetChild(0).transform.GetChild(1).transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material = catMaterial;
    }

    private void ActivateCatAccesory()
    {

        currentCat.transform.GetChild(0).GetChild(4).GetChild(0).GetChild(Random.Range(0, catAccesoriesAmount)).gameObject.SetActive(true);

    }
}
