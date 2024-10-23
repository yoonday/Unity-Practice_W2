using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPoolQ2 : MonoBehaviour
{
    public class Pool
    {
        public string tag;
        public GameObject prefab; // Ǯ�� ������ ������Ʈ�� ������
        public int poolSize = 300; // ������Ʈ ���� ����

    }
    private List<Pool> pool = new List<Pool>(); // ������Ʈ ���� ����Ʈ

    public Dictionary<string, Queue<GameObject>> Pools; // ��ųʸ��� ������Ʈ Ǯ ����


    void Start()
    {
        //PoolDictionary �ʱ�ȭ
        Pools = new Dictionary<string, Queue<GameObject>>();

        foreach (var Pool in pool)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            // �̸� poolSize��ŭ ���ӿ�����Ʈ�� �����Ѵ�.
            for (int i = 0; i < Pool.poolSize; i++)
            {
                GameObject obj = Instantiate(Pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            Pools.Add(Pool.tag, queue); // �� ����� ��ųʸ��� �ֱ�
        }
        
    }

    public GameObject Get(string tag)
    {
        // �����ִ� ���ӿ�����Ʈ�� ã�� active�� ���·� �����ϰ� return �Ѵ�.
        if (!Pools.ContainsKey(tag)) // Ǯ ��ųʸ��� ��ųʸ��� ���ٸ�(�������� �ʴ´�)
        {
            return null;
        }


        GameObject obj = Pools[tag].Dequeue(); // ���� �տ� �ִ� �� ��������
        obj.SetActive(true);  // Ȱ��ȭ
        Pools[tag].Enqueue(obj); // �ٽ� �ֱ�

        obj.SetActive(true); // �ش� ������Ʈ�� Ȱ��ȭ�Ͽ� ���ӿ� ��Ÿ�����Ѵ�.
        return obj;
    }

    public void Release(GameObject obj)
    {
        // ���ӿ�����Ʈ�� deactive�Ѵ�.
        obj.SetActive(false);
    }
}
