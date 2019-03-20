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
        roadCount = 3;

        roads = new GameObject[roadCount];
        for (int i = 0; i < roadCount; i++)
        {
            roads[i] = transform.GetChild(i).gameObject;
        }

        roadSprite = roads[0].GetComponent<SpriteRenderer>().sprite;

        roadPos = new Vector2[roadCount];

        roadPos[1] = Vector2.zero;
        roadPos[0] = new Vector2(0, roadPos[1].y + (roadSprite.bounds.size.y * 1.5f));
        roadPos[2] = new Vector2(0, roadPos[1].y - (roadSprite.bounds.size.y * 1.5f));


        for (int i = 0; i < roadCount; i++)
        {
            roads[i].transform.position = roadPos[i];
        }

        minHeight = -11.0f;

	}
	
	// Update is called once per frame
	void Update ()
    {
        Drive(0.25f);

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
        for (int i = 0; i < roadCount; i++)
        {
            roadPos[i] = new Vector2(roadPos[i].x, roadPos[i].y - speed);
            roads[i].transform.position = roadPos[i];
        }
    }

    private void RearrangeRoads(int roadNum)
    {
        int nextRoad = roadNum + 1;
        if (nextRoad > 2)
        {
            nextRoad = 0;
        }
        roadPos[roadNum] = new Vector2(roads[roadNum].transform.position.x, roads[nextRoad].transform.position.y + (roadSprite.bounds.size.y * 1.5f));
        roads[roadNum].transform.position = roadPos[roadNum];
    }
}
