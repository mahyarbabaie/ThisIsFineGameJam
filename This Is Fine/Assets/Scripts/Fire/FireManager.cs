﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireManager : MonoBehaviour {

    [SerializeField] private Text fireCountText;
    [SerializeField] private GameObject fireObject;
    [SerializeField] private int maxPoolSize;

    private GameObject[] fireSpawnPoints;

    private int fireCount;
    public int FireCount
    {
        get { return fireCount; }
        set { fireCount = value; }
    }

    private int fireDamage = 10;
    public int FireDamage
    {
        get { return fireDamage; }
    }

    private static FireManager instance;
    public static FireManager Instance
    {
        get
        {
            if (!instance) { instance = GameObject.FindObjectOfType<FireManager>(); }
            return instance;
        }
    }

    // Use this for initialization
    private void Start () {
        fireCount = 0;
        fireCountText.text = fireCount.ToString();
        fireSpawnPoints = GameObject.FindGameObjectsWithTag("FireSpawnPoint");
        GamePoolManager.Instance.CreatePool(fireObject, maxPoolSize);
        SpawnFireHandler();
    }
	
	// Update is called once per frame
	private void Update () {
        fireCountText.text = fireCount.ToString();
    }

    public void SpawnFireHandler(){

        foreach(GameObject spawn in fireSpawnPoints)
        {
            Vector2 spawnLoc = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
            GamePoolManager.Instance.ReuseObject(fireObject, spawnLoc, Quaternion.identity);
            fireCount++;
        }
    }

    public void DeSpawnFireHandler()
    {
        if (GameObject.FindGameObjectsWithTag("Fire") != null)
        {
            GameObject[] activeFires = GameObject.FindGameObjectsWithTag("Fire");
            foreach (GameObject fire in activeFires)
            {
                fire.SetActive(false);
            }
        }
        FireCount = 0;
    }

}
