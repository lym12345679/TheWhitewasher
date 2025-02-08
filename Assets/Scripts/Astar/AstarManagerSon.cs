using MizukiTool.AStar;
using UnityEngine;

public class AstarManagerSon : AstarManager
{
    public static new AstarManagerSon Instance;
    //2.5D效果
    public static bool IsTPFDUsed
    {
        get
        {
            return StaticDatas.IsTPFDUsed;
        }
        set
        {
            StaticDatas.IsTPFDUsed = value;
        }
    }
    new void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 更新地图信息
    /// </summary>
    public override AstarMap UpdateMap()
    {
        //Debug.Log("UpdateMap");
        float cellSize = map.GetCellSize();
        Vector3 origin = map.GetOrigin() + new Vector3(cellSize / 2, cellSize / 2, 0);
        int width = map.GetMapWidth();
        int height = map.GetMapHeight();
        PointMod[,] mapData = new PointMod[height, width];
        GameObject[,] gameObjects = new GameObject[height, width];
        Component[,] mainCompoments = new Component[height, width];
        GameObject[] go_planes = GameObject.FindGameObjectsWithTag("Go_Plane");
        foreach (var go in go_planes)
        {
            string[] names = go.name.Split('_');
            int x = int.Parse(names[2]);
            int y = int.Parse(names[3]);
            SquareController squareController = go.GetComponent<SquareController>();
            mapData[y, x] = squareController.MPoint;
            gameObjects[y, x] = go;
            mainCompoments[y, x] = squareController;

        }
        /*for (int i = 0; i < height; i++)
        {
            //十字检测
            for (int j = 0; j < width; j++)
            {
                //从中心到右发射射线
                RaycastHit2D[] hit = Physics2D.RaycastAll(origin + new Vector3(j * cellSize, i * cellSize, 0), Vector2.right, cellSize / 2 - 0.01f, wallLayer);
                foreach (var h in hit)
                {
                    if (h.collider != null && h.collider.tag != "Player" && h.collider.gameObject.layer != LayerMask.NameToLayer("Prop"))
                    {
                        //Debug.Log("Hit:" + h.collider.transform.position + " (" + i + "," + j + ")" + h.collider.gameObject.name);
                        mapData[i, j] = CheckPointMod(h.collider);
                        gameObjects[i, j] = h.collider.gameObject;
                        mainCompoments[i, j] = h.collider.gameObject.GetComponent<SquareController>();
                        break;
                    }
                    else if (h.collider != null && h.collider.tag == "Player")
                    {
                        Debug.Log("Hit Player:" + h.collider.transform.position + " (" + i + "," + j + ")");
                    }
                    else
                    {
                        Debug.Log("Can't Hit:" + h.collider.transform.position + " (" + i + "," + j + ")");
                    }
                }
            }
        }*/
        /*foreach (var go in gameObjects)
        {
            if (go != null)
            {
                go.GetComponent<SpriteRenderer>().color = SOManager.colorSO.GetColor(PointMod.Red);
            }
        }*/
        map.SetMapData(mapData);
        map.SetGameObjects(gameObjects);
        map.SetMainCompoment(mainCompoments);
        CheckAllPointNeighbour();
        return map;
    }
    public override PointMod CheckPointMod(Collider2D collider2D)
    {
        SquareController squareController;
        if (collider2D.TryGetComponent(out squareController))
        {
            return squareController.MPoint;
        }
        return PointMod.None;
    }
    public void CheckAllPointNeighbour()
    {
        Vector3 origin = map.GetOrigin() + new Vector3(cellSize / 2, cellSize / 2, 0);
        for (int i = 0; i < map.GetMapHeight(); i++)
        {
            for (int j = 0; j < map.GetMapWidth(); j++)
            {
                /*/if (map[i, j].GetMainCompoment<SquareController>() == null || map[i, j].GameObject == null)
                {
                    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Go_Plane");
                    string name = "Go_Plane(" + i + "," + j + ")";
                    foreach (var go in gameObjects)
                    {
                        if (go.name == name)
                        {
                            SquareController squareController = go.GetComponent<SquareController>();
                            //Debug.Log("Hit:" + h.collider.transform.position + " (" + i + "," + j + ")" + h.collider.gameObject.name);
                            map[i, j].Mod = squareController.MPoint;
                            map[i, j].GameObject = go;
                            map[i, j].MainCompoment = squareController;
                            squareController.CheckNeighbourPoint();
                            Debug.Log("Successfully Refind Point:" + go.transform.position + " (" + i + "," + j + ")" + go.name);
                            break;
                        }
                    }
                }
                else*/
                if (map[i, j].GameObject != null)
                {
                    map[i, j].GetMainCompoment<SquareController>().CheckNeighbourPoint();
                }
                else
                {
                    Debug.LogWarning("注意:" + j + "," + i + "没有找到方块!");
                }
            }
        }
    }
    public void SetTPFD()
    {
        //Debug.Log("SetTPFD");
        IsTPFDUsed = !IsTPFDUsed;
        //CheckAllPointNeighbour();
        Vector3 origin = map.GetOrigin() + new Vector3(cellSize / 2, cellSize / 2, 0);
        for (int i = 0; i < map.GetMapHeight(); i++)
        {
            for (int j = 0; j < map.GetMapWidth(); j++)
            {
                if (map[i, j].GameObject != null)
                {
                    map[i, j].GetMainCompoment<SquareController>().SetTPFDMod(IsTPFDUsed);
                }
                else
                {
                    Debug.LogWarning("注意:" + j + "," + i + "没有找到方块!");
                }
            }
        }
    }
    public void RefindPoint(Point point)
    {
        Vector3 origin = map.GetOrigin() + new Vector3(cellSize / 2, cellSize / 2, 0);
        RaycastHit2D[] hit = Physics2D.RaycastAll(origin + new Vector3(point.Y * cellSize, point.X * cellSize, 0), Vector2.right, cellSize / 2 - 0.01f, wallLayer);
        foreach (var h in hit)
        {
            if (h.collider != null && h.collider.tag != "Player" && h.collider.gameObject.layer != LayerMask.NameToLayer("Prop"))
            {
                Debug.Log("Successfully Refind Point:" + h.collider.transform.position + " (" + point.X + "," + point.Y + ")" + h.collider.gameObject.name);
                point.Mod = CheckPointMod(h.collider);
                point.GameObject = h.collider.gameObject;
                point.MainCompoment = h.collider.gameObject.GetComponent<SquareController>();
                break;
            }
            else
            {
                Debug.Log("Can't Hit:" + h.collider.transform.position + " (" + point.X + "," + point.Y + ")");
            }
        }
    }
}
