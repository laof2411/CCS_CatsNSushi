using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{

    public bool occupied = false;
    public Transform[] enteringPath;
    public Transform[] leavingPath;

    public Transform seatingPosition;

    public Interactable conveyor;

}
