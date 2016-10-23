using UnityEngine;
using System.Collections.Generic;

public class TurningPoint : MonoBehaviour {


    public bool down, left_downward, right_downward, up, left_upward, right_upward;
    List<Vector3> directions;

    public Vector3 newDirection()
    {
        directions = new List<Vector3>();
        if (GameManager.instance.wayDown) { 
            if (down)
                directions.Add(new Vector3(0, -1, 0));
            if (left_downward)
                directions.Add(new Vector3(-1, 0, 0));
            if (right_downward)
                directions.Add(new Vector3(1, 0, 0));
        }
        else
        {
            if(up)
                directions.Add(new Vector3(0, 1, 0));
            if(left_upward)
                directions.Add(new Vector3(-1, 0, 0));
            if(right_upward)
                directions.Add(new Vector3(1, 0, 0));
        }
        //TODO: Add Booleans to decide the walking direction?
        //return one random Value of List
        Debug.Log("Path index: " +Random.Range(0, directions.Count));
        return directions[(Random.Range(0, directions.Count))];
    }
}
