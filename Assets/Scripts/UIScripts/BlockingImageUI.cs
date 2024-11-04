using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingImageUI : MonoBehaviour
{

    private void Awake()
    {

        if (FindAnyObjectByType<LevelEndManager>().wonLevel)
        {

            Destroy(this.gameObject);

        }

    }

}
