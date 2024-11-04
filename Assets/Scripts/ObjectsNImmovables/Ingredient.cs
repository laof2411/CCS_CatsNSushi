using UnityEngine;

public class Ingredient : Interactable, IPickable
{
    [SerializeField] public IngredientData data;
    public GameObject preparedGameObject;

    public IngredientStatus status;
    public IngredientType type;

    //public Color BaseColor => data.baseColor;

    //[SerializeField] private IngredientStatus startingStatus = IngredientStatus.Not_Ready;

    //public float ProcessTime => data.processTime;
    //public float CookTime => data.cookTime;
    //public Sprite SpriteUI => data.sprite;

    protected override void Awake()
    {
        base.Awake();

       // _meshRenderer = GetComponent<MeshRenderer>();
       // _meshFilter = GetComponent<MeshFilter>();
       Setup();
    }

    private void Setup()
    {

        status = data.startingStatus;
        type = data.type;
    //Status = IngredientStatus.Not_Ready;
        //_meshFilter.mesh = data.un_ReadyMesh;
        //_meshRenderer.material = data.ingredientMaterial;

        if (status == IngredientStatus.Fried)
        {
            ChangeToFried();
        }
        if(status == IngredientStatus.Cut)
        {
            ChangeToCut();
        }
    }

    public GameObject Pick(Transform playerSlot)
    {

        transform.parent = playerSlot;
        transform.SetPositionAndRotation(playerSlot.position, playerSlot.rotation);
        FindAnyObjectByType<LevelEndManager>().picked_pickables++;
        return this.gameObject;

    }

    public GameObject Place(GameObject interactable)
    {

        if(interactable.TryGetComponent<Plate>(out Plate _plt))
        {

            interactable.GetComponent<Plate>().AddIngredient(this);
            FindAnyObjectByType<LevelEndManager>().placed_pickables++;
            return null;

        }
        else
        {

            Transform interactableSlot = interactable.GetComponent<Interactable>().slot;

            transform.SetParent(interactableSlot);
            transform.SetPositionAndRotation(interactableSlot.position, interactableSlot.rotation);
            interactable.GetComponent<Interactable>().currentSonInteractable = this.gameObject;
            FindAnyObjectByType<LevelEndManager>().placed_pickables++;
            return null;

        }


        
    }

    //public override GameObject Pick(Transform playerSlot)
    //{
    //    _rigidbody.isKinematic = true;
    //    _collider.enabled = false;

    //    return this.gameObject;
    //}

    //public override void Place(Transform interactableTransform, GameObject parentInteractable)
    //{
    //    gameObject.transform.SetParent(null);
    //    _rigidbody.isKinematic = false;
    //    _collider.enabled = true;
    //}

    public void ChangeToFried()
    {
        status = IngredientStatus.Fried;
        //_meshFilter.mesh = data.processedMesh;
    }

    public void ChangeToCut()
    {
        status = IngredientStatus.Cut;
        //_meshFilter.mesh = data.processedMesh;
    }

    //public void ChangeToCooked()
    //{
    //    Status = IngredientStatus.Cooked;
    //    var cookedMesh = data.cookedMesh;
    //    if (cookedMesh == null) return;

    //    _meshFilter.mesh = cookedMesh;
    //    SetMeshRendererEnabled(true);
    //}

    //public void SetMeshRendererEnabled(bool enable)
    //{
    //    _meshRenderer.enabled = enable;
    //}

    //public override void TryToPlaceIntoSlot(IPickable pickableToDrop)
    //{
    //    Ingredients normally don't get any pickables dropped into it.
    //     Debug.Log("[Ingredient] TryToDrop into an Ingredient isn't possible by design");

    //}

    //public override IPickable TryToPickUpFromSlot(IPickable playerHoldPickable)
    //{
    //    // Debug.Log($"[Ingredient] Trying to PickUp {gameObject.name}");
    //    _rigidbody.isKinematic = true;
    //    return this;
    //}
}
