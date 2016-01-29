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
    public GameObject salida;

    public int rows;
    public int columns;

	// Use this for initialization
	void Awake () {
        string[,] tiles = CSVReader.SplitCsvGrid(mapa.text);
        CSVReader.DebugOutputGrid(tiles);

        int offset = 1;
        GameObject wp= null;

        Dictionary<string, Transform> wps = new Dictionary<string, Transform>();

        Dictionary<string, Vector2> enemies = new Dictionary<string, Vector2>();
        Regex identifyWypoint = new Regex("[0-9][0-9]");
        
        for(int k=0;k < rows;k++) {
            for (int l = 0; l < columns; l++)
            {
                string val = tiles[k, l];
                if (val.Equals("WW"))
                {
                    //Debug.Log("Instantiating wall at " + new Vector2(k, l));
                    GameObject.Instantiate(floor, new Vector3(k * offset, 0.1f, l * offset), Quaternion.LookRotation(-Vector3.up));
                    GameObject newWall = (GameObject)Instantiate(wall, new Vector3(k * offset, 1, l * offset), Quaternion.LookRotation(Vector3.right));                    
                } else if (val.Equals("PP")) {
                    GameObject.Instantiate(floor, new Vector3(k * offset, 0.1f, l * offset), Quaternion.LookRotation(-Vector3.up));
                    GameObject p = (GameObject)Instantiate(player, new Vector3(k * offset, 0.5f, l * offset), Quaternion.identity);
                    p.GetComponent<PlayerMovement>().m_t = GameObject.Find("Score").transform;
                } else if (val.Contains("E")) {
                    enemies.Add(val, new Vector2(k, l)); 
                } else if (val.Equals("CC")) {
                    GameObject.Instantiate(floor, new Vector3(k * offset, 0.1f, l * offset), Quaternion.LookRotation(-Vector3.up));
                    GameObject.Instantiate(pickup, new Vector3(k * offset, 0.5f, l * offset), Quaternion.identity);
                }
                else if (val.Equals("SS"))
                {
                    GameObject.Instantiate(salida, new Vector3(k * offset, 0.1f, l * offset), Quaternion.LookRotation(-Vector3.up));
                }
                else if (val.Equals("__"))
                {
                    GameObject.Instantiate(floor, new Vector3(k * offset, 0.1f, l * offset), Quaternion.LookRotation(-Vector3.up));
                }
                else if (Regex.IsMatch(val, "[0-9][0-9]"))
                {
                    GameObject.Instantiate(floor, new Vector3(k * offset, 0.1f, l * offset), Quaternion.LookRotation(-Vector3.up));
                    wp = (GameObject)Instantiate(waypoint, new Vector3(k * offset, 0.1f, l * offset), Quaternion.identity);
                    wps.Add(val, wp.transform);
                }
            }
        }
        Debug.Log(enemies.Count);
        Debug.Log(wps.Count);
        foreach (KeyValuePair<string, Vector2> newenemy in enemies) {

            GameObject.Instantiate(floor, new Vector3(newenemy.Value.x * offset, 0.1f, newenemy.Value.y * offset), Quaternion.LookRotation(-Vector3.up));
            GameObject enemyGO = (GameObject)Instantiate(enemy, new Vector3(newenemy.Value.x * offset, 0.5f, newenemy.Value.y * offset), Quaternion.identity);
            
            List<Transform> wpss = new List<Transform>();
            foreach (KeyValuePair<string, Transform> wayp in wps)
            {
                if (wayp.Key[0] == newenemy.Key[1])
                {
                    wpss.Add(wayp.Value);
                }
            }           
            enemyGO.GetComponent<EnemyStateMachine>().addWaypoints(wpss);
        }
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}
