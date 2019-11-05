using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SolarSystem))]
public class SolarSystemEditor : Editor
{
    SolarSystem solarSystem;

    Vector3 systemCenter;
    Texture2D texture;
    float radius = 100;
    float distance ;
    int numberOfPointsInWidth;
    int numberOfPointsInHeight;
    bool ring = false;
    bool autotexture = false;
    bool foldoutNew = false;
    bool foldoutEdit = false;
    bool foldoutDelete = false;
    int indexOfPlanetToEdit=0;
    int indexOfPlanetToDelete = 0;

    void Awake()
    {
        solarSystem = target as SolarSystem;
    }

    public override void OnInspectorGUI()
    {
        
        EditorGUI.BeginChangeCheck();







        //  Textures
        /*
        planeten.textures[0] = EditorGUILayout.ObjectField(
            "Sun's Surface"     ,   planeten.textures[0],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[1] = EditorGUILayout.ObjectField(
            "Mercury's Surface" ,   planeten.textures[1],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[2] = EditorGUILayout.ObjectField(
            "Venus's Surface"   ,   planeten.textures[2],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[3] = EditorGUILayout.ObjectField(
            "Earth's Surface"   ,   planeten.textures[3],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[4] = EditorGUILayout.ObjectField(
            "Mars's Surface"    ,   planeten.textures[4],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[5] = EditorGUILayout.ObjectField(
            "Jupiter's Surface" ,   planeten.textures[5],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[6] = EditorGUILayout.ObjectField(
            "Saturn's Surface"  ,   planeten.textures[6],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[7] = EditorGUILayout.ObjectField(
            "Uran's Surface"    ,   planeten.textures[7],   typeof(Texture2D), true) as Texture2D;
        planeten.textures[8] = EditorGUILayout.ObjectField(
            "Neptune's Surface" ,   planeten.textures[8],   typeof(Texture2D), true) as Texture2D;
            */



        //  Planet Parameters

        //  Center of our system

        this.systemCenter = solarSystem.gameObject.transform.position;

        this.distance = solarSystem.distanceOfLastPlanet;


        //  Add new space object with GUI-elements which contain necessary Parameters   


        if (foldoutNew=EditorGUILayout.Foldout(foldoutNew, "Neues Weltraum Objekt"))
        {
            this.autotexture = EditorGUILayout.Toggle("autotexture ", this.autotexture);

            if (!autotexture)
            {
                this.texture = EditorGUILayout.ObjectField("Planet's Surface", this.texture, typeof(Texture2D), true) as Texture2D;
            }

            this.numberOfPointsInWidth = EditorGUILayout.IntSlider("numberOfPointsWidth", this.numberOfPointsInWidth, 20, 70);

            this.numberOfPointsInHeight = EditorGUILayout.IntSlider("numberOfPointsHeight", this.numberOfPointsInHeight, 20, 70);

            this.radius = EditorGUILayout.Slider("radius", this.radius, 5f, 700f);
            this.distance = EditorGUILayout.Slider("distance", this.distance, 5f, 20000f);

            this.ring = EditorGUILayout.Toggle("ring", this.ring);


            //  we apply our changes
            if (GUILayout.Button("neue Planete hinnfugen"))
            {
                
                solarSystem.spaceObjects.Add(new SpaceObject(this.systemCenter, this.texture, this.radius,
                                                this.distance, this.numberOfPointsInWidth, this.numberOfPointsInWidth,
                                                this.ring, this.autotexture));
                
                solarSystem.CreateMesh();

            }
        }



        //  Edit any parameters of existing spaceobjects with GUI-elements


        if (this.foldoutEdit = EditorGUILayout.Foldout(foldoutEdit, "Hinzugefugte Weltraum Objekte bearbeiten")) {
            
            this.indexOfPlanetToEdit = EditorGUILayout.IntSlider("Welches Weltraum Objekt bearbeiten? ", this.indexOfPlanetToEdit,  0, solarSystem.spaceObjects.Count-1);

            //  1 st we read parameters from the planet , 2nd we paint them on GUI , 3rd we edit these values with our GUI 
            
            this.autotexture=this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].autotexture = EditorGUILayout.Toggle("autotexture ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].autotexture);

            if (!autotexture)
            {
                this.texture = this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].texture= EditorGUILayout.ObjectField("Oberflaeche des Weltraum Objekts ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].texture, typeof(Texture2D), true) as Texture2D;
            }

            this.numberOfPointsInWidth = this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].numberOfPointsInWidth= EditorGUILayout.IntSlider("Anzahl der Punkte in breite ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].numberOfPointsInWidth, 20, 70);

            this.numberOfPointsInHeight = this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].numberOfPointsInHeight= EditorGUILayout.IntSlider("Anzahl der Punkte in hoehe ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].numberOfPointsInHeight, 20, 70);

            this.radius = this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].radius= EditorGUILayout.Slider("Radius ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].radius, 5f, 700f);
            this.distance = this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].distance= EditorGUILayout.Slider("Entfernung ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].distance, 5f, 20000f);

            this.ring=this.solarSystem.spaceObjects[this.indexOfPlanetToEdit].ring = EditorGUILayout.Toggle("Ring ", solarSystem.spaceObjects[this.indexOfPlanetToEdit].ring);


            //  we apply our changes
            if (GUILayout.Button("Weltraum Objekt bearbeiten "))
            {

                solarSystem.spaceObjects[indexOfPlanetToEdit]=new SpaceObject(this.systemCenter, this.texture, this.radius,
                                                                    this.distance, this.numberOfPointsInWidth, this.numberOfPointsInWidth,
                                                                    this.ring, this.autotexture);

                solarSystem.CreateMesh();

            }

        }



        //  Delete  n th space


        if (this.foldoutDelete = EditorGUILayout.Foldout(foldoutDelete, "Hinzugefugtes Weltraum Objekt loschen"))
        {
           
            this.indexOfPlanetToDelete = EditorGUILayout.IntSlider("Welches Weltraum Objekt loschen? ", this.indexOfPlanetToDelete, 0, solarSystem.spaceObjects.Count - 1);
            

            if (GUILayout.Button("Weltraum Objekt loeschen"))
            {
                this.solarSystem.spaceObjects.RemoveAt(this.indexOfPlanetToDelete);
                solarSystem.CreateMesh();
            }
        }
        
        if (GUILayout.Button("Draw"))
            solarSystem.CreateMesh();

        EditorGUI.EndChangeCheck();
    }

    [MenuItem("SolarSystem/Our System")]
    public static SolarSystem CreateLSystem()
    {
        GameObject go = new GameObject("Our System");

        SolarSystem lsys = go.AddComponent<SolarSystem>();
        return lsys;

    }

}
