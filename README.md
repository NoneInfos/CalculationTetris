Game Architecture
├── Core System
│   ├── SingletonClass - 싱글톤 패턴 기본 구현
│   ├── ManagerBase - 모든 매니저의 기본 클래스
│   ├── ControllerBase - 모든 컨트롤러의 기본 클래스
│   ├── Scene - 씬 기본 클래스
│   └── IGEngine - 게임 엔진 진입점
│
├── Managers (게임 시스템 전체 관리)
│   ├── IGGameManager - 전체 게임 흐름 관리
│   ├── IGBoardManager - 게임 보드 관리
│   ├── IGBlockManager - 블록 생성 및 관리
│   ├── GameStateManager - 게임 상태 관리 (메인메뉴/플레이/일시정지/게임오버)
│   ├── ScoreManager - 점수 시스템 관리
│   ├── AudioManager - 게임 오디오 관리
│   ├── PoolManager - 오브젝트 풀링 관리
│   ├── ThemeManager - 게임 테마 관리
│   ├── SettingsManager - 게임 설정 관리
│   ├── SaveManager - 게임 데이터 저장 관리
│   ├── GameStatsManager - 게임 통계 관리
│   ├── AchievementManager - 업적 시스템 관리
│   ├── DailyChallengeManager - 일일 도전과제 관리
│   ├── DifficultyManager - 게임 난이도 조절 관리
│   └── LeaderboardManager - 리더보드 관리
│
├── Controllers (게임 로직 처리)
│   ├── IGGameController - 게임 전체 컨트롤 담당
│   ├── IGBoardController - 게임 보드 동작 제어
│   ├── IGBlockController - 블록 동작 제어
│   └── IGInputController - 사용자 입력 처리
│
├── Models (게임 데이터 및 상태)
│   ├── IGObject - 게임 오브젝트 기본 클래스
│   ├── IGBoard - 게임 보드 모델
│   ├── IGBlock - 블록 모델
│   ├── IGTile - 타일 기본 클래스
│   ├── IGBoardTile - 보드 타일 모델
│   └── IGBlockTile - 블록 타일 모델
│
├── Views (시각적 표현)
│   ├── IGBoardView - 게임 보드 시각화
│   └── IGBlockView - 블록 시각화
│
├── UI System
│   ├── UIManager - UI 전체 관리
│   ├── SettingsMenuUI - 설정 메뉴 UI
│   ├── LeaderboardUI - 리더보드 UI
│   └── AchievementNotificationUI - 업적 알림 UI
│
└── Data
    ├── IGConfig - 게임 설정 상수
    ├── BlockShape - 블록 모양 데이터
    └── IGObjectData - 게임 오브젝트 데이터
