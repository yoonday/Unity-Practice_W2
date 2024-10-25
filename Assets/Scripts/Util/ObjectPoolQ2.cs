using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPoolQ2 : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab; // 풀에 저장할 오브젝트의 프리팹
        public int poolSize = 300; // 오브젝트 생성 수량

    }
    
    private List<Pool> poolList = new List<Pool>(); // 몬스터, 화살 풀을 담을 리스트
    public Dictionary<string, List<GameObject>> Pools; // 딕셔너리로 오브젝트 풀 관리


    void Start()
    {
        //PoolDictionary 초기화
        Pools = new Dictionary<string, List<GameObject>>();

        foreach (var pool in poolList)
        {
            List<GameObject> list = new List<GameObject>();
            // 미리 poolSize만큼 게임오브젝트를 생성한다.
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                list.Add(obj);
            }

            Pools.Add(pool.tag, list); // 다 만들면 딕셔너리에 넣기
        }
        
    }

    public GameObject Get(string tag)
    {
        // 꺼져있는 게임오브젝트를 찾아 active한 상태로 변경하고 return 한다.
        if (!Pools.ContainsKey(tag)) 
        {
            return null;
        }

        foreach (var obj in Pools[tag])
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
     
        return null;
    }

    public void Release(GameObject obj)
    {
        // 게임오브젝트를 deactive한다.
        obj.SetActive(false);
    }
}
