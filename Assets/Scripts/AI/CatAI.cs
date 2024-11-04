using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CatAI : MonoBehaviour
{
    public Transform slot;

    public Transform[] enterPathing;
    public Transform[] leavingPathing;
    public Seat assignedSeat;

    private bool isEntering = false;
    private bool isLeaving = false;
    private bool isWaitingForFood = false;
    private bool isEatingFood = false;
    private bool isReturningPlate = false;

    private int enterPathingNumber = 0;
    private int leavingPathingNumber = 0;
    public float elapsedTime = 0;

    public float desiredDuration = 3f;

    [SerializeField] private float catSpeed = 0.01f;

    [SerializeField] private float waitingTime = 40f;
    [SerializeField] private float currentWaitingTime = 40f;

    [SerializeField] private float eatingTime = 10f;
    [SerializeField] private float currentEatingTime = 0f;

    public GameObject foodUI;

    [SerializeField] private Recipes currentRecipeAsked;

    [SerializeField] private Animator animatorAI;
    [SerializeField] private int foodNumber;

    private int coins = 0;

    private int currentDesesperation = 0;

    [SerializeField] private float stressAmountPerCat = 0.2f;

    [SerializeField] private AudioSource angryCat;
    [SerializeField] private AudioSource foodCat;

    private void Awake()
    {
        foodNumber = Random.Range(1, 3);
        coins = (foodNumber * 2);

        StartCoroutine(EnterCoroutine());
    }

    private void SelectPathing()
    {
        assignedSeat = GameObject.FindAnyObjectByType<SeatManager>().AssignSeat();
        enterPathing = assignedSeat.enteringPath;
        leavingPathing = assignedSeat.leavingPath;
    }

    private IEnumerator EnterCoroutine()
    {
        SelectPathing();
        isEntering = true;

        while (isEntering == true)
        {

            transform.LookAt(new Vector3(enterPathing[enterPathingNumber].position.x, transform.position.y, enterPathing[enterPathingNumber].position.z));
            transform.position = Vector3.MoveTowards(transform.position, enterPathing[enterPathingNumber].position, catSpeed * Time.deltaTime);
            animatorAI.Play("Walk_Npc");

            if (transform.position == enterPathing[enterPathingNumber].position && (enterPathingNumber + 1) != enterPathing.Length)
            {

                enterPathingNumber++;

            }
            if(transform.position == enterPathing[enterPathing.Length - 1].position)
            {

                isEntering = false;
                StartCoroutine(JumpToChairCoroutine());

            }
            yield return null;

        }

        yield return null;

    }

    private IEnumerator JumpToChairCoroutine()
    {

        animatorAI.Play("Jump_chair");
        transform.LookAt(new Vector3(assignedSeat.seatingPosition.position.x, transform.position.y, assignedSeat.seatingPosition.position.z));

        FindAnyObjectByType<TutorialManager>().ActivateTutorial(3); 

        yield return new WaitForSeconds(2.25f);

        Tween _tween = transform.DOJump(assignedSeat.seatingPosition.position, 0.75f, 1, 0.75f);
        yield return _tween.WaitForCompletion();      

        AskForFood();


    }

    private void AskForFood()
    {

        FindAnyObjectByType<TutorialManager>().ActivateTutorial(1);
        FindAnyObjectByType<TutorialManager>().ActivateTutorial(6);
        FindAnyObjectByType<TutorialManager>().ActivateTutorial(7);
        FindAnyObjectByType<TutorialManager>().ActivateTutorial(8);
        FindAnyObjectByType<TutorialManager>().ActivateTutorial(9);

        foodCat.Play();
        currentEatingTime = 0;
        animatorAI.Play("idle1");
        foodNumber--;
        currentRecipeAsked = GameManager.instance.currentLevelData.recipesUsed[Random.Range(0, GameManager.instance.currentLevelData.recipesUsed.Length)];

        RestoreWhiteColor();
        foodUI.transform.GetChild(1).GetComponent<Image>().sprite = currentRecipeAsked.recipeSprite;
        foodUI.SetActive(true);

        currentWaitingTime = waitingTime;
        StartCoroutine(WaitForFoodCoroutine());

    }

    private IEnumerator WaitForFoodCoroutine()
    {

        isWaitingForFood = true;

        while (isWaitingForFood)
        {
            currentWaitingTime -= Time.deltaTime;

            if (assignedSeat.conveyor.currentSonInteractable != null && assignedSeat.conveyor.currentSonInteractable.TryGetComponent<Plate>(out Plate _plt))
            {

                if (assignedSeat.conveyor.currentSonInteractable.GetComponent<Plate>().currentRecipe == currentRecipeAsked)
                {

                    isWaitingForFood = false;

                    StartCoroutine(EatingFoodCoroutine());

                    assignedSeat.conveyor.currentSonInteractable.transform.SetParent(slot);
                    assignedSeat.conveyor.currentSonInteractable.transform.position = slot.position;
                    assignedSeat.conveyor.currentSonInteractable = null;
                    foodUI.SetActive(false);

                }

            }

            if (currentWaitingTime <= 20 && currentDesesperation <= 0)
            {

                WhiteToOrange();

            }
            else if (currentWaitingTime <= 10 && currentDesesperation <= 1)
            {

                OrangeToRed();

            }
            else if (currentWaitingTime <= 0)
            {

                FailFoodDelivery();
                isWaitingForFood = false;

            }

            yield return null;
        }

        yield return null;

    }

    private IEnumerator EatingFoodCoroutine()
    {

        FindAnyObjectByType<LevelEndManager>().IncreaseOrdersDelivered();
        isEatingFood = true;
        animatorAI.Play("Make");

        while (isEatingFood)
        {

            currentEatingTime += Time.deltaTime;
            if (currentEatingTime >= eatingTime)
            {

                isEatingFood = false;
                slot.transform.GetChild(0).gameObject.GetComponent<Plate>().SetDirty();
                slot.transform.GetChild(0).gameObject.GetComponent<Plate>().RemoveRecipe();

                FindAnyObjectByType<TutorialManager>().ActivateTutorial(4);

                StartCoroutine(ReturningPlateCoroutine());

            }
            yield return null;

        }
        yield return null;

    }

    private IEnumerator ReturningPlateCoroutine()
    {

        isReturningPlate = true;

        while(isReturningPlate)
        {

            if (assignedSeat.conveyor.currentSonInteractable == null)
            {

                assignedSeat.conveyor.currentSonInteractable = slot.transform.GetChild(0).gameObject;
                slot.transform.GetChild(0).position = assignedSeat.conveyor.slot.position;
                slot.transform.GetChild(0).SetParent(assignedSeat.conveyor.slot);

                isReturningPlate = false;

                if (foodNumber <= 0)
                {

                    StartCoroutine(LeaveSeatCoroutine());
                    FindObjectOfType<LevelEndManager>().GainNekoins(coins);

                }
                else
                {

                    AskForFood();

                }

            }
            yield return null;

        }
        
        yield return null;

    }

    private IEnumerator LeaveSeatCoroutine()
    {

        Tween _temptween = transform.DOLookAt(new Vector3(leavingPathing[0].position.x, transform.position.y, leavingPathing[0].position.z),0.5f);

        yield return _temptween.WaitForCompletion();

        animatorAI.Play("get up");
        _temptween = transform.DOJump(enterPathing[enterPathing.Length-1].position,0.75f,1,1f);

        yield return _temptween.WaitForCompletion();
        StartCoroutine(LeavingCoroutine());
    }

    private IEnumerator LeavingCoroutine()
    {

        isLeaving = true;

        while (isLeaving)
        {

            transform.LookAt(new Vector3(leavingPathing[leavingPathingNumber].position.x, transform.position.y, leavingPathing[leavingPathingNumber].position.z));
            transform.position = Vector3.MoveTowards(transform.position, leavingPathing[leavingPathingNumber].position, catSpeed * Time.deltaTime);
            animatorAI.Play("Walk_Npc");

            if (transform.position == leavingPathing[leavingPathingNumber].position && (leavingPathingNumber + 1) != leavingPathing.Length)
            {

                leavingPathingNumber++;

            }

            if (transform.position == leavingPathing[leavingPathing.Length - 1].position && (enterPathingNumber + 1) == enterPathing.Length)
            {

                FindAnyObjectByType<TutorialManager>().ActivateTutorial(10);
                FindAnyObjectByType<Spawner>().DespawnCat(this.gameObject, foodUI);

            }
            yield return null;

        }

        yield return null;

    }  

    private void FailFoodDelivery()
    {

        angryCat.Play();
        FindObjectOfType<StressManager>().AddStress(stressAmountPerCat);
        FindAnyObjectByType<LevelEndManager>().IncreaseAngryCostumers();
        foodUI.SetActive(false);
        StartCoroutine(LeaveSeatCoroutine());

    }

    private void RestoreWhiteColor()
    {

        foodUI.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        currentDesesperation = 0;
    }

    private void WhiteToOrange()
    {

        foodUI.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 165, 0);
        coins--;
        currentDesesperation++;

    }

    private void OrangeToRed()
    {

        foodUI.transform.GetChild(0).GetComponent<Image>().color = Color.red;
        currentDesesperation++;
        coins--;

    }

}
