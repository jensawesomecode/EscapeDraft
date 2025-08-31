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

Minimal “win/lose” hooks

Wherever you detect success in the Room (e.g., the door’s unlock script):

FindObjectOfType<GameManager>()?.WinGame();


If you want to raise events instead (decoupled), you can do:

EventBus.GameWon();


…and subscribe in GameManager to load the Win scene (similar to time-expired).

UNITY SETTINGS
🎮 1. Open Build Settings

Menu: File → Build Settings…

Select PC, Mac & Linux Standalone.

Target Platform: Windows.

Architecture: x86_64 (64-bit).

Click Add Open Scenes so your Start scene is included. (Make sure all 4 scenes are added in order: Start, Room, Win, Lose.)

🖼️ 2. Set Default Resolution & Fullscreen

Menu: Edit → Project Settings → Player.

Under Resolution and Presentation (you’ll see a section for Standalone):

Default Screen Width/Height:

A safe choice for a 2D point-and-click game is 1920 × 1080 (Full HD).

If you want something lighter, 1280 × 720 is fine too.

Fullscreen Mode:

Set to Windowed while developing (easier to debug).

For release, you can switch to Fullscreen Window or Exclusive Fullscreen depending on preference.

Resizable Window: On or off depending if you want players to resize.

Run In Background: Off (unless you want the game to keep running when tabbed out).

🖌️ 3. Configure the Canvas (UI scaling)

For your Start screen UI (and any other Canvas):

Select the Canvas in the Hierarchy.

In the Canvas Scaler component:

UI Scale Mode: Scale With Screen Size.

Reference Resolution:

Match your chosen default resolution (e.g., 1920 × 1080).

Screen Match Mode: Match Width Or Height.

Match slider: 0.5 (balances width & height).

This ensures UI elements scale properly on different resolutions.

🔊 4. Audio Settings

In Project Settings → Audio:

Leave defaults for now.

You’ll route your music and SFX through your managers anyway.

If you add a Mixer, you’ll expose master/music/SFX volumes here.

⏱️ 5. Time Settings (for your 5-minute timer)

In Project Settings → Time:

Fixed Timestep: 0.02 (50 FPS physics).

Maximum Allowed Timestep: 0.333.

You don’t need to tweak this unless you’re doing physics-heavy stuff. Your timer runs in real time (WaitForSeconds) so it’s unaffected.

⚙️ 6. Quality Settings

In Project Settings → Quality:

Since you’re making a 2D point-and-click, you don’t need fancy graphics.

You can keep Medium or High as default.

Turn off unnecessary post-processing features to keep builds lightweight.

📦 7. Build

Back to File → Build Settings…

Click Build and Run.

Unity will spit out an .exe + *_Data folder.

Double-click the .exe to test like a normal Windows game.

TL;DR Recommended Baseline

Resolution: 1920 × 1080, Windowed while deving.

Canvas Scaler: Scale With Screen Size, Reference 1920 × 1080.

Scenes: Start, Room, Win, Lose all added in Build Settings.

Build target: Windows 64-bit.

