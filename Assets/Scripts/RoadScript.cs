using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour {
    
    GameObject[] roads;
    Vector2[] roadPos;
    Sprite roadSprite;

    int roadCount;
    float minHeight;

	// Use this for initialization
	void Start ()
    {
        //number of pieces the road is made from
        roadCount = 3;

        //array of road sprites
        roads = new GameObject[roadCount];

        //sets the roads as the actual sprite gameobjects
        for (int i = 0; i < roadCount; i++)
        {
            roads[i] = transform.GetChild(i).gameObject;
        }

        //gets the sprite used so we can use the sizr of its bounds
        roadSprite = roads[0].GetComponent<SpriteRenderer>().sprite;

        //array of the positions of each road piece
        roadPos = new Vector2[roadCount];

        //individually sets the position of each road based on the sprite size
        roadPos[1] = Vector2.zero;
        roadPos[0] = new Vector2(0, roadPos[1].y + (roadSprite.bounds.size.y * 1.0f));
        roadPos[2] = new Vector2(0, roadPos[1].y - (roadSprite.bounds.size.y * 1.0f));

        //sets the road transforms to their positions
        for (int i = 0; i < roadCount; i++)
        {
            roads[i].transform.position = roadPos[i];
        }

        //the height which the roads move back to the top
        minHeight = -11.0f;

	}
	
	// Update is called once per frame
	void Update ()
    {
        //moves the roads forward with a certain speed
        Drive(0.25f * Time.deltaTime * 60);

        //if a road is too low move it to the top
        for (int i = 0; i < roadCount; i++)
        {
            if (roadPos[i].y < minHeight)
            {
                RearrangeRoads(i);
            }
        }
	}

    private void Drive(float speed)
    {
        //moves the roads downwards
        for (int i = 0; i < roadCount; i++)
        {
            roadPos[i] = new Vector2(roadPos[i].x, roadPos[i].y - speed);
            roads[i].transform.position = roadPos[i];
        }
    }

    private void RearrangeRoads(int roadNum)
    {
        //change the order of the roads
        int nextRoad = roadNum + 1;
        if (nextRoad > 2)
        {
            nextRoad = 0;
        }
        roadPos[roadNum] = new Vector2(roads[roadNum].transform.position.x, roads[nextRoad].transform.position.y + (roadSprite.bounds.size.y * 1.0f));
        roads[roadNum].transform.position = roadPos[roadNum];
    }
}
