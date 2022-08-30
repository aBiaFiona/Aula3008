using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe usada para gerenciar o jogo
/// </summary>
public class GameManager : MonoBehaviour
{

     public static GameManager Instance; // instancia do singleton
     
    [SerializeField] 
    private string guiName; // nome da fase de interface

    [SerializeField] 
    private string levelName; // nome da fase de jogo

    [SerializeField] 
    private GameObject playerAndCameraPrefab; // referencia pro prefab do jogador + camera

    private void Awake()
    {
        // Condição de criação do singleton
        if (Instance == null)
        {
            Instance = this;
            
            // Impede que o objeto indicado entre parenteses seja destruido
            DontDestroyOnLoad(this.gameObject); // referencia pro objeto que contem o GameManager
        }
        else Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    { 
        // se estiver inicializado a cena da partir de initialization,  carregue o jogo do jeito antes
        if(SceneManager.GetActiveScene().name == "Initialization")
            StarGameFromInitialization();
        else // caso contratio, rdta iniciando a parir do level, carregue o jogo de modo apropriado 
            StartGameFromLevel();
    }

    private void StartGameFromLevel()
    {
        //1 - Carrega a cena de interface de modo aditivo para nao apagar a cena do level da memoria ram
        SceneManager.LoadScene(guiName, LoadSceneMode.Additive);
        
        // 2 - Precisa instanciar o jogador na cena
        // começa procurando o objeto PlayerStart na cena do level
        Vector3 playerStartPosition = GameObject.Find("PlayerStart").transform.position;
    
        // instancia o prefab do jogador na posicao do player start com rotação zerada
        Instantiate(playerAndCameraPrefab, playerStartPosition, Quaternion.identity);
    }

    private void StarGameFromInitialization()
    {
       
        // 1 - Carregar a cena de interface e do jogo
        SceneManager.LoadScene(guiName);
        //SceneManager.LoadScene(levelName, LoadSceneMode.Additive); // Additive carrega uma nova cena sem descarregar a anterior

        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive).completed += operation =>
        {
            // Inicializa a variavel para guardar a cena do level com o valor padrao (default)
            Scene levelScene = default;
            
            // encontrar a cena de level que está carregada
            // for que itera no array de cenas abertas
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                // se o nome da cena na posição i do array for igual ao nome do level
                if (SceneManager.GetSceneAt(i).name == levelName)
                {
                    // associe a cena na posição i do array à variável 
                    levelScene = SceneManager.GetSceneAt(i);
                    break;
                }
            }
            // se a variável tiver um valor diferente do padrão, significa que ela foi alterada
            // e a cena do level atual foi encontrada no array, entao faça ela ser a
            // nova cena ativa
            if(levelScene != default) SceneManager.SetActiveScene(levelScene);
            
            // 2 - Precisa instanciar o jogador na cena
            // começa procurando o objeto PlayerStart na cena do level
            Vector3 playerStartPosition = GameObject.Find("PlayerStart").transform.position;
    
            // instancia o prefab do jogador na posicao do player start com rotação zerada
            Instantiate(playerAndCameraPrefab, playerStartPosition, Quaternion.identity);
            
            // 3 - Começar a partida
        }


    }

    private void StarGameFromInitialization()
    {
        SceneManager.LoadScene("Splash");
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
