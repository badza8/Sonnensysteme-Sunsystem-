using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject
{
    #region Attributs

    //  do we have a ring for this planet?
    public bool ring        =   false   ;
    //  do we generate Texture automatically?
    public bool autotexture =   false   ;


    public Texture2D    texture     ;
    public Vector3      planetCenter;

    // center of our game object
    public Vector3      systemCenter;


    //  distance of our new spaceobject from the center of game object
    public float    distance;
    //  radius of our new  spaceobject
    public float    radius  ;

    public int numberOfPointsInWidth    ;
    public int numberOfPointsInHeight   ;
    

    private float   widthStep ;
    private float   heightStep;
    
    private float   textureWidth ;
    private float   textureHeight;

    public  List<Vector3>   vertices  = new List<Vector3>   ();
    public  List<Vector2>   uvs       = new List<Vector2>   ();
    public  List<int>       triangles = new List<int>       ();

    public static Texture2D ringTexture = (Resources.Load("saturn_ring") as Texture2D);

    private int verticesProPlanet;

    #endregion Attributs

    public SpaceObject( Vector3 systemCenter,   Texture2D texture,  
                    float radius,   float distance  ,   
                    int numberOfPointsInWidth,  int numberOfPointsInHeight ,    
                    bool ring, bool autotexture)
    {

        // center of our game object
        this.systemCenter           =   systemCenter            ;
        this.texture                =   texture                 ;

        this.numberOfPointsInWidth  =   numberOfPointsInWidth   ;
        this.numberOfPointsInHeight =   numberOfPointsInHeight  ;


        //  do we have a ring for this spaceobject? 
        this.ring                   =   ring                    ;

        //  do we generate Texture automatically?
        this.autotexture            =   autotexture             ;

        //  radius of our new  planet
        this.radius                 =   radius                  ;

        //  distance of our new spaceobject from the cnter of game object
        this.distance               =   distance                ;

        
        genrateParameters();

        
        generateVerticeAndUVsOfThePlanet();


        //  When we need to have spaceobject's ring, then we generate its vertexes and uv's
        if (ring) {
            generateVerticesAndUVsOfRingOfThePlanet();
        }
    }

    //  We generate all local parameters that depend on the parameters from the constructor 
    private void genrateParameters()
    {
        //  generating the center of our spaceobject
        this.planetCenter = systemCenter + new Vector3(this.distance, 0f, 0f);

        //  we have 360/ number of points on width of the sphere
        this.widthStep = 360f / ((float)numberOfPointsInWidth - 1f);
        //  we have 180/ Number of Points on height of the sphere
        this.heightStep = 180f / ((float)numberOfPointsInHeight - 1f);

        //  we must use our whole texture so , that all the points will be on the same distance from each other and the max size is 1
        this.textureWidth = 1f / ((float)numberOfPointsInWidth - 1f);
        this.textureHeight = 1f / ((float)numberOfPointsInHeight - 1f);

        //  here we save the number of every Point on Sphere
        this.verticesProPlanet = this.numberOfPointsInWidth * this.numberOfPointsInHeight;
    }

    #region Planet
    
    public void generateVerticeAndUVsOfThePlanet()
    {
        float height, radiusWithHeight;
        for (int i = 0; i < this.numberOfPointsInWidth; i++)
        {
            for (int j = 0; j < this.numberOfPointsInHeight; j++)
            {
                // Height of point on surface became from grayscale of the color on surface
                height = this.texture.GetPixelBilinear(i * textureWidth, j * textureWidth).grayscale;
                if (height > 0.65) height = 0.65f;
                if (height < 0.3) height = 0.3f;

                // Height of point from center of our spaceobject 
                radiusWithHeight = this.radius + (height * (this.radius / 10));

                //  Here we calculate where the points on sphere must be allocated  
                Vector3 pointOnSphere = PointOnSphere(i * widthStep,
                                                j * heightStep,
                                                radiusWithHeight
                                                );

                // We change the angle of the point so , that North will be up on Y axis
                Vector3 rotatedPoint = RotateAroundPivot(pointOnSphere,
                                                        new Vector3(90, 0f, 0f));

                // We add our generated point to vertice array (x,y,z)
                vertices.Add(rotatedPoint);

                //  We add a new point on the Texture2D (x,y)
                uvs.Add(new Vector2(i * this.textureWidth, j * this.textureHeight));
            }
        }
        
    }
    
   
    #endregion
        
    #region Ring

    public void generateVerticesAndUVsOfRingOfThePlanet()
    {
        for (int i = 0; i < this.numberOfPointsInWidth; i++)
        {
            //  1st we generate a point on inner circle and then on outer  

            //  Here we calculate where the points on circle must be allocated 
            Vector3 pointOnCircle = PointOnCircle(i * this.widthStep, this.radius * 1.2f);
            // We add our generated point to vertice array (x,0,z)
            this.vertices.Add(pointOnCircle);
            //  We add new Point on the Texture2D (x,0)
            this.uvs.Add(new Vector2(0f, 0f));

            //  Here we calculate where the points on circle must be allocated 
            pointOnCircle = PointOnCircle(i * this.widthStep, this.radius * 1.3f);
            // We add our generated point to vertice array (x,0,z)
            this.vertices.Add(pointOnCircle);
            //  We add new point on the Texture2D (x,1)
            this.uvs.Add(new Vector2(1f, 0f));

        }
    }
    
    #endregion

    #region Calculation Methods


    //  Method calculates the coordinates of each point, these build in the end a sphere
    private Vector3 PointOnSphere(float width, float height,float radiusWithHeight)
    {

        float widthRadian = width * Mathf.Deg2Rad;
        float heightRadian = height * Mathf.Deg2Rad;


        float x = radiusWithHeight * Mathf.Cos(widthRadian) * Mathf.Sin(heightRadian);
        float y = radiusWithHeight * Mathf.Sin(widthRadian) * Mathf.Sin(heightRadian);
        float z = radiusWithHeight * Mathf.Cos(heightRadian);

        return this.planetCenter + new Vector3(x, y, z);
    }

    // We change the angle of the point so , that North will be up on Y axis
    private Vector3 RotateAroundPivot(Vector3 point, Vector3 angles)
    {
        Vector3 direction = point - this.planetCenter;
        direction = Quaternion.Euler(angles) * direction;
        point = this.planetCenter + direction;
        return point;
    }

    //  Method calculates the coordinates of each point, these build in the end a circle
    private Vector3 PointOnCircle(float width, float radiusWithHeight)
    {
        float myCos = Mathf.Cos(width * Mathf.Deg2Rad) * radiusWithHeight;   // angle * 
        float mySin = Mathf.Sin(width * Mathf.Deg2Rad) * radiusWithHeight;   // angle * 

        return this.planetCenter + new Vector3(myCos, 0f, mySin);
    }

    #endregion

}
