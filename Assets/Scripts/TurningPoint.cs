using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurningPoint : MonoBehaviour {

    public List<Vector3> directions;

    public Vector3 newDirection()
    {
        //return one random Value of List
        return directions[Random.Range(0, directions.Count)];
    }
}
