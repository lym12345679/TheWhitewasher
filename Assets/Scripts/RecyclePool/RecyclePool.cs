using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MizukiTool.RecyclePool
{

    public class RecyclePool
    {
        internal static Dictionary<string, RecycleContext> contextDic = new Dictionary<string, RecycleContext>();
        internal static Dictionary<string, Stack<RecyclableObject>> componentDic = new Dictionary<string, Stack<RecyclableObject>>();

        //注册所有回收物
        public static void RegisterAllPrefab()
        {
            RigisterOnePrefab(TestEnum.Test1, Resources.Load<GameObject>("Text/Text123"));
        }
        //注册一个回收物
        public static void RigisterOnePrefab<T>(T id, GameObject prefab) where T : Enum
        {
            Debug.Log("RigisterOnePrefab:" + id + ":" + prefab.name);
            EnumIdentifier identifier = new EnumIdentifier();
            identifier.SetEnum(id);
            RecycleContext context = new RecycleContext();
            context.Prefab = prefab;
            context.id = identifier.GetID();
            contextDic.Add(context.id, context);
            componentDic.Add(context.id, new Stack<RecyclableObject>());
        }
        //检查是否存在该回收物
        public static bool CheckIdentifer<T>(T id) where T : Enum
        {
            EnumIdentifier identifier = new EnumIdentifier();
            identifier.SetEnum(id);
            return contextDic.ContainsKey(identifier.GetID());
        }
        //创建一个回收物
        public static GameObject Create<T>(T id) where T : Enum
        {
            EnumIdentifier identifier = new EnumIdentifier();
            identifier.SetEnum<T>(id);
            RecycleContext context = contextDic[identifier.GetID()];
            GameObject go = GameObject.Instantiate(context.Prefab);
            RecyclableObject controller;
            if (go.TryGetComponent<RecyclableObject>(out controller))
            {
                controller.id = identifier.GetID();
            }
            else
            {
                controller = go.AddComponent<RecyclableObject>();
                controller.id = identifier.GetID();
            }
            return go;
        }

        //请求一个回收物
        public static void Request<T>(T id, Action<RecycleCollection> hander = null, Transform parent = null) where T : Enum
        {
            Debug.Log("Request");
            GameObject target;
            RecycleCollection collection = new RecycleCollection();
            RecyclableObject controller;
            if (CheckIdentifer(id))
            {
                EnumIdentifier identifier = new EnumIdentifier();
                identifier.SetEnum(id);
                if (componentDic.TryGetValue(identifier.GetID(), out Stack<RecyclableObject> stack))
                {
                    Debug.Log("Recycle");
                    if (stack.Count == 0)
                    {
                        target = Create(id);
                        collection.RecyclingController = target.GetComponent<RecyclableObject>();
                    }
                    else
                    {
                        controller = stack.Pop();
                        target = controller.gameObject;
                        target.gameObject.SetActive(true);
                        controller.OnReset.Invoke();
                        collection.RecyclingController = controller;
                    }
                    collection.GameObject = target;
                    collection.Transform = target.transform;
                    target.transform.SetParent(parent);
                    if (hander != null)
                    {
                        hander.Invoke(collection);
                    }
                }

            }
        }
        //回收一个回收物
        public static void CollectRecycleObject(GameObject go, RecyclableObject controller)
        {
            EnsureSceneRecycleGuardExist();
            go.SetActive(false);
            componentDic[controller.id].Push(controller);
            go.transform.SetParent(SceneRecycleGuard.Instance.transform);
        }

        #region 确认是否存在        
        public static void EnsureContextExist()
        {
            if (contextDic.Count == 0)
            {
                RegisterAllPrefab();
            }
        }
        public static void EnsureSceneRecycleGuardExist()
        {
            if (SceneRecycleGuard.Instance == null)
            {
                GameObject go = new GameObject("SceneRecycleGuard");
                go.AddComponent<SceneRecycleGuard>();
            }
        }
        #endregion
    }

}