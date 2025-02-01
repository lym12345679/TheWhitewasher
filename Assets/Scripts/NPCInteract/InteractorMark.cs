using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MizukiTool.Interact
{
    /// <summary>
    /// 用于创建和显示交互图标，返回交互目标
    /// </summary>
    public class InteractorMark : MonoBehaviour
    {
        [HideInInspector]
        public GameObject icon;
        /// <summary>
        /// 一般指玩家,找到相关Layer的物体则显示交互图标
        /// </summary>
        public LayerMask PlayerLayer;
        public float InteractableRange = 1.5f;
        [HideInInspector]
        public bool IsFindTarget = false;
        public bool ShowInteractableRange = false;
        [HideInInspector]
        public string id;
        private float UpdateTime = 0.1f;
        private float currentTime;

        void Awake()
        {
            id = Guid.NewGuid().ToString();
        }
        // Start is called before the first frame update
        void Start()
        {
            CreateIcon();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (currentTime < UpdateTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                UpdateTarget();
            }
        }

        private void Update()
        {
            if (KeyEnum.Interact.IsKeyUp() && InteractorManager.InteractableTarget == this && !InteractorManager.Interacting)
            {
                InteractorManager.Interacting = true;
            }
        }

        public void CreateIcon()
        {
            GameObject iconPre = Resources.Load<GameObject>(InteractorManager.IconPrePath);
            icon = Instantiate(iconPre, this.transform);
            icon.GetComponent<InteractableIcon>().Initialize();
            icon.SetActive(false);
        }

        public void UpdateTarget()
        {
            float distance = Vector2.Distance(this.transform.position, PlayerController.Instance.transform.position);
            if (distance < InteractableRange)
            {
                if (!IsFindTarget || !InteractorManager.InteractableTarget)
                {
                    InteractorManager.UpdateInteractableTarget(this);
                    icon.SetActive(true);
                    IsFindTarget = true;
                }
            }
            else
            {
                InteractorManager.RemoveInteractableTarget(this);
                IsFindTarget = false;
                icon.SetActive(false);
            }

        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!ShowInteractableRange) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, InteractableRange);
#endif
        }
    }
}
