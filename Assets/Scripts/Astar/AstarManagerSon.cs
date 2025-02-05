using MizukiTool.AStar;
using UnityEngine;

public class AstarManagerSon : AstarManager
{
    public static new AstarManagerSon Instance;
    //2.5D效果
    public static bool IsTPFDUsed = false;
    new void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 更新地图信息
    /// </summary>
    public override AstarMap UpdateMap()
    {
        float cellSize = map.GetCellSize();
        Vector3 origin = map.GetOrigin() + new Vector3(cellSize / 2, cellSize / 2, 0);
        int width = map.GetMapWidth();
        int height = map.GetMapHeight();
        PointMod[,] mapData = new PointMod[height, width];
        GameObject[,] gameObjects = new GameObject[height, width];
        Component[,] mainCompoments = new Component[height, width];
        for (int i = 0; i < height; i++)
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
                        //Debug.Log("Hit:" + h.collider.transform.position + " (" + i + "," + j + ")");
                        mapData[i, j] = CheckPointMod(h.collider);
                        gameObjects[i, j] = h.collider.gameObject;
                        mainCompoments[i, j] = h.collider.gameObject.GetComponent<SquareController>();
                        break;
                    }
                }
            }
        }
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
        for (int i = 0; i < map.GetMapHeight(); i++)
        {
            for (int j = 0; j < map.GetMapWidth(); j++)
            {
                map[i, j].GetMainCompoment<SquareController>().CheckNeighbourPoint();
            }
        }
    }
    public void SetTPFD()
    {
        IsTPFDUsed = true;
        CheckAllPointNeighbour();
    }
}
