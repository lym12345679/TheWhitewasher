using System.Collections.Generic;
using UnityEngine;
using static MizukiTool.AStar.AStarWrapper;
namespace MizukiTool.AStar
{
    public class AstarManager : MonoBehaviour
    {
        public static AstarManager Instance;
        public AstarMap map;

        public float cellSize = 1;
        public Transform StartTransform;
        public Transform EndTransform;
        private int mapHeight = 50;
        private int mapWidth = 50;
        public LayerMask wallLayer;

        public void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            TryLoadMap(out map);
            //Test();
        }


        public static void EnsureInstance()
        {
            if (Instance == null)
            {
                GameObject go = new GameObject("AstarManager");
                Instance = go.AddComponent<AstarManager>();
            }
        }
        #region 地图相关
        /// <summary>
        /// 尝试获取地图数据
        /// </summary>
        public bool TryLoadMap(out AstarMap map)
        {
            if (this.StartTransform != null && this.EndTransform != null)
            {
                int width = (int)(Mathf.Abs(StartTransform.position.x - EndTransform.position.x) / cellSize);
                int height = (int)(Mathf.Abs(StartTransform.position.y - EndTransform.position.y) / cellSize);
                map = new AstarMap(height, width, cellSize, StartTransform.position);
                map.InitMap();
                map = UpdateMap();
                return true;
            }
            else
            {
                map = new AstarMap(mapHeight, mapWidth, cellSize, Vector3.zero - new Vector3(mapWidth * cellSize / 2, mapHeight * cellSize / 2, 0));
                map.InitMap();
                map = UpdateMap();
                Debug.LogWarning("StartTransform or EndTransform is null, use default map data");
                return false;
            }
        }
        public void InitMap(int width, int height, float cellSize, Vector3 origin, PointMod[,] mapData)
        {
            map = new AstarMap(width, height, cellSize, origin, mapData);
        }

        /// <summary>
        /// 更新地图信息
        /// </summary>
        public virtual AstarMap UpdateMap()
        {
            float cellSize = map.GetCellSize();
            Vector3 origin = map.GetOrigin() + new Vector3(cellSize / 2, cellSize / 2, 0);
            int width = map.GetMapWidth();
            int height = map.GetMapHeight();
            PointMod[,] mapData = new PointMod[width, height];
            for (int i = 0; i < width; i++)
            {
                //十字检测
                for (int j = 0; j < height; j++)
                {
                    //从左到右发射射线
                    RaycastHit2D[] hit = Physics2D.RaycastAll(origin + new Vector3(i * cellSize - cellSize / 2, j * cellSize, 0), Vector2.right, cellSize, wallLayer);
                    foreach (var h in hit)
                    {
                        Debug.Log("hit:" + h.collider);
                        if (h.collider != null)
                        {
                            mapData[i, j] = CheckPointMod(h.collider);
                            break;
                        }
                    }
                    //从下到上发射射线
                    hit = Physics2D.RaycastAll(origin + new Vector3(i * cellSize, j * cellSize - cellSize / 2, 0), Vector2.up, cellSize, wallLayer);
                    foreach (var h in hit)
                    {
                        if (h.collider != null)
                        {
                            mapData[i, j] = CheckPointMod(h.collider);
                            break;
                        }
                    }
                }
            }
            map.SetMapData(mapData);
            return map;
        }
        #endregion
        #region 路径相关
        [Header("寻路相关")]
        public float PathFindingDistance = 20;
        public float PathFindingInterval = 0.5f;
        public bool UseSimplePath = false;
        //在一定距离外采用直线寻路
        //在一定距离内采用A*寻路
        public bool TryFindPath(Vector3 startPos, Vector3 endPos, out List<Point> path)
        {
            EnsureInstance();
            if (map == null)
            {
                TryLoadMap(out map);
            }
            if (Vector3.Distance(startPos, endPos) > PathFindingDistance)
            {
                path = new List<Point>
                {
                    map.GetPointOnMap(startPos),
                    map.GetPointOnMap(endPos)
                };
                return true;
            }
            List<Point> newPath;
            bool b = AStarWrapper.TryFindPath(map, startPos, endPos, out newPath);
            path = newPath;
            return b;
        }

        //下一个方向
        public Vector3 NextDirection(Vector3 currentPos, List<Point> path)
        {
            EnsureInstance();
            if (path == null || path.Count == 0)
            {
                return Vector3.zero;
            }
            Vector3 nextPoint = map.GetPositionOnMap(path[0]);
            //Debug.Log("nextPoint:" + nextPoint);
            return (nextPoint - currentPos).normalized;
        }

