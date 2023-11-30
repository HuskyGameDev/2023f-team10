using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] int wavesUntilBoss = 3;
    [SerializeField] GameObject[] bosses;
    [SerializeField] GameObject bossSpawnPoint;

    [SerializeField] GameObject[] backgrounds;
    [SerializeField] SpriteRenderer warpBackground;
    private float red, green, blue, count;

    public enum LevelTypes { WORLD1 = 0, WORLD2 = 1, WORLD3 = 2 };

    public LevelTypes currentLevel = LevelTypes.WORLD1;
    public int wavesCompleted = 0;

    private void Start()
    {
        waveManager = GetComponent<WaveManager>();

        red = warpBackground.color.r;
        green = warpBackground.color.g;
        blue = warpBackground.color.b;
        count = 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //changeLevel((LevelTypes) ((int)(++currentLevel)%3));

            checkWave();
        }
    }

    //spawn and keep track of waves
    public void checkWave()
    {
        switch(currentLevel)
        {
            case LevelTypes.WORLD1:
                if (wavesCompleted == wavesUntilBoss)
                {
                    wavesCompleted = 0;
                    bossLevel();
                }
                else
                    waveManager.spawnWave(1);

                break;

            case LevelTypes.WORLD2:
                if (wavesCompleted == wavesUntilBoss)
                {
                    wavesCompleted = 0;
                    bossLevel();
                }
                else
                    waveManager.spawnWave(2);

                break;

            case LevelTypes.WORLD3:
                if (wavesCompleted == wavesUntilBoss)
                {
                    wavesCompleted = 0;
                    bossLevel();
                }
                else
                    waveManager.spawnWave(3);

                break;
        }
    }

    //handles boss levels
    private void bossLevel()
    {
        //spawn boss
        Instantiate(bosses[(int) currentLevel], bossSpawnPoint.transform);
    }

    public void onBossDefeat()
    {
        //if boss is defeated switch level
        changeLevel((LevelTypes)((int)(++currentLevel) % 3));

        //spawn the next wave after the boss is defeated
        checkWave();
    }

    //change background with warp transition
    public void changeLevel(LevelTypes newLevel)
    {
        CancelInvoke();

        //update current level for the rest of the script
        currentLevel = newLevel;

        //fade in warp and then change the level
        InvokeRepeating("fadeIn", 0, .01f);
    }

    private void fadeIn()
    {
        if (count < 100)
        {
            warpBackground.color = new Color(red, green, blue, (count / 100));
            count++;
        }
        else
        {
            for(int i = 0; i < 3; i++) 
            {
                backgrounds[i].SetActive(false);
            }

            backgrounds[(int)currentLevel].SetActive(true);
            InvokeRepeating("fadeOut", 1, .01f);
        }

    }

    private void fadeOut()
    {
        if (count > 0)
        {
            warpBackground.color = new Color(red, green, blue, (count / 100));
            count--;
        }
    }

}
