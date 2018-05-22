using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public SegmentList lstSegmentList;
    public bool SHOW_COLLIDER;
    // Level Spawning
    //public const float DISTANCE_BEFORE_SPAWN = 100.0f;
    //private const int INITIAL_SEGMENTS = 10;
    //private const int INITIAL_TRANSITON_SEGMENTS = 2;
    //private const int MAX_SEGMENTS_ON_SCREEN = 15;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1;
    private int y2;
    private int y3;

    // List of Piece
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();

    // List of Segment
   // public List<Segment> availableSegments = new List<Segment>();
   // public List<Segment> availableTransitions = new List<Segment>();
    public List<Segment> segments = new List<Segment>();

    // 
    private bool isMoving = false;
    public List<Piece> pieces = new List<Piece>(); // all pieces in pool 
    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;
        SHOW_COLLIDER = false;
        Debug.Log(lstSegmentList.availableSegments[0].name);
    }
    void Start()
    {
        
        for (int i = 0; i < GameSettings.INITIAL_SEGMENTS; i++)
        {
            // generate segments
            if( i < GameSettings.INITIAL_TRANSITON_SEGMENTS)
            {
                SpawnTransition();
            }
            else
            {
                GenerateSegments();
            }
            
        }
    }
    void Update()
    {
        if (currentSpawnZ - cameraContainer.position.z < GameSettings.DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegments();
        }
        if (amountOfActiveSegments >= GameSettings.MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
    }
    public void GenerateSegments()
    {
        SpawnSegment();
        if (Random.Range(0f, 1f) < (continiousSegments * 0.25f))
        {
            // spawn transitin seg
            continiousSegments = 0;
            SpawnTransition();
        }
        else
        {
            continiousSegments++;
        }

    }
    public void SpawnTransition()
    {
        List<Segment> possiableTransition = lstSegmentList.availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        //List<Segment> possiableTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possiableTransition.Count);

        Segment s = GetSegment(id, true);
        //Segment s = possiableSeg[id];
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;
        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;
        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }
    public void SpawnSegment()
    {
        List<Segment> possiableSeg = lstSegmentList.availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        //List<Segment> possiableSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possiableSeg.Count);

        Segment s = GetSegment(id, false);
        //Segment s = possiableSeg[id];
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;
        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;
        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }
    public Segment GetSegment(int id, bool transition)
    {
        Segment r = null;
        r = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);
        if(r == null)
        {
            GameObject go = Instantiate((transition) ? lstSegmentList.availableTransitions[id].gameObject : lstSegmentList.availableSegments[id].gameObject) as GameObject;
            r = go.GetComponent<Segment>();
            r.SegId = id;
            r.transition = transition;
            segments.Insert(0, r);
        }
        else
        {
            segments.Remove(r);
            segments.Insert(0, r);
        }
        return r;
    }
    public Piece GetPiece(PieceTypes pt, int visualIndex)
    {
        //Debug.Log("nhay vao day");
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);
        if(p == null)
        {
           // Debug.Log("nhay vao day 2222");
            GameObject go = null;
            if(pt == PieceTypes.ramp)
            {
                go = ramps[visualIndex].gameObject;
            }
            else if(pt == PieceTypes.longblock)
            {
                go = longblocks[visualIndex].gameObject;
            }
            else if(pt == PieceTypes.jump)
            {
                go = jumps[visualIndex].gameObject;
            }
            else if(pt == PieceTypes.slide)
            {
                go = slides[visualIndex].gameObject;
            }
            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }
        return p;
    }
}
