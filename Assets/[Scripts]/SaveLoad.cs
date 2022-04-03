using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
class SaveData
{
    public float[] playerPosition;
    public float[] playerRotation;

    public List<float> ratTransformX;
    public List<float> ratTransformY;
    public List<float> ratTransformZ;

    public List<float> roombaTransformX;
    public List<float> roombaTransformY;
    public List<float> roombaTransformZ;


   
    public int lives;
    public int score;
    public int health;
    public string scene;

    [System.NonSerialized]
    public GameObject ratGo;
    [System.NonSerialized]
    public GameObject roombaGo;

    //public int score;
    //public int enemyCount;
    //public Vector3[] enemyPositions;  //later
    //public GameObject[] enemies;

    public SaveData()
    {
        playerPosition = new float[3]; // create empty container
        playerRotation = new float[3]; // create empty container

        ratTransformX = new List<float>();
        ratTransformY = new List<float>();
        ratTransformZ = new List<float>();



        roombaTransformX = new List<float>();
        roombaTransformY = new List<float>();
        roombaTransformZ = new List<float>();
    }
    
}

public class SaveLoad : MonoBehaviour
{
    public Transform player;
    GameObject[] allObjects;
    public GameObject scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager");
        player = GameObject.Find("CatGirlChar").transform;
    }

    // Serialize the player data
    private void SaveGame()
    {
        int ratCount = 0;
        int roombaCount = 0;

        allObjects = GameObject.FindObjectsOfType<GameObject>();
        PlayerPrefs.SetString("savedScene", SceneManager.GetActiveScene().name);





        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();


        foreach (GameObject item in allObjects)
        {
            if (item.gameObject.tag == "Rat")
            {
                ratCount++;
                data.ratTransformX.Add(item.transform.position.x);
                data.ratTransformY.Add(item.transform.position.y);
                data.ratTransformZ.Add(item.transform.position.z);
            }

            if (item.gameObject.tag == "Roomba")
            {
                roombaCount++;
                data.roombaTransformX.Add(item.transform.position.x);
                data.roombaTransformY.Add(item.transform.position.y);
                data.roombaTransformZ.Add(item.transform.position.z);
            }


        }

        Debug.Log("RatCount: " + ratCount);
        Debug.Log("RoombaCount: " + roombaCount);




        data.playerPosition[0] = player.position.x;
        data.playerPosition[1] = player.position.y;
        data.playerPosition[2] = player.position.z;

        data.playerRotation[0] = player.localEulerAngles.x;
        data.playerRotation[1] = player.localEulerAngles.y;
        data.playerRotation[2] = player.localEulerAngles.z;


        data.lives = player.gameObject.GetComponent<ThirdPersonMovement>().lives;
        data.health = player.gameObject.GetComponent<ThirdPersonMovement>().health;
        data.score = scoreManager.gameObject.GetComponent<ScoreManager>().score;

        


        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Game data saved!");
    }

    // Deserialize the player data
    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();


            foreach (GameObject item in allObjects) // remove all enemies then reinstantiate at saved positions
            {
                if (item.gameObject.tag == "Rat")
                {
                    data.ratGo = item;
                    Destroy(item.gameObject);
                }

                if (item.gameObject.tag == "Roomba")
                {
                    data.roombaGo = item;
                    Destroy(item.gameObject);
                }
            }

            for (int i = 0; i < data.ratTransformX.Count; i++)
            {
                Instantiate(data.ratGo, new Vector3(data.ratTransformX[i], data.ratTransformY[i], data.ratTransformZ[i]), Quaternion.identity);
            }

            for (int i = 0; i < data.roombaTransformX.Count; i++)
            {
                Instantiate(data.roombaGo, new Vector3(data.roombaTransformX[i], data.roombaTransformY[i], data.roombaTransformZ[i]), Quaternion.identity);
            }

            var x = data.playerPosition[0];
            var y = data.playerPosition[1];
            var z = data.playerPosition[2];

            var RotX = data.playerRotation[0];
            var RotY = data.playerRotation[1];
            var RotZ = data.playerRotation[2];

            var savedLives = data.lives;
            var savedHealth = data.health;
            var savedScore = data.score;

            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.rotation = Quaternion.Euler(RotX, RotY, RotZ);
            player.gameObject.GetComponent<CharacterController>().enabled = true;

            player.gameObject.GetComponent<ThirdPersonMovement>().lives = savedLives;
            player.gameObject.GetComponent<ThirdPersonMovement>().health = savedHealth;
            scoreManager.gameObject.GetComponent<ScoreManager>().score = savedScore;

            Debug.Log("Game data loaded!");
            print(savedLives);
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
        {
            Debug.LogError("No save data to delete.");
        }
    }

    public void OnSaveButton_Pressed()
    {
        SaveGame();
    }

    public void OnLoadButton_Pressed()
    {
        LoadGame();
    }
}
