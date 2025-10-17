using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageType
{
    Debug,      // 디버그 씬 (개발용)
    Intro,      // 인트로
    Main,       // 메인 화면
    Roguelike,  // 로그라이크
    Shooting,   // 슈팅
    DeckStrategy, // 덱 전략
    ExtractionShooter, // 익스트랙션 슈터
    Production  // 생산/건설/강화
}

[Serializable]
public class StageTransition
{
    public StageType start;
    public StageType end;
}

public class StageManager : Singleton<StageManager>
{
    [Header("Fade Settings")]
    private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 1f;
    
    [SerializeField] private StageType currentStage;

    private BaseStage currentStageInstance;
    private bool isTransitioning = false;

    // Transition 검증용 2차원 배열
    private bool[,] transitionTable;

    // StageType → Scene 이름 매핑
    private Dictionary<StageType, string> sceneNameMap = new Dictionary<StageType, string>
    {
        { StageType.Debug, "DebugScene" },
        { StageType.Intro, "IntroScene" },
        { StageType.Main, "MainScene" },
        { StageType.Roguelike, "RoguelikeScene" },
        { StageType.Shooting, "ShootingScene" },
        { StageType.DeckStrategy, "DeckStrategyScene" },
        { StageType.ExtractionShooter, "ExtractionShooterScene" },
        { StageType.Production, "ProductionScene" }
    };

    protected override void Awake()
    {
        base.Awake();

        InitializeTransitionTable();
        InitializeFadeCanvas();
    }

    private void InitializeFadeCanvas()
    {
        if (!fadeCanvas)
        {
            GameObject fadeObj = GameObject.Find("FadeCanvas");
            if (!fadeObj)
            {
                fadeObj = new GameObject("FadeCanvas");

                Canvas canvas = fadeObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 999; // 최상단에 렌더링

                // CanvasScaler 추가
                UnityEngine.UI.CanvasScaler scaler = fadeObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);

                // GraphicRaycaster 추가
                fadeObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                fadeCanvas = fadeObj.AddComponent<CanvasGroup>();

                GameObject fadeImage = new GameObject("FadeImage");
                fadeImage.layer = LayerMask.NameToLayer("UI"); // UI Layer 설정
                fadeImage.transform.SetParent(fadeObj.transform, false);

                UnityEngine.UI.Image image = fadeImage.AddComponent<UnityEngine.UI.Image>();
                image.color = Color.black;
                image.raycastTarget = false; // Raycast 불필요

                // RectTransform 설정 (전체 화면)
                RectTransform rectTransform = fadeImage.GetComponent<RectTransform>();
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.anchoredPosition = Vector2.zero;

                // DontDestroyOnLoad 설정
                DontDestroyOnLoad(fadeObj);

                Debug.Log("FadeCanvas created and set to DontDestroyOnLoad");
            }
            else
            {
                fadeCanvas = fadeObj.GetComponent<CanvasGroup>();
                Debug.Log("FadeCanvas found in scene");
            }
        }

        // 초기 상태: 투명하게 설정 (게임 시작 시 검은 화면이 보이지 않도록)
        if (fadeCanvas)
        {
            fadeCanvas.alpha = 0f;
            fadeCanvas.blocksRaycasts = false;
        }
    }

    // Transition 테이블 초기화
    private void InitializeTransitionTable()
    {
        int stageCount = System.Enum.GetValues(typeof(StageType)).Length;
        transitionTable = new bool[stageCount, stageCount];

        SetTransition(StageType.Debug, StageType.Intro);
        SetTransition(StageType.Debug, StageType.Main);
        SetTransition(StageType.Debug, StageType.Roguelike);
        SetTransition(StageType.Debug, StageType.Shooting);
        SetTransition(StageType.Debug, StageType.DeckStrategy);
        SetTransition(StageType.Debug, StageType.ExtractionShooter);
        SetTransition(StageType.Debug, StageType.Production);

        SetTransition(StageType.Intro, StageType.Main);

        SetTransition(StageType.Main, StageType.Roguelike);
        SetTransition(StageType.Main, StageType.Shooting);
        SetTransition(StageType.Main, StageType.DeckStrategy);
        SetTransition(StageType.Main, StageType.ExtractionShooter);
        SetTransition(StageType.Main, StageType.Production);

        SetTransition(StageType.Roguelike, StageType.Main);
        SetTransition(StageType.Shooting, StageType.Main);
        SetTransition(StageType.DeckStrategy, StageType.Main);
        SetTransition(StageType.ExtractionShooter, StageType.Main);
        SetTransition(StageType.Production, StageType.Main);
    }

    private void SetTransition(StageType from, StageType to)
    {
        transitionTable[(int)from, (int)to] = true;
    }

    // Stage 전환 
    public void LoadStage(StageType targetStage)
    {
        if (isTransitioning)
        {
            Debug.LogWarning("Already transitioning!");
            return;
        }

        // 1. Transition 검증
        if (!IsValidTransition(currentStage, targetStage))
        {
            Debug.LogError($"Invalid transition: {currentStage} → {targetStage}");
            return;
        }

        // 2. 전환 시작
        StartCoroutine(TransitionCoroutine(targetStage));
    }

    // Transition 검사
    private bool IsValidTransition(StageType from, StageType to)
    {
        return transitionTable[(int)from, (int)to];
    }

    /// Stage 전환 Coroutine
    private IEnumerator TransitionCoroutine(StageType targetStage)
    {
        isTransitioning = true;

        // Stage Exit 처리
        if (currentStageInstance)   
            currentStageInstance.OnExit();

        // FadeOut은 항상 실행
        yield return StartCoroutine(FadeOut());

        // 4. Scene 로드
        string sceneName = sceneNameMap.ContainsKey(targetStage)
            ? sceneNameMap[targetStage]
            : targetStage.ToString();

        // 씬매니저에 씬이 존재하는지 확인 - 빌드 세팅
        if (SceneManager.GetSceneByName(sceneName).IsValid() == false &&
            SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings! Add it to File → Build Settings → Scenes In Build");
            isTransitioning = false;
            yield break;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        if (asyncLoad == null)
        {
            Debug.LogError($"Failed to load scene '{sceneName}'");
            isTransitioning = false;
            yield break;
        }

        while (!asyncLoad.isDone)
        {
            // 로딩 진행도 표시 가능
            float progress = asyncLoad.progress;
            yield return null;
        }

        currentStageInstance = FindFirstObjectByType<BaseStage>();

        if (currentStageInstance)
            currentStageInstance.OnEnter();

        yield return StartCoroutine(FadeIn());

        currentStage = targetStage;
        isTransitioning = false;
    }

    private IEnumerator FadeOut()
    {
        if (!fadeCanvas)
        {
            Debug.LogError("FadeCanvas is null! This should not happen.");
            yield break;
        }

        Debug.Log($"FadeOut Start - FadeCanvas: {fadeCanvas.name}, Alpha: {fadeCanvas.alpha}, Active: {fadeCanvas.gameObject.activeSelf}");
        fadeCanvas.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 1f;
        Debug.Log($"FadeOut End - Alpha: {fadeCanvas.alpha}");
    }

    private IEnumerator FadeIn()
    {
        if (!fadeCanvas)
        {
            Debug.LogError("FadeCanvas is null! This should not happen.");
            yield break;
        }

        Debug.Log($"FadeIn Start - Alpha: {fadeCanvas.alpha}");
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 0f;
        fadeCanvas.blocksRaycasts = false;
        Debug.Log($"FadeIn End - Alpha: {fadeCanvas.alpha}, Active: {fadeCanvas.gameObject.activeSelf}");
    }
}
