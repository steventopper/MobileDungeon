using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    //public int numOfRooms=10;
    //public List<GameObject>[] enemies;
    public Dictionary<string,Dictionary<string,Vector4>> sceneEnemies = new Dictionary<string, Dictionary<string,Vector4>>();
    public Dictionary<string,Vector3> scenePlayer = new Dictionary<string, Vector3>();
    public Dictionary<string,int> sceneChests = new Dictionary<string, int>();
    public GameObject gameEngine;
    public int sceneCount=0;
    public List<string> scenes;
    // Start is called before the first frame update
    void Start()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            scenes.Add(sceneName);
            //List<GameObject> listOfEnemies= new List<GameObject>();
            sceneEnemies.Add(sceneName,new Dictionary<string, Vector4>());
            scenePlayer.Add(sceneName, new Vector3());
            //sceneChests.Add(sceneName,)
        }
    }
    public void updateScene(string sceneName)
    {
        //updates pos and hp of enemies
        List<GameObject> listOfEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        for (int i = 0; i < listOfEnemies.Count; i++)
        {
            Vector4 hpAndPos = new Vector4(listOfEnemies[i].transform.position.x,listOfEnemies[i].transform.position.y,listOfEnemies[i].transform.position.z,listOfEnemies[i].GetComponent<enemyHP>().getHP());
            if (!sceneEnemies[sceneName].ContainsKey(listOfEnemies[i].name))
            {
                sceneEnemies[sceneName].Add(listOfEnemies[i].name,hpAndPos);
            }
            else
            {
                sceneEnemies[sceneName][listOfEnemies[i].name] = hpAndPos;
            }
            
        }
        
        //updates player pos
        scenePlayer[sceneName] = GameObject.FindGameObjectWithTag("Player").transform.position;

        //updates chests
        List<GameObject> listOfChests = new List<GameObject>(GameObject.FindGameObjectsWithTag("Chest"));
        for (int i = 0; i < listOfChests.Count; i++)
        {
            if (sceneChests.ContainsKey(sceneName+listOfChests[i].name))
            {
                sceneChests[sceneName+listOfChests[i].name] = listOfChests[i].GetComponent<chestController>().opened;
            }
            else
            {
                sceneChests.Add(sceneName+listOfChests[i].name,listOfChests[i].GetComponent<chestController>().opened);

            }
        }
    }
    public void resetVals()
    {
        sceneEnemies = new Dictionary<string, Dictionary<string,Vector4>>();
        scenePlayer = new Dictionary<string, Vector3>();
        sceneChests = new Dictionary<string, int>();
        Start();
    }
    public void setScene(string sceneName)
    {
        List<GameObject> listOfEnemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        List<GameObject> listOfChests = new List<GameObject>(GameObject.FindGameObjectsWithTag("Chest"));


        for (int i = 0; i < listOfChests.Count; i++)
        {
            if (sceneChests.ContainsKey(sceneName+listOfChests[i].name))
            {
                listOfChests[i].GetComponent<chestController>().opened = sceneChests[sceneName+listOfChests[i].name];
            }
            
        }

        if(scenePlayer.ContainsKey(sceneName)&&scenePlayer[sceneName] != new Vector3(0f,0f,0f))
            player.transform.position = scenePlayer[sceneName];

    
        for (int i = 0; i < listOfEnemies.Count; i++)
        {
            if(sceneEnemies[sceneName].ContainsKey(listOfEnemies[i].name))
            {
                if (sceneEnemies[sceneName][listOfEnemies[i].name].w<=0.0f)
                {
                    Object.Destroy(listOfEnemies[i]);
                }
                //Debug.Log(sceneEnemies[sceneName][listOfEnemies[i].name]);
                float x = sceneEnemies[sceneName][listOfEnemies[i].name].x;
                float y = sceneEnemies[sceneName][listOfEnemies[i].name].y;
                float z = sceneEnemies[sceneName][listOfEnemies[i].name].z;
                listOfEnemies[i].transform.position = new Vector3(x,y,z);
                listOfEnemies[i].GetComponent<enemyHP>().setHP(sceneEnemies[sceneName][listOfEnemies[i].name].w);
                //sceneEnemies[sceneName][listOfEnemies[i].name]
            }
            
        }
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
