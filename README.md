# Dokumentace k funkcionalitě programu Boulder Dash

Tento program je implementací klasické hry Boulder Dash v jazyce C# s využitím technologie WPF (Windows Presentation Foundation). Hra obsahuje následující prvky a funkcionality:

## Hlavní Třídy a Metody

### `GameElement.cs`
- **GameElement**: Abstraktní třída, kterou dědí všechny herní prvky. Obsahuje:
  - `int X`: Horizontální souřadnice prvku.
  - `int Y`: Vertikální souřadnice prvku.
  - `abstract void Update(Game game)`: Abstraktní metoda, kterou implementují všechny dědící třídy pro aktualizaci stavu prvku.

### `Player.cs`
- **Player**: Třída reprezentující hráče. Dědí z `GameElement`.
  - `void Move(int dx, int dy, Game game)`: Pohybuje hráčem o zadaný počet jednotek v osách X a Y. Kontroluje kolize s jinými prvky a upravuje jejich stav.
  - `bool CanMoveTo(int x, int y, Game game)`: Kontroluje, zda je možné se přesunout na zadané souřadnice.
  - `void RemoveSandIfPresent(int x, int y, Game game)`: Odstraní písek z daných souřadnic, pokud je tam přítomen.

### `Rock.cs`
- **Rock**: Třída reprezentující kámen. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizuje stav kamene každým tikem hry. Volá metodu `Fall`.
  - `void Fall(Game game)`: Řídí pád kamene a kolize s hráčem.
  - `bool CanRollLeft(Game game)`, `bool CanRollRight(Game game)`: Kontrolují, zda se kámen může převalit doleva nebo doprava.
  - `void MoveLeft(int belowY, Game game)`, `void MoveRight(int belowY, Game game)`: Přesouvají kámen vlevo nebo vpravo.
  - `bool IsValidMove(int x, int y, Game game)`, `bool IsEmptySpot(int x, int y, Game game)`, `bool IsPlayerAtPosition(int x, int y, Game game)`, `bool IsRockUnder(int x, int y, Game game)`: Pomocné metody pro kontrolu platnosti pohybu a kolizí.

### `Monster.cs`
- **Monster**: Třída reprezentující monstrum. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizuje stav monstra každým tikem hry. Volá metodu `MoveTowardsPlayer`.
  - `void MoveTowardsPlayer(Game game)`: Pohybuje monstrem směrem k hráči a řídí kolize s hráčem.
  - `bool isBorderBlock(int x, int y, Game game)`, `bool IsValidMove(int x, int y, Game game)`: Pomocné metody pro kontrolu pohybu a kolizí.
  - `Directions direction`: Enum, který určuje směr pohybu monstra (Up, Down, Right, Left).

### `Diamond.cs`
- **Diamond**: Třída reprezentující diamant. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizace stavu diamantu (diamanty nepadají).

### `Wall.cs`
- **Wall**: Třída reprezentující stěnu. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizace stavu stěny (stěny jsou statické).

### `Sand.cs`
- **Sand**: Třída reprezentující písek. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizace stavu písku (písek je statický).

### `EmptySpot.cs`
- **EmptySpot**: Třída reprezentující prázdné místo. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizace stavu prázdného místa (statické).

### `Exit.cs`
- **Exit**: Třída reprezentující výstup. Dědí z `GameElement`.
  - `void Update(Game game)`: Aktualizace stavu výstupu (logika pro dokončení úrovně).

### `Game.cs`
- **Game**: Třída řídící logiku hry.
  - `List<GameElement> Elements`: Seznam všech herních prvků na hrací ploše.
  - `DispatcherTimer timer`: Časovač pro řízení herní smyčky.
  - `Player Player`: Odkaz na objekt hráče.
  - `int Width`, `int Height`: Šířka a výška hrací plochy.
  - `int Score`: Skóre hráče.
  - `void Start()`: Spuštění hry.
  - `void RestartLevel()`: Restartování úrovně načtením mapy z disku.
  - `void CompleteLevel()`, `void PlayerDied()`: Metody pro zobrazení dialogu při dokončení úrovně nebo smrti hráče.
  - `void ShowEndDialog(string message)`: Zobrazení dialogového okna s možnostmi pro restart nebo ukončení hry.
  - `void OnTick(object sender, EventArgs e)`: Aktualizace stavu hry každým tikem časovače.
  - `void IncreaseScore(int points)`: Zvýšení skóre.
  - `void UpdateElementUI(GameElement oldElement, GameElement newElement)`: Aktualizace UI při změně herního prvku.
  - `event Action<GameElement, GameElement> OnElementChanged`, `event Action OnTickEvent`, `event Action OnGameRestarted`: Události pro změnu prvku, tik časovače a restart hry.

### `MainWindow.xaml.cs`
- **MainWindow**: Hlavní okno aplikace.
  - `Game game`: Instance hry.
  - `Dictionary<GameElement, Rectangle> elementToUI`: Mapa herních prvků na UI prvky.
  - `void InitializeGameCanvas()`: Inicializace herního plátna.
  - `void AddElementToUI(GameElement element)`, `void RemoveElementFromUI(GameElement element)`: Přidání a odstranění herních prvků z UI.
  - `Brush GetBrushForElement(GameElement element)`: Získání správného vzhledu pro herní prvek.
  - `void UpdateGameCanvas()`: Aktualizace herního plátna.
  - `void OnElementChanged(GameElement oldElement, GameElement newElement)`: Aktualizace UI při změně prvku.
  - `void Window_KeyDown(object sender, KeyEventArgs e)`: Řízení pohybu hráče pomocí klávesnice.

### `MapParser.cs`
- **MapParser**: Třída pro načítání mapy z textového souboru.
  - `List<GameElement> ParseMap(string filePath, Game game)`: Načítá mapu ze zadaného souboru a inicializuje herní prvky.
