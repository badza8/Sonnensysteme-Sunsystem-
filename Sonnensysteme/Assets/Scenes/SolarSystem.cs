using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SolarSystem : MonoBehaviour
{
    #region Mesh
    Mesh           mesh        ;
    MeshFilter     meshFilter  ;
    MeshRenderer   meshRenderer;
    #endregion

    #region PlanetParameters

    int numberOfPointsInWidth    = 20;
    int numberOfPointsInHeight   = 20;
    

    public List<Texture2D>  textures    = new List<Texture2D>();
    public List<SpaceObject>     spaceObjects     = new List<SpaceObject>();   

    private int numberOfSpaceObjects=9;
    public float distanceOfLastPlanet = 2550;

    #endregion

    public void Reset()
    {

        // Here we have Texture for  spaceobjects of our Solarsystem 
        textures.Add(Resources.Load("sun") as Texture2D);
        textures.Add(Resources.Load("mercury") as Texture2D);
        textures.Add(Resources.Load("venus") as Texture2D);

        textures.Add(Resources.Load("earth") as Texture2D);
        textures.Add(Resources.Load("mars") as Texture2D);
        textures.Add(Resources.Load("jupiter") as Texture2D);

        textures.Add(Resources.Load("saturn") as Texture2D);
        textures.Add(Resources.Load("uranus") as Texture2D);
        textures.Add(Resources.Load("neptune") as Texture2D);



        // Here we have spaceobjects of our Solarsystem 
        spaceObjects.Add(new SpaceObject(transform.position, textures[0], 100, 0, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[1], 100, 450, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[2], 100, 700, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[3], 100, 950, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[4], 100, 1200, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[5], 100, 1550, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[6], 100, 1800, this.numberOfPointsInWidth, this.numberOfPointsInHeight, true, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[7], 100, 2050, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));
        spaceObjects.Add(new SpaceObject(transform.position, textures[8], 100, 2300, this.numberOfPointsInWidth, this.numberOfPointsInHeight, false, false));

        CreateMesh();

    }

    public void CreateMesh()
    {
        #region Mesh Initialization

        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = this.gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
            meshRenderer = this.gameObject.AddComponent<MeshRenderer>();

        mesh = meshFilter.sharedMesh;
        if (this.mesh == null)
            this.mesh = new Mesh();

        #endregion  Mesh Initialization


        #region Relative and fix coordinates of our solar system (Vertices and trianglesmust be fixed)
        /*#region Vertices UV's and Triangles Initialization

        List<Vector3>   vertices    = new List<Vector3>     ();
        List<Vector2>   uvs         = new List<Vector2>     ();
        List<List<int>> triangles   = new List<List<int>>   ();
        for (int i = 0; i < 10; i++)
            triangles.Add(new List<int>());

        #endregion

        #region Local Parameters

        float widthStep     = 360f / ((float)numberOfPointsInWidth - 1f)    ;
        float heightStep    = 180f / ((float)numberOfPointsInHeight - 1f)   ;

        float textureWidth  = 1f / ((float)numberOfPointsInWidth - 1f)  ;
        float textureHeight = 1f / ((float)numberOfPointsInHeight - 1f) ;

        float height,   radiusWithHeight;

        int verticesProPlanet = numberOfPointsInWidth * numberOfPointsInHeight;

        #endregion Local Parameters

        #region Planeten Daten

        // Radius Array and Variables

        float[] radiuses = new float[9];

        float   radiusOfMercury = radiusOfSun * 3f / 100,
                radiusOfVenus   = radiusOfSun * 8f / 100,
                radiusOfEarth   = radiusOfSun * 9f / 100,
                radiusOfMars    = radiusOfSun * 4f / 100,
                radiusOfJupiter = radiusOfSun * 102f/ 100,
                radiusOfSaturn  = radiusOfSun * 86f / 100,
                radiusOfUran    = radiusOfSun * 36f / 100,
                radiusOfNeptun  = radiusOfSun * 35f / 100;

        radiuses[0] = radiusOfSun       ;
        radiuses[1] = radiusOfMercury   ;
        radiuses[2] = radiusOfVenus     ; 
        radiuses[3] = radiusOfEarth     ; 
        radiuses[4] = radiusOfMars      ; 
        radiuses[5] = radiusOfJupiter   ; 
        radiuses[6] = radiusOfSaturn    ; 
        radiuses[7] = radiusOfUran      ;
        radiuses[8] = radiusOfNeptun    ;


        // Distance Array and Variables

        float[] distances = new float[9];

        float   distaceFromSunToMercury = radiusOfSun * 2f  ,// * 4.1f
                distaceFromSunToVenus   = radiusOfSun * 3f  ,// * 7.7f  
                distaceFromSunToEarth   = radiusOfSun * 4f  ,// * 10.7f 
                distaceFromSunToMars    = radiusOfSun * 5f  ,// * 16.3f 
                distaceFromSunToJupiter = radiusOfSun * 6.6f  ,// * 55.8f 
                distaceFromSunToSaturn  = radiusOfSun * 9.1f  ,// * 102.4f
                distaceFromSunToUran    = radiusOfSun * 10.6f  ,// * 206f  
                distaceFromSunToNeptun  = radiusOfSun * 11.5f  ;// * 332.8f

        distances[0] = 0f;
        distances[1] = distaceFromSunToMercury  ;
        distances[2] = distaceFromSunToVenus    ;
        distances[3] = distaceFromSunToEarth    ;
        distances[4] = distaceFromSunToMars     ;
        distances[5] = distaceFromSunToJupiter  ;
        distances[6] = distaceFromSunToSaturn   ;
        distances[7] = distaceFromSunToUran     ;
        distances[8] = distaceFromSunToNeptun   ;

        // Centers Array and Variables

        Vector3[] centersOfSpaceObjects = new Vector3[9];

        Vector3 centerOfMercury = transform.TransformPoint( new Vector3(distaceFromSunToMercury ,0f ,0f)),
                centerOfVenus   = transform.TransformPoint( new Vector3(distaceFromSunToVenus   ,0f ,0f)),
                centerOfEarth   = transform.TransformPoint( new Vector3(distaceFromSunToEarth   ,0f ,0f)),
                centerOfMars    = transform.TransformPoint( new Vector3(distaceFromSunToMars    ,0f ,0f)),
                centerOfJupiter = transform.TransformPoint( new Vector3(distaceFromSunToJupiter ,0f ,0f)),
                centerOfSaturn  = transform.TransformPoint( new Vector3(distaceFromSunToSaturn  ,0f ,0f)),
                centerOfUran    = transform.TransformPoint( new Vector3(distaceFromSunToUran    ,0f ,0f)),
                centerOfNeptun  = transform.TransformPoint( new Vector3(distaceFromSunToNeptun  ,0f ,0f));

        centersOfSpaceObjects[0] = transform.position   ;
        centersOfSpaceObjects[1] = centerOfMercury      ;
        centersOfSpaceObjects[2] = centerOfVenus        ;
        centersOfSpaceObjects[3] = centerOfEarth        ;
        centersOfSpaceObjects[4] = centerOfMars         ;
        centersOfSpaceObjects[5] = centerOfJupiter      ;
        centersOfSpaceObjects[6] = centerOfSaturn       ;
        centersOfSpaceObjects[7] = centerOfUran         ;
        centersOfSpaceObjects[8] = centerOfNeptun       ;


        #endregion Planeten Daten


        #region Verices and UV's for the Sun and Planets

        for (int p = 0; p < 9; p++)     //  Sun and Planets
        {
            for (int i = 0; i < numberOfPointsInWidth; i++)
            {
                for (int j = 0; j < numberOfPointsInHeight; j++)
                {
                    // Height of Point from Surface
                    height = textures[p].GetPixelBilinear(i * textureWidth, j * textureWidth).grayscale;

                    //if (height > 0.65) height = 0.65f;
                    //if (height < 0.3) height = 0.3f;
                    // Height of Point from Center 
                    radiusWithHeight    =  radiuses[p] + (height*(radiuses[p]/10));
                    

                    Vector3 pointOnSphere = PointOnSphere(  i * widthStep,
                                                    j * heightStep,
                                                    radiusWithHeight,
                                                    centersOfSpaceObjects[p]);

                    // Point on Angle 
                    Vector3 rotatedPoint = RotateAroundPivot(   pointOnSphere, 
                                                        centersOfSpaceObjects[p],    
                                                        new Vector3(90, 0f, 0f));

                    // Vertice
                    vertices.Add(rotatedPoint);
                    
                    // UV- Coordinates
                    uvs.Add(    new Vector2(i * textureWidth, j * textureHeight));
                }
            }
        }
        #endregion Verices and UV's for the Sun and Planets

        
        #region Submeshes Triangles 

        int offset;
        for (int p = 0; p < 9; p++)
        {
            offset = verticesProPlanet* p;
            for (int i = 0; i < numberOfPointsInWidth - 1; i++)
            {
                for (int j = 1; j < numberOfPointsInHeight; j++)
                {
                    triangles[p].Add(i          * numberOfPointsInHeight    + j - 1    + offset);
                    triangles[p].Add(i          * numberOfPointsInHeight    + j        + offset);
                    triangles[p].Add((i + 1)    * numberOfPointsInHeight    + j - 1    + offset);
                                                                                       
                    triangles[p].Add((i + 1)    * numberOfPointsInHeight    + j - 1    + offset);
                    triangles[p].Add(i          * numberOfPointsInHeight    + j        + offset);
                    triangles[p].Add((i + 1)    * numberOfPointsInHeight    + j        + offset);

                }
            }
        }

        #endregion Submeshes Triangles

        #region Vertices for Ring

        for (int i = 0; i < numberOfPointsInWidth ; i++)
        {
            Vector3 pointOnCircle = PointOnCircle(i * widthStep, radiusOfSaturn * 1.2f, centerOfSaturn);
            vertices.Add(pointOnCircle);
            uvs.Add(new Vector2(i * textureWidth, 0f));
            
            pointOnCircle = PointOnCircle(i * widthStep, radiusOfSaturn * 1.3f, centerOfSaturn);
            vertices.Add(pointOnCircle);
            uvs.Add(new Vector2(i * textureWidth, 1f));
            
            
        }

        #endregion Vertices for Ring

        #region Triangles for Ring

        offset = verticesProPlanet * 9;

        for (int i = 0; i < numberOfPointsInWidth *2 - 4; i+=2)
        {
            
            triangles[9].Add(i + offset);
            triangles[9].Add(i + 1 + offset);
            triangles[9].Add(i + 2 + offset);

            triangles[9].Add(i + 1 + offset);
            triangles[9].Add(i + 3 + offset);
            triangles[9].Add(i + 2 + offset);
            
            
        }
        triangles[9].Add(numberOfPointsInWidth * 2 - 4 + offset);
        triangles[9].Add(numberOfPointsInWidth * 2 - 3 + offset);
        triangles[9].Add(0 + offset);

        triangles[9].Add(numberOfPointsInWidth * 2 - 3 + offset);
        triangles[9].Add(1 + offset);
        triangles[9].Add(0 + offset);

        #endregion Triangles for Ring*/
        #endregion Relative and fix coordinates of our solar system


        if (numberOfSpaceObjects<spaceObjects.Count)
        {
            distanceOfLastPlanet += 250;
        }

        //  With this parameter we can take note of whether the spaceobject have had a ring (or rings) before.
        bool ringMet = false;

        // We save vertices of every spaceobjects in 1 big array
        List<Vector3> vertices = new List<Vector3>();
        
        for (int i = 0; i < spaceObjects.Count; i++)
        {
            int verticesCount = spaceObjects[i].vertices.Count;
            for (int j = 0; j < verticesCount; j++)
            {
                vertices.Add(   spaceObjects[i].vertices[j]  );
            }
            
        }


        // We save uv's of every spaceobject in 1 big array
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < spaceObjects.Count; i++)
        {
            int uvsCount = spaceObjects[i].uvs.Count;
            for (int j = 0; j < uvsCount; j++)
            {
                uvs.Add(    spaceObjects[i].uvs[j]   );
            }

        }


        // Now we calculate triangles of every spaceobjecta in a list of triangles ,which itself contains list for every planet's triangles
        // and 1 list for rings of the planets , here we save every ring in last List, because for every ring we have the same texture.   
        List<List<int>> trianglesOfPlanet = new List<List<int>>();

        //  We initialize every list that we need for spaceobjects , except for the last one for rings
        for (int i = 0; i < spaceObjects.Count; i++)
        {
            trianglesOfPlanet.Add(new List<int>());
        }

        //  We need offset , because without it we will allocate every place on the start coorinate over each other 
        int offset=0;

        for (int p = 0; p < spaceObjects.Count; p++)
        {
            
            for (int i = 0; i < spaceObjects[p].numberOfPointsInWidth - 1; i++)
            {
                for (int j = 1; j < spaceObjects[p].numberOfPointsInHeight; j++)
                {
                    trianglesOfPlanet[p].Add(i * spaceObjects[p].numberOfPointsInHeight + j - 1      + offset);
                    trianglesOfPlanet[p].Add(i * spaceObjects[p].numberOfPointsInHeight + j          + offset);
                    trianglesOfPlanet[p].Add((i + 1) * spaceObjects[p].numberOfPointsInHeight + j - 1 + offset);

                    trianglesOfPlanet[p].Add((i + 1) * spaceObjects[p].numberOfPointsInHeight + j - 1    + offset);
                    trianglesOfPlanet[p].Add(i * spaceObjects[p].numberOfPointsInHeight + j              + offset);
                    trianglesOfPlanet[p].Add((i + 1) * spaceObjects[p].numberOfPointsInHeight + j        + offset);

                }
            }

            // We increase our offset with the number of used vertices in last for loop
            offset += spaceObjects[p].numberOfPointsInWidth * spaceObjects[p].numberOfPointsInHeight ;


            if (spaceObjects[p].ring)
            {
                //  If we meet a planet, which has the ring , then we initialize last list for it
                if (ringMet==false)
                {
                    ringMet = true;
                    trianglesOfPlanet.Add(new List<int>());
                }
                
                for (int i = 0; i < spaceObjects[p].numberOfPointsInWidth * 2 - 4; i += 2)
                {
                    //  Ringsurfaceview from down
                    trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(i + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(i + 1 + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(i + 2 + offset);
                                      
                    trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(i + 1 + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(i + 3 + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(i + 2 + offset);

                    //  Ringsurfaceview from up
                    trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(i + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(i + 2 + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(i + 1 + offset);

                    trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(i + 1 + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(i + 2 + offset);
                    trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(i + 3 + offset);
                }

                //  We initialize triangles of last square with the start vertices to avoid their clones (0,1)

                //  Ringsurfaceview from down
                trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(spaceObjects[p].numberOfPointsInWidth * 2 - 4 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(spaceObjects[p].numberOfPointsInWidth * 2 - 3 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(0 + offset);

                trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(spaceObjects[p].numberOfPointsInWidth * 2 - 3 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(1 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count-1].Add(0 + offset);

                //  Ringsurfaceview from up
                trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(spaceObjects[p].numberOfPointsInWidth * 2 - 4 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(0 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(spaceObjects[p].numberOfPointsInWidth * 2 - 3 + offset);
                

                trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(spaceObjects[p].numberOfPointsInWidth * 2 - 3 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(0 + offset);
                trianglesOfPlanet[trianglesOfPlanet.Count - 1].Add(1 + offset);

                // We increase our offset with the number of used vertices in last for loop + 12 non-for loop initialization
                offset += spaceObjects[p].numberOfPointsInWidth * 2  ;
            }

            
        }
        

        

        mesh.Clear();

        //  Vertices
        mesh.vertices       =   vertices.ToArray();

        //  Submeshes
        mesh.subMeshCount   = trianglesOfPlanet.Count;

        for (int i = 0; i < trianglesOfPlanet.Count; i++)
            mesh.SetTriangles(trianglesOfPlanet[i].ToArray(), i);
        

        //UnityEditor.SceneView.lastActiveSceneView.camera.farClipPlane =5000;

        //  UV
        mesh.uv             =   uvs.ToArray();

        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;


        #region Materials

        // Initialization of materials with the textures of spaceobjects  and ,when we have, with static spaceobjects ring texture 
        Material[] materials = new Material[trianglesOfPlanet.Count];
        
        for (int i = 0; i < trianglesOfPlanet.Count; i++)
        {


            // If spaceobjects has the ring and index of materials is last one, then we apply ring texture
            if (ringMet==true && i == trianglesOfPlanet.Count - 1)
            {
                materials[i] = new Material(Shader.Find("Diffuse"));
                materials[i].mainTexture = SpaceObject.ringTexture;
                break;
            }
            materials[i] = new Material(Shader.Find("Diffuse"));
            materials[i].mainTexture = spaceObjects[i].texture;
            
        }

        meshRenderer.sharedMaterials = materials;

        #endregion Materials

        
    }



    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Unity Methods
}
