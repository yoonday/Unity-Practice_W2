using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int Size;  // 몇 개나 생성할지

    }

    public List<Pool> pools = new List<Pool>(); // 리스트는 기본적으로 시리얼라이즈가 됨(딕셔너리는 X)
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        //PoolDictionary 초기화
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++) // 풀 사이즈만큼
            {
                GameObject obj = Instantiate(pool.prefab, transform); // 풀에 있는 프리팹 생성 
                obj.SetActive(false); // 생성 후 비활성화 (만들어 놓기만 함)
                                      // → 나중에 꺼낸 후 다시 활성화해야 게임 내에서 보이거나 상호작용 가능
                queue.Enqueue(obj); // 큐에 생성한 프리팹 인스턴스 저장
            }

            PoolDictionary.Add(pool.tag, queue); // 하나의 게임 오브젝트에 대한 풀을 딕셔너리에 넣어 줌
        }
    }

    public GameObject SpawnFromPool(string tag) // Instantiate 대신 사용
    {
        if (!PoolDictionary.ContainsKey(tag)) // 풀 딕셔너리가 딕셔너리에 없다면(존재하지 않는다)
        {
            return null; 
        }

        GameObject obj = PoolDictionary[tag].Dequeue(); // 가장 앞에 있는 값을 가져옴
        PoolDictionary[tag].Enqueue(obj); // 다시 넣는 과정 - 풀링 시스템에서 재사용하기 위한 준비과정이다


        obj.SetActive(true); // 해당 오브젝트를 활성화하여 게임에 나타나게한다.
        return obj;
    }


}