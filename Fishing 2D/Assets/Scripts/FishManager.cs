using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;
    private List<Fish> fishInWaterList;
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    List<GameObject> fishPrefabList;
    [SerializeField]
    List<int> numeroFishInWaterDependOnLevelList;
    List<Fish> fishToAddList, fishToDestroyList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        fishInWaterList = new List<Fish>();
    }
    // Start is called before the first frame update
    void Start()
    {
        fishToAddList = new List<Fish>();
        fishToDestroyList = new List<Fish>();
        fishPrefabList = fishPrefabList.OrderBy(o => o.GetComponent<Fish>().levelOfFish).ToList();
        CheckFish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckFish()
    {
        for (int level = 0; level < maxLevel; level++)
        {
            fishInWaterList.AddRange(fishToAddList);
            foreach (Fish fish in fishToDestroyList)
            {
                fishInWaterList.Remove(fish);
            }
            fishToAddList.Clear();
            fishToDestroyList.Clear();
            CheckIfEnoughtFish(level);
        }
        
    }

    void CheckIfEnoughtFish(int level)
    {
        int numberFish = 0;
        foreach(Fish fish in fishInWaterList)
        {
            if(fish.levelOfFish == level)
            {
                numberFish += 1;
            }
        }
        Debug.Log(numberFish +" ;"+ numeroFishInWaterDependOnLevelList[level]);
        for (int i= numberFish; i< numeroFishInWaterDependOnLevelList[level]; i++)
        {
            Debug.Log("StartSpawn");
            SpawnRandomlyFish(level);
        }
        
    }

    void SpawnRandomlyFish(int level)
    {
        Debug.Log("Level" + level);
        List<Fish> fishCanSpawnList = new List<Fish>();
        foreach(GameObject fish in fishPrefabList)
        {
            if(fish.GetComponent<Fish>().levelOfFish == level)
            {
                fishCanSpawnList.Add(fish.GetComponent<Fish>());
            }
           
        }
        fishCanSpawnList = fishCanSpawnList.OrderBy(o => o.GetComponent<Fish>().chanceToSpawn).ToList();
        int amount = 0;
        foreach(Fish fish in fishCanSpawnList)
        {
            Debug.Log(fish.chanceToSpawn);
            amount += fish.chanceToSpawn;
        }
        if(amount != 100)
        {
            Debug.Log("La probabilité de spawn de tous les poissons combinés du niveau " + level +
                " n'est pas égale à 100");
        }
        else
        {
            int randomChance = Random.Range(0, 100);
            int seuilProb=0;
            bool fishFind = false;
            for(int i=0;i<fishCanSpawnList.Count && !fishFind; i++)
            {
                if(seuilProb<randomChance && randomChance< seuilProb+fishCanSpawnList[i].chanceToSpawn)
                {
                    float randomXSpawn = Random.Range(fishCanSpawnList[i].spawnMin.x, fishCanSpawnList[i].spawnMax.x);
                    float randomYSpawn = Random.Range(fishCanSpawnList[i].spawnMin.y, fishCanSpawnList[i].spawnMax.y);
                    GameObject fishSpawned = Instantiate(fishCanSpawnList[i].gameObject, new Vector2(randomXSpawn, randomYSpawn), Quaternion.identity);
                    fishToAddList.Add(fishSpawned.GetComponent<Fish>());
                    fishFind = true;
                }
            }
        }
        
    }

    public void DestroyFish(Fish fish)
    {
        fishToDestroyList.Add(fish);
        Destroy(fish);
        CheckFish();
    }


}
