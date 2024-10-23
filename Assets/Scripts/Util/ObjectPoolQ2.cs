using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPoolQ2 : MonoBehaviour
{
    public class Pool
    {
        public string tag;
        public GameObject prefab; // 풀에 저장할 오브젝트의 프리팹
        public int poolSize = 300; // 오브젝트 생성 수량

    }
    private List<Pool> pool = new List<Pool>(); // 오브젝트 담을 리스트

    public Dictionary<string, Queue<GameObject>> Pools; // 딕셔너리로 오브젝트 풀 관리


    void Start()
    {
        //PoolDictionary 초기화
        Pools = new Dictionary<string, Queue<GameObject>>();

        foreach (var Pool in pool)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            // 미리 poolSize만큼 게임오브젝트를 생성한다.
            for (int i = 0; i < Pool.poolSize; i++)
            {
                GameObject obj = Instantiate(Pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            Pools.Add(Pool.tag, queue); // 다 만들면 딕셔너리에 넣기
        }
        
    }

    public GameObject Get(string tag)
    {
        // 꺼져있는 게임오브젝트를 찾아 active한 상태로 변경하고 return 한다.
        if (!Pools.ContainsKey(tag)) // 풀 딕셔너리가 딕셔너리에 없다면(존재하지 않는다)
        {
            return null;
        }


        GameObject obj = Pools[tag].Dequeue(); // 가장 앞에 있는 값 가져오기
        obj.SetActive(true);  // 활성화
        Pools[tag].Enqueue(obj); // 다시 넣기

        obj.SetActive(true); // 해당 오브젝트를 활성화하여 게임에 나타나게한다.
        return obj;
    }

    public void Release(GameObject obj)
    {
        // 게임오브젝트를 deactive한다.
        obj.SetActive(false);
    }
}
