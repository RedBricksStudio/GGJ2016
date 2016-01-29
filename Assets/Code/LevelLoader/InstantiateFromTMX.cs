using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class InstantiateFromTMX : MonoBehaviour {

    public TextAsset mapa;

    public GameObject wall;
    public GameObject player;
    public GameObject enemy;
    public GameObject pickup;
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

        float offset = 0.1f;
        GameObject wp= null;

        Dictionary<string, Transform> wps = new Dictionary<string, Transform>();

        Dictionary<string, Vector2> enemies = new Dictionary<string, Vector2>();
        Regex identifyWypoint = new Regex("[0-9][0-9]");
        
        for(int k=0;k < columns-1;k++) {
            for (int l = 0; l < rows; l++)
            {
                string val = tiles[k, (rows - l - 1)];
				
				Debug.Log("Loading map... rows[" + (rows - l - 1) + "] colums [" + l +"] value [" + val + "]" );
                
				if (val.Equals("WW"))
                {
                    GameObject newWall = (GameObject)Instantiate(wall, new Vector3(k * offset, l * offset, 0), Quaternion.identity);                    
                } else if (val.Equals("PP")) {
                    GameObject.Instantiate(player, new Vector3(k * offset, l * offset, 0), Quaternion.identity);
                } else if (val.Contains("E")) {
                    enemies.Add(val, new Vector2(k, l)); 
                }               
				else if (val.Equals("DD")) {
					GameObject.Instantiate(death, new Vector3(k * offset, l * offset,0), Quaternion.identity);
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
}
