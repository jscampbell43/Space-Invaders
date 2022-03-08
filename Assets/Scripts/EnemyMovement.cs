using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject MysteryEnemy;
    public GameObject Enemy_10pts;
    public int num10Enemies = 10;
    public GameObject Enemy_20pts;
    public int num20Enemies = 10;
    public GameObject Enemy_30pts;
    public int num30Enemies = 10;

    private float totalEnemies = 0f;
    private float enemiesRemaining = 0f;
    public Transform levelRoot;

    private bool enemiesSpawned = false;

    public Transform parent;
    private float accumulatedTime = 0f;
    private float totalTime = 0f;

    private float currentX = 0f;
    private float currentY = 0f;
    
    public Text guiText;

    private int currentScore;

    private int highScore;

    private bool gameStarted = false;
    
    private bool movingRight = true;
    
    private GameObject displayMysteryEnemy;
    private GameObject display_10_Enemy;
    private GameObject display_20_Enemy;
    private GameObject display_30_Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get highScore value from Player Preferences at beginning of game
        highScore = PlayerPrefs.GetInt("HighScore");
        // Spawn enemies for displaying next to points
        displayMysteryEnemy= Instantiate(MysteryEnemy, new Vector3(-8f, 6f, 0f), levelRoot.rotation);
        display_10_Enemy= Instantiate(Enemy_10pts, new Vector3(-8f, 3.5f, 0f), levelRoot.rotation);
        display_20_Enemy= Instantiate(Enemy_20pts, new Vector3(-8f, 1f, 0f), levelRoot.rotation);
        display_30_Enemy= Instantiate(Enemy_30pts, new Vector3(-8f, -2f, 0f), levelRoot.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get highScore value from Player Preferences constantly throughout the game
        highScore = PlayerPrefs.GetInt("HighScore");
        
        // When Space is pressed Change Start Screen UI to score keeping UI
        if (Input.GetKey(KeyCode.Space))
        {
            guiText.text = "\t\tSCORE\t\t\t\t\tHI-SCORE\n\t\t" +
                           currentScore.ToString("D4") + "\t\t\t\t\t\t" + highScore.ToString("D4") +
                           "\n\n\n\n\n\n\n";
            gameStarted = true;

        }

        // If the game has started continuously update score
        if (gameStarted)
        {
            guiText.text = "\t\tSCORE\t\t\t\t\tHI-SCORE\n\t\t" +
                           currentScore.ToString("D4") + "\t\t\t\t\t\t" + highScore.ToString("D4") +
                           "\n\n\n\n\n\n\n";
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            resetHighScore();
            guiText.text = "\t\tSCORE\t\t\t\t\tHI-SCORE\n\t\t" +
                           currentScore.ToString("D4") + "\t\t\t\t\t\t" + highScore.ToString("D4") +
                           "\n\n\n\n\n\n\n";

        }

        if (gameStarted)
        {
            if (!enemiesSpawned)
            {
                Destroy(displayMysteryEnemy.gameObject);
                Destroy(display_10_Enemy.gameObject);
                Destroy(display_20_Enemy.gameObject);
                Destroy(display_30_Enemy.gameObject);
                // Spawn Mystery Enemy
                var spawnedMysteryEnemy = Instantiate(MysteryEnemy, new Vector3(0f, 15f, 0f), levelRoot.rotation);
                // Subscribe to the OnWallCollide Method with OnWallCollide Event
                FindObjectOfType<Enemy>().wallCollideEvent += OnWallCollide;
                FindObjectOfType<Enemy>().enemy_mystery_DestroyedEvent += OnEnemy_mystery_Destroyed;
        
                // Spawn 2 rows of 10 point enemies and 2 rows of 20 point enemies
                for (int i = 0; i < 10; i++) // Loop 10 times for 5 rows and 5 spaces between rows
                {
                    // Start counter at negative value 1 less than total number of enemies (10) so 9 enemies are spawned for symetry
                    // Continue to number of enemies*2 for room for spaces between enemies
                    for (int j = -(num10Enemies - 1); j < num10Enemies * 2 - num10Enemies; j++)
                    {
                        // Skip every other j for space between rows
                        if (i % 2 == 0)
                        { 
                            // Skip every other i for space in between enemies
                            if (j % 2 == 0)
                            {
                                // If on the first 2 rows spawn 10 point enemy
                                if (i < 4)
                                {
                                    var spawnedEnemy = Instantiate(Enemy_10pts, new Vector3(j, i, 0f), levelRoot.rotation);
                                    spawnedEnemy.transform.SetParent(parent);
                                    // Subscribe to the OnWallCollide Method with OnWallCollide Event
                                    FindObjectOfType<Enemy>().wallCollideEvent += OnWallCollide;
                                    FindObjectOfType<Enemy>().enemy_10_DestroyedEvent += OnEnemy_10_Destroyed;
                                    // Increase Total Enemies and Enemies Remaining
                                    totalEnemies++;
                                    enemiesRemaining++;
                                }
                                // If on the second 2 rows spawn 20 point enemy
                                else if (i >= 4 && i < 8)
                                {
                                    var spawnedEnemy = Instantiate(Enemy_20pts, new Vector3(j, i, 0f), levelRoot.rotation);
                                    spawnedEnemy.transform.SetParent(parent);
                                    // Subscribe to the OnWallCollide Method with OnWallCollide Event
                                    FindObjectOfType<Enemy>().wallCollideEvent += OnWallCollide;
                                    FindObjectOfType<Enemy>().enemy_20_DestroyedEvent += OnEnemy_20_Destroyed;
                                    totalEnemies++;
                                    enemiesRemaining++;
                                }
                                // If on last row spawn 30 point enemy
                                else
                                {
                                    var spawnedEnemy = Instantiate(Enemy_30pts, new Vector3(j, i, 0f), levelRoot.rotation);
                                    spawnedEnemy.transform.SetParent(parent);
                                    // Subscribe to the OnWallCollide Method with OnWallCollide Event
                                    FindObjectOfType<Enemy>().wallCollideEvent += OnWallCollide;
                                    FindObjectOfType<Enemy>().enemy_30_DestroyedEvent += OnEnemy_30_Destroyed;
                                    totalEnemies++;
                                    enemiesRemaining++;
                                }
                            }
                        }
                    }
                }
                enemiesSpawned = true;
            }
            // Enemies Move Towards Player Section
            currentY = this.transform.position.y;
            // Step Right until first enemy hits Right edge (USE DELEGATE FUNCTION)
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime > (enemiesRemaining / totalEnemies))
            {
                totalTime += 1f;
                accumulatedTime = 0f;
                // Get current x position
                currentX = this.transform.position.x;
                // Update to current x position + 1

                // BOOLEAN CONDITION
                if (movingRight)
                {
                    this.transform.position = new Vector3((currentX + 1), currentY, 0f);
                }
                else
                {
                    this.transform.position = new Vector3((currentX - 1), currentY, 0f);
                }
            }
        }
        // Step Down 
        // Turn boolean
        // Step Left until first enemy hits Left edge (USE DELEGATE FUNCTION)
        // Step Down
        // Turn boolean
        
        // Enemies Speed up as more are destroyed
        // Listen for total number of enemies (USE DELEGATE)
        // Every time enemies decreased by 1 reduce time update interval by fraction equal to (number of enemies remaining/starting number of enemies)
        // instead of (accumulatedTime > 1f) take it down incrementally to (accumulatedTime > .01f)
    }

    public void resetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public void OnWallCollide()
    {
        //Move down 1 space
        this.transform.position = new Vector3(currentX,(currentY-1),0f);
        Debug.Log("Wall Collide Delegate Heard in EnemyMovement Class!");
        // Toggle Direction Between right and left
        if (movingRight == true)
        {
            movingRight = false;
        }
        else
        {
            movingRight = true;
        }
    }

    public void OnEnemy_mystery_Destroyed()
    {
        Debug.Log("Mystery Enemy Destroyed Delegate Heard in EnemyMovement Class!");
        currentScore += 100;
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
    public void OnEnemy_10_Destroyed()
    {
        Debug.Log("Enemy 10 Destroyed Delegate Heard in EnemyMovement Class!");
        enemiesRemaining -= .5f;
        currentScore += 10;
        Debug.Log("Enemies Remaining: " + enemiesRemaining);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
    public void OnEnemy_20_Destroyed()
    {
        Debug.Log("Enemy 20 Destroyed Delegate Heard in EnemyMovement Class!");
        enemiesRemaining -= .5f;
        currentScore += 20;
        Debug.Log("Enemies Remaining: " + enemiesRemaining);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
    public void OnEnemy_30_Destroyed()
    {
        Debug.Log("Enemy 30 Destroyed Delegate Heard in EnemyMovement Class!");
        enemiesRemaining -= .5f;
        currentScore += 30;
        Debug.Log("Enemies Remaining: " + enemiesRemaining);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
}