        //简化路径
        public List<Point> SimplizePath(List<Point> path, Vector3 selfPos, Vector3 targetPos)
        {
            EnsureInstance();
            if (path.Count < 3)
            {
                return path;
            }
            Debug.Log("Simplize Path");

            Vector3 startPos;
            Vector3 nextPos;
            Vector3 offset = new Vector3(map.GetCellSize() / 2, map.GetCellSize() / 2, 0);
            int i = -1;
            while (i < path.Count - 2)
            {
                if (i == -1)
                {
                    startPos = selfPos;
                }
                else
                {
                    startPos = map.GetPositionOnMap(path[i]) + offset;
                }
                nextPos = map.GetPositionOnMap(path[i + 1]) + offset;
                RaycastHit2D hit1 = Physics2D.Raycast(startPos, nextPos - startPos, Vector3.Distance(startPos, nextPos), wallLayer);
                nextPos = map.GetPositionOnMap(path[i + 2]) + offset;
                RaycastHit2D hit2 = Physics2D.Raycast(startPos, nextPos - startPos, Vector3.Distance(startPos, nextPos), wallLayer);
                if (hit1.collider == null && hit2.collider == null)
                {
                    Debug.Log("Remove Point:" + path[i + 1]);
                    path.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }
            return path;
        }
        //将List<Point>型路径转换为List<Vector3>型
        public List<Vector3> ConvertPathToVector3(List<Point> path, Vector3 selfPos, Vector3 targetPos)
        {
            List<Vector3> newPath = new List<Vector3>();
            foreach (var p in path)
            {
                newPath.Add(map.GetPositionOnMap(p));
            }
            newPath.Insert(0, selfPos);
            newPath.Add(targetPos);
            return newPath;
        }
        //将List<Vector3>型路径转换为List<Point>型
        public List<Point> ConvertPathToPoint(List<Vector3> path)
        {
            List<Point> newPath = new List<Point>();
            foreach (var p in path)
            {
                newPath.Add(map.GetPointOnMap(p));
            }
            return newPath;
        }
        #endregion
        #region 向量场寻路相关
        [Header("向量场寻路相关")]
        public int VectorPathRange = 20;
        public float RefreshInterval = 1f;
        public void CreatAstarVector(Vector3 startPos)
        {
            EnsureInstance();
            if (map == null)
            {
                TryLoadMap(out map);
            }
            map = AStarWrapper.CreatAstarVector(map, startPos, VectorPathRange);
        }
        public Vector3 GetNextDirection(Vector3 startPos)
        {
            EnsureInstance();
            if (map == null)
            {
                TryLoadMap(out map);
            }
            Point point = map.GetPointOnMap(startPos);
            //Debug.Log("NextDirection: " + map[point.X, point.Y].Direction);
            return map[point.X, point.Y].Direction;
        }
        #endregion
        #region 节点相关
        /// <summary>
        /// 更新CloseList中所有的AstarPoint的Mod
        /// </summary>
        /// <param name="astarMap">用到的地图</param>
        /// <param name="startPos">起始点</param>
        /// <param name="pointMods">可通行的节点Mod</param>
        /// <param name="func">改变Point状态的函数</param>
        /// <returns></returns>
        public AstarMap UpdateAllAstarPonitInCloseList(AstarMap astarMap, Vector3 startPos, PointMod[] pointMods, PointParameter func)
            => AStarWrapper.UpdateAllAstarPonitInCloseList(astarMap, startPos, pointMods, func);
        public List<Point> GetNeighbourPoints(Vector3 position)
        {
            Point point = map.GetPointOnMap(position);
            return AStarWrapper.GetNeighbourPoints(map, point);
        }

        public List<Point> GetNeighbourPoints(Point point)
            => AStarWrapper.GetNeighbourPoints(map, point);

        public Point GetPointOnMap(Vector3 position)
            => map.GetPointOnMap(position);
        #endregion
        #region 标注地图
        [Header("显示地图标注")]
        public bool ShowMapLableGizmos = true;
        [Header("显示路径标注")]
        public bool ShowPathLableGizmos = true;

        public Transform t;
        public List<Point> path;

        void OnDrawGizmos()
        {
            if (map != null && ShowMapLableGizmos)
            {
                DrawMap(map);
            }
            if (path != null && ShowPathLableGizmos)
            {
                DrawPath(path, t);
            }
        }

        private void DrawMap(AstarMap map)
        {
            float cellSize = map.GetCellSize();
            for (int i = 0; i < map.GetMapHeight(); i++)
            {
                for (int j = 0; j < map.GetMapWidth(); j++)
                {
                    if (map[i, j].Mod != PointMod.None)
                    {
                        Gizmos.color = Color.red;
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                    }
                    Vector3 pos = map.GetOrigin() + new Vector3(j * cellSize, i * cellSize, 0);
                    Gizmos.DrawLine(pos + new Vector3(cellSize / 2, cellSize, 0), pos + new Vector3(cellSize / 2, 0, 0));
                    Gizmos.DrawLine(pos + new Vector3(cellSize, cellSize / 2, 0), pos + new Vector3(0, cellSize / 2, 0));
                }
            }
        }

        private void DrawPath(List<Point> path, Transform selfTransform = null)
        {
            if (path == null || path.Count == 0)
            {
                return;
            }
            if (selfTransform != null)
            {
                Gizmos.color = Color.blue;
                Vector3 startPos = selfTransform.position;
                Vector3 endPos = map.GetPositionOnMap(path[0]);
                Gizmos.DrawLine(startPos, endPos);
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.color = Color.blue;
                Vector3 startPos = map.GetPositionOnMap(path[i]);
                Vector3 endPos = map.GetPositionOnMap(path[i + 1]);
                Gizmos.DrawLine(startPos, endPos);
            }
        }
        #endregion

        #region Test        
        void Test()
        {

        }
        #endregion
        #region Others
        public virtual PointMod CheckPointMod(Collider2D collider2D)
        {
            return (PointMod)1;
        }
        #endregion
    }

}
