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
        public GameObject prefab; // Ǯ�� ������ ������Ʈ�� ������
        public int poolSize = 300; // ������Ʈ ���� ����

    }
    
    private List<Pool> poolList = new List<Pool>(); // ����, ȭ�� Ǯ�� ���� ����Ʈ
    public Dictionary<string, List<GameObject>> Pools; // ��ųʸ��� ������Ʈ Ǯ ����


    void Start()
    {
        //PoolDictionary �ʱ�ȭ
        Pools = new Dictionary<string, List<GameObject>>();

        foreach (var pool in poolList)
        {
            List<GameObject> list = new List<GameObject>();
            // �̸� poolSize��ŭ ���ӿ�����Ʈ�� �����Ѵ�.
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                list.Add(obj);
            }

            Pools.Add(pool.tag, list); // �� ����� ��ųʸ��� �ֱ�
        }
        
    }

    public GameObject Get(string tag)
    {
        // �����ִ� ���ӿ�����Ʈ�� ã�� active�� ���·� �����ϰ� return �Ѵ�.
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
        // ���ӿ�����Ʈ�� deactive�Ѵ�.
        obj.SetActive(false);
    }
}
