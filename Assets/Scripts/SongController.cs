using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class SongController : MonoBehaviour
{
    public int bpm = 100; // bpm
    private float bps;
    private float noteSpawnInterval;
    private float spawnTimer;
    private bool speedInced = false;

    public Note[] notesToSpawn;
    public Scoreboard scoreboard;
    public AudioSource song;

    private Vector3[] noteSpawnPositions;

    // Use this for initialization
    void Start()
    {
        this.bps = this.bpm / 60f; // 1 Beats per second = beats per minute / secs in min
        this.noteSpawnInterval = 1 / bps; // 1 second / amount of beats per second
        this.speedDown(); // Start slower
        this.spawnTimer = .15f; // Reaction-time buffer;
        this.scoreboard.maxPossibleScore = (int) (this.song.clip.length * this.bps * (Scoreboard.SCORE_MULTIPLYER * 2));

        this.noteSpawnPositions = this.CreateSpawnPositions();
    }

    private Vector3[] CreateSpawnPositions()
    {
        NoteBarBtn[] noteBarBtns = FindObjectsOfType<NoteBarBtn>();
        Vector3[] spawnPositions = new Vector3[noteBarBtns.Length];
        int y = Screen.height - (Screen.height / 9);

        // Sorting btns by left to right
        Array.Sort(noteBarBtns, (NoteBarBtn a, NoteBarBtn b) =>
        {
            return a.transform.position.x < b.transform.position.x ? -1 : 1;
        });

        for (int i = 0; i < noteBarBtns.Length; i++)
        {
            NoteBarBtn currBtn = noteBarBtns[i];
            spawnPositions[i] = new Vector3(currBtn.transform.position.x, y);
        }

        return spawnPositions;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.song.isPlaying)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                // Width (x) then Height (y)
                int rndSpawnPoint = Random.Range(0, this.noteSpawnPositions.Length);

                Note spawned = Instantiate(this.notesToSpawn[rndSpawnPoint], this.noteSpawnPositions[rndSpawnPoint], Quaternion.identity);
                spawned.timeToReachBar = this.bps;
                spawned.scoreboard = scoreboard;
                this.spawnTimer = this.noteSpawnInterval;
            }

            if (this.song.time >= 62.5f - this.bps)
            {
                if (this.speedInced)
                {
                    this.speedDown();
                }
            }
            else if (this.song.time >= 9.5f - this.bps)
            {
                if (!this.speedInced)
                {
                    this.speedUp();
                }
            } 
        }
    }

    private void speedUp ()
    {
        this.speedInced = true;
        this.noteSpawnInterval /= 2;
    }

    private void speedDown()
    {
        this.speedInced = false;
        this.noteSpawnInterval *= 2;
    }
}
