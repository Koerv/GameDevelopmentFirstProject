using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurningPoint : MonoBehaviour {

    public List<Vector3> directions;

    public Vector3 newDirection()
    {
        //TODO: Add Booleans to decide the walking direction?
        //return one random Value of List
        return directions[Random.Range(0, directions.Count)];
    }
}
