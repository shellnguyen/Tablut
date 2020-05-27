using System.Collections;
using UnityEngine;

public sealed class Utilities
{
    //Singleton Init
    private static readonly Utilities instance = new Utilities();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static Utilities()
    {
    }

    private Utilities()
    {
    }

    public static Utilities Instance
    {
        get
        {
            return instance;
        }
    }
    //

    //Functions
    //Position related
    public Vector3 SnappedPosition(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);

        return snapped;
    }

    public Vector3 GetWorldPosition(Camera cam, Vector3 position)
    {
        Ray ray = cam.ScreenPointToRay(position);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, new Vector3(20.0f, 0f, 0f), Color.red, 100f);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Vector3 GetWorldPosition2D(Camera cam, Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(position), Vector2.zero, 15.0f);
        if (hit)
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Transform GetRaycastHitObject(Vector3 position)
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin,ray.direction * 100, Color.cyan, 30.0f);
            return hit.transform;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 30.0f);
        return null;
    }

    public bool GetRaycastHit(Camera cam, Vector3 position, out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 30.0f);
            return true;
        }

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 30.0f);

        return false;
    }
    //

    //Bitwise support
    // Function to extract k bits from p position 
    // and returns the extracted value as integer 
    public int ExtractBit(int number, int k, int p)
    {
        return (((1 << k) - 1) & (number >> (p - 1)));
    }

    public int CheckAdjacentBit(int number)
    {
        return (number & (number >> 1));
    }
    //

    public void PrintBitArray(BitArray array)
    {
        string bits = "";
        for(int i = 0; i < array.Count; ++i)
        {
            bits += i + "-" + array[i].ToString() + " ";
        }

        Debug.Log(bits);
    }

    ////iTween functions
    //public void MoveToWithCallBack(GameObject target, Vector3 position, float time, string fnCallBackName)
    //{
    //    Hashtable hash = new Hashtable();
    //    hash.Add("position", position);
    //    hash.Add("time", time);
    //    hash.Add("oncomplete", fnCallBackName);
    //    iTween.MoveTo(target, hash);
    //}
    ////

    ////Event functions
    //public void DispatchEvent<T>(Solitaire.Event eventId, string key, T data)
    //{
    //    EventParam param = new EventParam();
    //    param.EventID = (int)eventId;
    //    param.Add<string>("tag", key);
    //    param.Add<T>(key, data);
    //    EventManager.Instance.RaiseEvent(eventId, param);
    //}
    //

    //Draw Rectangle
    //public void DrawRectangle(ref GameObject obj, ref Vector3[] vertices, bool IsWithMesh = false)
    //{
    //    if (IsWithMesh)
    //    {
    //        DrawRectangleWithMesh(ref obj, ref vertices);
    //    }
    //    else
    //    {
    //        DrawRectangle(ref obj, ref vertices);
    //    }
    //}

    //private void DrawRectangle(ref GameObject obj, ref Vector3[] vertices)
    //{
    //    LineRenderer rend = obj.GetComponent<LineRenderer>();

    //    rend.positionCount = 4;
    //    rend.SetPositions(vertices);
    //}

    //private void DrawRectangleWithMesh(ref GameObject obj, ref Vector3[] vertices)
    //{
    //    MeshFilter filter = obj.GetComponent<MeshFilter>();

    //    Vector2[] v2Lines = {
    //                            new Vector2(vertices[0].x, vertices[0].z),
    //                            new Vector2(vertices[1].x, vertices[1].z),
    //                            new Vector2(vertices[2].x, vertices[2].z),
    //                            new Vector2(vertices[3].x, vertices[3].z)
    //                        };

    //    Triangulator tr = new Triangulator(v2Lines);
    //    int[] indices = tr.Triangulate();

    //    Mesh msh = new Mesh();
    //    msh.vertices = vertices;
    //    msh.triangles = indices;
    //    //msh.colors = colors;

    //    msh.RecalculateNormals();
    //    msh.RecalculateBounds();
    //    msh.RecalculateTangents();

    //    filter.mesh = msh;
    //}
    //
}
