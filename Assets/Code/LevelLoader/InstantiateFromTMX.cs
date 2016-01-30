﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using KWorks.Wrappers;

public class InstantiateFromTMX : MonoBehaviour {

    public TextAsset mapa;

    public GameObject wall;
    public GameObject player;
    public GameObject enemy;
    public GameObject spikes;
    public GameObject waypoint;
    public GameObject floor;
	public GameObject death;
    public GameObject salida;	

    private int rows;
    private int columns;

	// Use this for initialization
	void Awake () {
        string[,] tiles = CSVReader.SplitCsvGrid(mapa.text);
        CSVReader.DebugOutputGrid(tiles);
	
		rows = tiles.GetLength(1) - 1;
		columns = tiles.GetLength(0) - 1;

		Debug.Log("Loading map... rows[" + rows + "] colums [" + columns +"]");

        float offset = 0.75f;
        GameObject wp= null;

        Dictionary<string, Transform> wps = new Dictionary<string, Transform>();

        Dictionary<string, Vector2> enemies = new Dictionary<string, Vector2>();
        //Regex identifyWypoint = new Regex("[0-9][0-9]");
        
		bool extendingWall = false;
		Vector2 wallStartPoint = new Vector2(0,0);
		int wallSize = 0;
        
		for (int l = 0; l < rows; l++) {
			
			if (extendingWall) {
				//Instantiate wall
				instantiateWall(wall, wallStartPoint, wallSize);
				wallSize = 0;
				extendingWall = false;
			}			
			
			for(int k=0;k < columns-1;k++) {
                string val = tiles[k, (rows - l - 1)];
				
				Debug.Log("Loading map... rows[" + (rows - l - 1) + "] colums [" + l +"] value [" + val + "]" );
                
				if (!val.Equals("WW") && extendingWall) {
					instantiateWall(wall, wallStartPoint, wallSize);
					wallSize = 0;
					extendingWall = false;
				}
								
				if (val.Equals("WW")) {
					if (!extendingWall) {
						wallStartPoint.x = k * offset;
						wallStartPoint.y = l * offset;
					}
					wallSize++;
                } else if (val.Equals("PP")) {
                    GameObject.Instantiate(player, new Vector3(k * offset, l * offset, 0), Quaternion.identity);
                } else if (val.Contains("E")) {
                    enemies.Add(val, new Vector2(k, l)); 
                }               
				else if (val.Equals("DD")) {
					GameObject.Instantiate(death, new Vector3(k * offset, l * offset,0), Quaternion.identity);
				} else if (val.Equals("SP")) {
					GameObject.Instantiate(spikes, new Vector3(k * offset, l * offset,0), Quaternion.identity);
				}               
                else if (Regex.IsMatch(val, "[0-9][0-9]"))
                {
                    wp = (GameObject)Instantiate(waypoint, new Vector3(k * offset, l * offset,0), Quaternion.identity);
                    wps.Add(val, wp.transform);
                }
            }
        }
        Debug.Log(enemies.Count);
        Debug.Log(wps.Count);
        foreach (KeyValuePair<string, Vector2> newenemy in enemies) {

            /*GameObject enemyGO = (GameObject)*/GameObject.Instantiate(enemy, new Vector3(newenemy.Value.x * offset, 0.5f, newenemy.Value.y * offset), Quaternion.identity);
            
            List<Transform> wpss = new List<Transform>();
            foreach (KeyValuePair<string, Transform> wayp in wps)
            {
                if (wayp.Key[0] == newenemy.Key[1])
                {
                    wpss.Add(wayp.Value);
                }
            }           
            //enemyGO.GetComponent<EnemyStateMachine>().addWaypoints(wpss);
        }
	}    


    // Update is called once per frame
    void Update () {
	

	}
	
	private void instantiateWall(GameObject wall, Vector2 wallStartPoint, int wallSize)
    {
        GameObject newWall = (GameObject)Instantiate(wall, wallStartPoint, Quaternion.identity);
		newWall.transform.SetScaleX(newWall.transform.localScale.x * wallSize);
		RectTransform rt = (RectTransform)newWall.transform;
		newWall.transform.SetX(newWall.transform.position.x + rt.rect.width);
    }
	
}
