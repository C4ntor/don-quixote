using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class terrainGenerator : MonoBehaviour
{

    [SerializeField] GameObject cell1;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cell2;
    [SerializeField] private int terrainSizeX = 20;
    [SerializeField] private int terrainSizeZ = 20;
    private float noiseHeight = 1.5f;
    private Vector3 playerStartPos;
    private Hashtable blockContainer = new Hashtable();

   

    private float generatePerlinNoise(int x, int z) {
        //generates perlin noise that will be applied for the height of terrain cells
        float xNoise = (x + this.transform.position.x)/4;
        float zNoise = (z + this.transform.position.y)/4;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }

    private int xPlayer
    {
        get { return (int)(Mathf.Floor(player.transform.position.x)) ; }
    }
    private int zPlayer
    {
        get { return (int)(Mathf.Floor(player.transform.position.z)); }
    }

    private int xPlayerMov
    {
        get { return (int)(player.transform.position.x - playerStartPos.x); }
    }
    private int zPlayerMov
    {
        get { return (int)(player.transform.position.z - playerStartPos.z); }
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        //instead of starting from zero and generate the world, we start by generating the world around the player
        for (int x=-terrainSizeX; x < terrainSizeX; x++) { 
            for (int z=-terrainSizeZ; z < terrainSizeZ; z++)
            {
                float heightCell = generatePerlinNoise(x + xPlayer, z +zPlayer) * noiseHeight;
                Vector3 pos = new Vector3(x+xPlayer, heightCell, z+zPlayer); 
                GameObject block = heightCell>=1.05 ? Instantiate(cell1, pos, Quaternion.identity) : Instantiate(cell2, pos, Quaternion.identity);
                blockContainer.Add(pos, block);
                block.transform.SetParent(this.transform);


            }
        }
    }

    void updateTerrain() {

       
        
            for (int x = -terrainSizeX; x < terrainSizeX; x++)
            {
                for (int z = -terrainSizeZ; z < terrainSizeZ; z++)
                {
                    float heightCell = generatePerlinNoise(x + xPlayer, z + zPlayer) * noiseHeight;
                    Vector3 pos = new Vector3(x + xPlayer, heightCell, z + zPlayer);

                    if (!blockContainer.ContainsKey(pos))
                    {
                        //spawn objects only if they don't already exist
                        GameObject block = heightCell >= 1.05 ? Instantiate(cell1, pos, Quaternion.identity) : Instantiate(cell2, pos, Quaternion.identity);
                        blockContainer.Add(pos, block);
                        block.transform.SetParent(this.transform);
                    }




                }
            }


        


    }

    void Update()
    {
        //check if player has moved further, to generate terrain
        if (Mathf.Abs(xPlayerMov) >= 5 || Mathf.Abs(zPlayerMov) >= 5) updateTerrain();
    }

}



