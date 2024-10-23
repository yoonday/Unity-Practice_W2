using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolQ2 : MonoBehaviour
{
    public GameObject prefab; // Ǯ�� ������ ������Ʈ�� ������
    private List<GameObject> pool = new List<GameObject>(); // ������Ʈ ���� ����Ʈ
    public int poolSize = 300; // ������Ʈ ���� ����

    void Start()
    {
        // �̸� poolSize��ŭ ���ӿ�����Ʈ�� �����Ѵ�.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        // �����ִ� ���ӿ�����Ʈ�� ã�� active�� ���·� �����ϰ� return �Ѵ�.
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
            
        }

        // ����� ������Ʈ ������ null
        return null;
    }

    public void Release(GameObject obj)
    {
        // ���ӿ�����Ʈ�� deactive�Ѵ�.
        obj.SetActive(false);
    }
}
