using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatManager : MonoBehaviour
{

    [SerializeField] private Seat[] seats;
    
    public Seat AssignSeat()
    {

        bool validSeat = false;

        while (!validSeat)
        {

            int seatNumber = Random.Range(0, seats.Length);
            if (seats[seatNumber].occupied == false)
            {

                validSeat = true;
                seats[seatNumber].occupied = true;
                return seats[seatNumber];

            }
            
        }

        return null;

    }
    

}
