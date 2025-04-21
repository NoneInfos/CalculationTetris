## 📝 개요
기존의 서비스되고 있는 Blockudoku 모바일 게임 배경에 산수를 추가해 점수 계산방식을 변경하여 추가 개발할 게임입니다<br>
이 프로젝트는 MVC(Model-View-Controller) 패턴과 Manager 시스템을 결합한 블록 퍼즐 게임입니다. <br>
확장성과 유지보수성을 고려한 구조로 설계되었으며, 다양한 기능을 모듈화하여 관리합니다.
### 주요 기능

블록 배치 및 회전 시스템
점수 및 하이스코어 기록
테마 변경 기능
일일 도전과제
업적 시스템
리더보드

<br><br><br>


## 🏗️ 아키텍처
게임은 크게 다음과 같은 구조로 설계되었습니다:

- Model: 게임 데이터와 상태 관리 (IGBoard, IGBlock 등)
- View: 시각적 표현 담당 (IGBoardView, IGBlockView 등)
- Controller: 게임 로직 처리 (IGGameController, IGBoardController 등)
- Manager: 게임 시스템 전체 관리 (ScoreManager, AudioManager 등)


 <br><br><br>
## 🧩 시스템 구조
**Core System**
<pre>
Core System
├── SingletonClass - 싱글톤 패턴 기본 구현
├── ManagerBase - 모든 매니저의 기본 클래스
├── ControllerBase - 모든 컨트롤러의 기본 클래스
├── Scene - 씬 기본 클래스
└── IGEngine - 게임 엔진 진입점
</pre>

**Managers**
<pre>Managers (게임 시스템 전체 관리)
├── IGGameManager - 전체 게임 흐름 관리
├── IGBoardManager - 게임 보드 관리
├── IGBlockManager - 블록 생성 및 관리
├── GameStateManager - 게임 상태 관리
├── ScoreManager - 점수 시스템 관리
├── AudioManager - 게임 오디오 관리
├── PoolManager - 오브젝트 풀링 관리
├── ThemeManager - 게임 테마 관리
├── SettingsManager - 게임 설정 관리
├── SaveManager - 게임 데이터 저장 관리
├── GameStatsManager - 게임 통계 관리
├── AchievementManager - 업적 시스템 관리
├── DailyChallengeManager - 일일 도전과제 관리
├── DifficultyManager - 게임 난이도 조절 관리
└── LeaderboardManager - 리더보드 관리
</pre>
**Controllers**
<pre>
Controllers (게임 로직 처리)
├── IGGameController - 게임 전체 컨트롤 담당
├── IGBoardController - 게임 보드 동작 제어
├── IGBlockController - 블록 동작 제어
└── IGInputController - 사용자 입력 처리
</pre>
**Models**
<pre>
Models (게임 데이터 및 상태)
├── IGObject - 게임 오브젝트 기본 클래스
├── IGBoard - 게임 보드 모델
├── IGBlock - 블록 모델
├── IGTile - 타일 기본 클래스
├── IGBoardTile - 보드 타일 모델
└── IGBlockTile - 블록 타일 모델
</pre>
**Views**
<pre>
Views (시각적 표현)
├── IGBoardView - 게임 보드 시각화
└── IGBlockView - 블록 시각화
</pre>
**UI System**
<pre>
UI System
├── UIManager - UI 전체 관리
├── SettingsMenuUI - 설정 메뉴 UI
├── LeaderboardUI - 리더보드 UI
└── AchievementNotificationUI - 업적 알림 UI
</pre>
**Data**
<pre>
Data
├── IGConfig - 게임 설정 상수
├── BlockShape - 블록 모양 데이터
└── IGObjectData - 게임 오브젝트 데이터
</pre>

<br><br><br>

## 🔍 주요 컴포넌트
**SingletonClass<T>**
모든 매니저 클래스의 기본이 되는 싱글톤 패턴 구현:

	public class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour

    private static T _instance;

    public static T Instance 
    {
        get 
        {
            if(_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }

            if (_instance == null)
            {
                var singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                _instance.gameObject.name = typeof(T).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
            }

            return _instance;
        }
    }
    
    // ... 추가 코드 ...



**ManagerBase<T>**
모든 매니저의 기본 인터페이스를 정의:

	public abstract class ManagerBase<T> : SingletonClass<T> where T : ManagerBase<T>
	{
	    public abstract void InitializeManager();
	    public abstract void ClearManager();
	    public abstract void FinalizeManager();
	}

<br><br><br>

## 💻 디자인 패턴
이 프로젝트에서 사용된 주요 디자인 패턴:

- **MVC 패턴**: 데이터(Model), 표현(View), 로직(Controller) 분리
- **싱글톤 패턴**: 관리자 클래스에 전역 접근점 제공
- **오브젝트 풀링**: 게임 오브젝트 재사용으로 성능 최적화
- **옵저버 패턴**: 이벤트 기반 통신으로 컴포넌트 간 결합도 감소
- **상태 패턴**: 게임 상태 관리 및 전환

<br><br><br>


### 추가 예정
