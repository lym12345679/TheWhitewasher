
using MizukiTool.RecyclePool;
using UnityEngine;

public enum TestEnum
{
    Test1,
    Test2,
    Test3
}
public class RecyclePoolTest : MonoBehaviour
{
    void Awake()
    {
        RecyclePool.RegisterAllPrefab();
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RecyclePool.Request(TestEnum.Test1);
    }
}
