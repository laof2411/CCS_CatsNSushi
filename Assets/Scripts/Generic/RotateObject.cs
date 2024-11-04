using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField] float speed = 15f;


    private void Update()
    {

        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));


    }
}
