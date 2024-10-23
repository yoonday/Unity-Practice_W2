using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolQ2 : MonoBehaviour
{
    public GameObject prefab; // 풀에 저장할 오브젝트의 프리팹
    private List<GameObject> pool = new List<GameObject>(); // 오브젝트 담을 리스트
    public int poolSize = 300; // 오브젝트 생성 수량

    void Start()
    {
        // 미리 poolSize만큼 게임오브젝트를 생성한다.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        // 꺼져있는 게임오브젝트를 찾아 active한 상태로 변경하고 return 한다.
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
            
        }

        // 사용할 오브젝트 없으면 null
        return null;
    }

    public void Release(GameObject obj)
    {
        // 게임오브젝트를 deactive한다.
        obj.SetActive(false);
    }
}
