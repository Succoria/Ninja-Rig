using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBrushO : MonoBehaviour
{
    public int resolution = 512;
    public Texture2D whiteMap;
    public Texture2D tex;
    public float brushSize;
    public Texture2D brushTexture;
    public Texture2D alpha;
    public RenderTexture RT;
    Vector2 stored;

    private Vector3 cPos;

    public bool fActive;
    public Material fog;
    public static Dictionary<Collider, RenderTexture> paintTextures = new Dictionary<Collider, RenderTexture> ();

    void Start ()
    {
        tex = new Texture2D (resolution, resolution, TextureFormat.RGB24, false);
        CreateClearTexture (); // clear white texture to draw on
        fActive = false;

    }

    void Update ()
    {
        // if (fActive == true)
        //{
        cPos = transform.position;
        cPos.z = -12;
        Debug.DrawRay (cPos, Vector3.forward * 20f, Color.magenta);
        RaycastHit hit;
        if (Physics.Raycast (cPos, Vector3.forward, out hit))
        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) // delete previous and uncomment for mouse painting
        {
            Debug.Log ("raycast hit");
            Collider coll = hit.collider;
            if (coll != null && fActive == true)
            {
                // Debug.Log("raycast hit after if");
                if (!paintTextures.ContainsKey (coll)) // if there is already paint on the material, add to that
                {
                    Debug.Log ("Draw");
                    Renderer rend = hit.transform.GetComponent<Renderer> ();
                    paintTextures.Add (coll, GetWhiteRT ());
                    // Graphics.CopyTexture(paintTextures[coll], tex); 
                    fog.SetTexture ("_Paint_map", GetWhiteRT ());

                    //rend.material.SetTexture("_PaintMap", paintTextures[coll]);
                }
                if (stored != hit.lightmapCoord) // stop drawing on the same point
                {
                    stored = hit.lightmapCoord;
                    Debug.Log (stored);
                    Vector2 pixelUV = hit.lightmapCoord;
                    pixelUV.y *= resolution;
                    pixelUV.x *= resolution;
                    //pixelUV.x = -pixelUV.x;

                    DrawTexture (paintTextures[coll], pixelUV.x, pixelUV.y);
                }
            }
        }
        //}
    }

    void DrawTexture (RenderTexture rt, float posX, float posY)
    {

        RenderTexture.active = rt; // activate rendertexture for drawtexture;
        GL.PushMatrix (); // save matrixes
        GL.LoadPixelMatrix (0, resolution, resolution, 0); // setup matrix for correct size

        // draw brushtexture
        Graphics.DrawTexture (new Rect (posX - brushTexture.width / brushSize, (rt.height - posY) - brushTexture.height / brushSize, brushTexture.width / (brushSize * 0.5f), brushTexture.height / (brushSize * 0.5f)), brushTexture);
        GL.PopMatrix ();
        RenderTexture.active = null; // turn off rendertexture

    }

    RenderTexture GetWhiteRT ()
    {
        //RenderTexture RT = new RenderTexture(resolution, resolution, 32);//
        Graphics.Blit (whiteMap, RT);
        return RT;
    }

    void CreateClearTexture ()
    {
        whiteMap = new Texture2D (1, 1);
        whiteMap.SetPixel (0, 0, Color.white);
        whiteMap.Apply ();
    }
}