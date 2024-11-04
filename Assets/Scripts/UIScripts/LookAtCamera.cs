using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform mainCamera;
    public Transform targetToFollow;
    [SerializeField] private float followYDisntace = 2f;

    public void Awake()
    {

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    public void Update()
    {
        
        transform.LookAt(new Vector3(mainCamera.position.x, transform.position.y, mainCamera.position.z));
        transform.position = new Vector3(targetToFollow.position.x, targetToFollow.position.y + followYDisntace, targetToFollow.position.z);

    }

}
