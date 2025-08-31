this is going to be an escape game. So fun.

Assets/
  Scripts/
    Core/
      GameManager.cs
      TimerManager.cs
      EventBus.cs
      SceneNames.cs
    Audio/
      MusicManager.cs
      SfxManager.cs
    UI/
      StartSceneController.cs
      CountdownUI.cs
  Art/
    UI/
      StartBackground.png
  Audio/
    Music/
    SFX/
  Scenes/
    Start.unity
    Room.unity
    Win.unity
    Lose.unity
  Prefabs/
    CoreManagers.prefab


Scenes and flow

Start → shows background, title “Escape The Room”, green Start button → GameManager.StartGame() → loads Room.

Room → gameplay; when escape condition met, call GameManager.WinGame().
If timer expires, EventBus.OnTimeExpired → GameManager.LoseGame().

Win/Lose → simple screens with buttons to Restart (GameManager.GoToStart()).

All managers live in one prefab CoreManagers (with GameManager, TimerManager, MusicManager, SfxManager). It’s spawned once and marked DontDestroyOnLoad.

Wiring order (what to connect, when)

Create CoreManagers prefab

Empty GO “CoreManagers”

Add: GameManager, TimerManager, MusicManager, SfxManager

Add 2 child GOs each with an AudioSource:

“MusicSource” (loop = on) → assign on MusicManager

“SfxSource” (loop = off) → assign on SfxManager

Save as Prefabs/CoreManagers.prefab.

Ensure CoreManagers exists at runtime

Put the prefab in the Start scene hierarchy.

GameManager will DontDestroyOnLoad it so it persists across scenes.

Start scene UI

Canvas → RawImage/Image = StartBackground.png (Stretch full-screen).

TextMeshPro title: “Escape The Room”.

Use TMP default font for now; later swap in a funhouse-creepy font (e.g., Creepster, Rubik Wet Paint) by importing a TTF and creating a TMP Font Asset.

Button “Start” (green, e.g., #2ECC71) → OnClick → StartSceneController.OnClickStart() → which calls GameManager.StartGame().

Timer display

In Room scene, add a TMP text GO with CountdownUI attached, listening to timer ticks.

Room win condition

Your puzzle/exit controller calls GameManager.WinGame() when solved.

Win/Lose scenes

Buttons wired to GameManager.GoToStart().