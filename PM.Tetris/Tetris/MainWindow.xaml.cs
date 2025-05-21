using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading; // Für DispatcherTimer

namespace WpfTetris
{
    public partial class MainWindow : Window
    {
        private const int BlockSize = 25; // Größe eines einzelnen Blocks in Pixel
        private const int BoardWidth = 10; // Breite des Spielfelds in Blöcken
        private const int BoardHeight = 20; // Höhe des Spielfelds in Blöcken

        private DispatcherTimer gameTimer;
        private Tetromino currentTetromino;
        private Tetromino nextTetromino;
        private Point currentPosition; // Position des aktuellen Tetrominos (linke obere Ecke des Begrenzungsrahmens)
        private SolidColorBrush[,] landedBlocks; // Speichert die Farben der gelandeten Blöcke

        private int score;
        private int level;
        private int linesCleared;
        private bool gameRunning;

        private Random random = new Random();

        // Farben für die Tetrominoes
        private readonly SolidColorBrush[] tetrominoColors = new SolidColorBrush[]
        {
            Brushes.Cyan,       // I
            Brushes.Blue,       // J
            Brushes.Orange,     // L
            Brushes.Yellow,     // O
            Brushes.Green,      // S
            Brushes.Purple,     // T
            Brushes.Red         // Z
        };

        public MainWindow()
        {
            InitializeComponent();
            landedBlocks = new SolidColorBrush[BoardWidth, BoardHeight];
            GameCanvas.Width = BoardWidth * BlockSize;
            GameCanvas.Height = BoardHeight * BlockSize;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialisiere das Spiel hier, wenn das Fenster geladen ist,
            // oder warte auf den Start-Button
            // InitializeGame(); // Optional: Direkt starten
        }

        private void InitializeGame()
        {
            GameCanvas.Children.Clear();
            NextPieceCanvas.Children.Clear();
            landedBlocks = new SolidColorBrush[BoardWidth, BoardHeight]; // Spielfeld zurücksetzen

            score = 0;
            level = 1;
            linesCleared = 0;
            UpdateScoreDisplay();
            UpdateLevelDisplay();

            gameRunning = true;
            GameOverText.Visibility = Visibility.Collapsed;
            StartButton.IsEnabled = false; // Deaktiviere Start-Button während des Spiels

            SpawnNextTetromino(); // Ersten "nächsten" Stein erzeugen
            SpawnNewTetromino();  // Ersten spielbaren Stein erzeugen

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimer_Tick;
            SetTimerInterval();
            gameTimer.Start();
        }

        private void SetTimerInterval()
        {
            // Geschwindigkeit erhöht sich mit dem Level
            double intervalSeconds = Math.Max(0.1, 1.0 - (level - 1) * 0.07);
            gameTimer.Interval = TimeSpan.FromSeconds(intervalSeconds);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!gameRunning) return;
            MoveTetromino(0, 1); // Einen Block nach unten bewegen
        }

        private void SpawnNextTetromino()
        {
            nextTetromino = GetRandomTetromino();
            DrawNextPiece();
        }

        private void SpawnNewTetromino()
        {
            currentTetromino = nextTetromino; // Der "nächste" wird zum aktuellen
            SpawnNextTetromino(); // Neuen "nächsten" Stein generieren

            // Startposition in der Mitte oben
            currentPosition = new Point((BoardWidth - currentTetromino.Shape.GetLength(1)) / 2, 0);

            if (!IsValidPosition(currentTetromino, currentPosition))
            {
                // Game Over Bedingung: Neuer Stein kann nicht platziert werden
                GameOver();
                return;
            }
            DrawTetromino(currentTetromino, currentPosition, GameCanvas);
        }

        private Tetromino GetRandomTetromino()
        {
            int type = random.Next(0, 7); // 7 Typen von Tetrominoes
            SolidColorBrush color = tetrominoColors[type];
            return new Tetromino(type, color);
        }

        private void DrawTetromino(Tetromino tetromino, Point position, Canvas targetCanvas, bool clearPrevious = false)
        {
            if (clearPrevious && targetCanvas == GameCanvas) // Nur für das Hauptspielfeld relevant
            {
                // Entferne die vorherige Darstellung des aktuellen Tetrominos
                // Dies ist eine vereinfachte Methode. Effizienter wäre es, die spezifischen Rechtecke zu speichern und zu entfernen.
                // Für dieses Beispiel zeichnen wir das Spielfeld bei jeder Bewegung neu (außer dem fallenden Stein).
                RedrawLandedBlocks();
            }

            for (int y = 0; y < tetromino.Shape.GetLength(0); y++)
            {
                for (int x = 0; x < tetromino.Shape.GetLength(1); x++)
                {
                    if (tetromino.Shape[y, x] == 1)
                    {
                        Rectangle block = new Rectangle
                        {
                            Width = BlockSize,
                            Height = BlockSize,
                            Fill = tetromino.Color,
                            Stroke = Brushes.DimGray, // Rand für bessere Sichtbarkeit
                            StrokeThickness = 0.5
                        };
                        // Wichtig: Position auf dem Canvas ist relativ zum Canvas, nicht zur Grid-Zelle
                        Canvas.SetLeft(block, (position.X + x) * BlockSize);
                        Canvas.SetTop(block, (position.Y + y) * BlockSize);
                        targetCanvas.Children.Add(block);
                    }
                }
            }
        }

        private void DrawNextPiece()
        {
            NextPieceCanvas.Children.Clear();
            if (nextTetromino == null) return;

            // Zentriere den nächsten Stein im Vorschau-Canvas
            double offsetX = (NextPieceCanvas.Width - nextTetromino.Shape.GetLength(1) * BlockSize) / 2;
            double offsetY = (NextPieceCanvas.Height - nextTetromino.Shape.GetLength(0) * BlockSize) / 2;

            for (int y = 0; y < nextTetromino.Shape.GetLength(0); y++)
            {
                for (int x = 0; x < nextTetromino.Shape.GetLength(1); x++)
                {
                    if (nextTetromino.Shape[y, x] == 1)
                    {
                        Rectangle block = new Rectangle
                        {
                            Width = BlockSize,
                            Height = BlockSize,
                            Fill = nextTetromino.Color,
                            Stroke = Brushes.DimGray,
                            StrokeThickness = 0.5
                        };
                        Canvas.SetLeft(block, offsetX + x * BlockSize);
                        Canvas.SetTop(block, offsetY + y * BlockSize);
                        NextPieceCanvas.Children.Add(block);
                    }
                }
            }
        }


        private void RedrawLandedBlocks()
        {
            // Entferne alle Kinder, die nicht zum aktuellen Tetromino gehören könnten (vereinfacht)
            // Eine genauere Methode wäre, nur die gelandeten Blöcke zu entfernen und neu zu zeichnen.
            // Hier entfernen wir alles und zeichnen die gelandeten Blöcke neu.
            GameCanvas.Children.Clear();

            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    if (landedBlocks[x, y] != null)
                    {
                        Rectangle block = new Rectangle
                        {
                            Width = BlockSize,
                            Height = BlockSize,
                            Fill = landedBlocks[x, y],
                            Stroke = Brushes.DimGray,
                            StrokeThickness = 0.5
                        };
                        Canvas.SetLeft(block, x * BlockSize);
                        Canvas.SetTop(block, y * BlockSize);
                        GameCanvas.Children.Add(block);
                    }
                }
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameRunning || currentTetromino == null) return;

            switch (e.Key)
            {
                case Key.Left:
                    MoveTetromino(-1, 0); // Nach links
                    break;
                case Key.Right:
                    MoveTetromino(1, 0);  // Nach rechts
                    break;
                case Key.Down:
                    MoveTetromino(0, 1);  // Nach unten (beschleunigen)
                    // Optional: Punkte für manuelles Beschleunigen
                    // score += 1; 
                    // UpdateScoreDisplay();
                    break;
                case Key.Up: // Oder eine andere Taste für Rotation, z.B. 'R' oder Space
                    RotateTetromino();
                    break;
                case Key.Space: // Hard Drop (sofortiges Fallenlassen)
                    HardDrop();
                    break;
            }
        }

        private void MoveTetromino(int dx, int dy)
        {
            if (currentTetromino == null) return;

            Point newPosition = new Point(currentPosition.X + dx, currentPosition.Y + dy);

            if (IsValidPosition(currentTetromino, newPosition))
            {
                currentPosition = newPosition;
            }
            else if (dy > 0) // Konnte sich nicht nach unten bewegen -> gelandet
            {
                LandTetromino();
                return; // Wichtig, um doppeltes Zeichnen zu vermeiden
            }

            // Neuzeichnen des Spielfelds und des aktuellen Steins
            RedrawLandedBlocks(); // Zuerst die gelandeten Blöcke
            DrawTetromino(currentTetromino, currentPosition, GameCanvas); // Dann den aktuellen Stein darüber
        }

        private void RotateTetromino()
        {
            if (currentTetromino == null) return;

            Tetromino rotated = currentTetromino.GetRotated();
            if (IsValidPosition(rotated, currentPosition))
            {
                currentTetromino = rotated;
            }
            else
            {
                // Versuch, den Stein leicht zu verschieben (Wall Kick - einfache Version)
                // Teste Positionen links und rechts vom Original
                Point[] kickOffsets = { new Point(0, 0), new Point(-1, 0), new Point(1, 0), new Point(-2, 0), new Point(2, 0), new Point(0, -1) /*I-Block*/};
                foreach (var offset in kickOffsets)
                {
                    Point newPos = new Point(currentPosition.X + offset.X, currentPosition.Y + offset.Y);
                    if (IsValidPosition(rotated, newPos))
                    {
                        currentTetromino = rotated;
                        currentPosition = newPos;
                        break;
                    }
                }
            }
            RedrawLandedBlocks();
            DrawTetromino(currentTetromino, currentPosition, GameCanvas);
        }

        private void HardDrop()
        {
            if (currentTetromino == null) return;
            Point testPosition = currentPosition;
            while (IsValidPosition(currentTetromino, new Point(testPosition.X, testPosition.Y + 1)))
            {
                testPosition.Y++;
                // Optional: Punkte für Hard Drop
                // score += 2; 
            }
            currentPosition = testPosition;
            LandTetromino();
            // UpdateScoreDisplay(); // Falls Punkte für Hard Drop vergeben werden
        }


        private bool IsValidPosition(Tetromino tetromino, Point position)
        {
            for (int y = 0; y < tetromino.Shape.GetLength(0); y++)
            {
                for (int x = 0; x < tetromino.Shape.GetLength(1); x++)
                {
                    if (tetromino.Shape[y, x] == 1)
                    {
                        int boardX = (int)(position.X + x);
                        int boardY = (int)(position.Y + y);

                        // Außerhalb der Grenzen?
                        if (boardX < 0 || boardX >= BoardWidth || boardY < 0 || boardY >= BoardHeight)
                        {
                            return false;
                        }
                        // Kollision mit einem bereits gelandeten Block?
                        if (landedBlocks[boardX, boardY] != null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void LandTetromino()
        {
            if (currentTetromino == null) return;

            for (int y = 0; y < currentTetromino.Shape.GetLength(0); y++)
            {
                for (int x = 0; x < currentTetromino.Shape.GetLength(1); x++)
                {
                    if (currentTetromino.Shape[y, x] == 1)
                    {
                        int boardX = (int)(currentPosition.X + x);
                        int boardY = (int)(currentPosition.Y + y);
                        // Stelle sicher, dass der Block innerhalb der Grenzen ist, bevor er platziert wird
                        if (boardX >= 0 && boardX < BoardWidth && boardY >= 0 && boardY < BoardHeight)
                        {
                            landedBlocks[boardX, boardY] = currentTetromino.Color;
                        }
                        else
                        {
                            // Dies sollte idealerweise nicht passieren, wenn IsValidPosition korrekt funktioniert
                            // Könnte auf einen Fehler in der Logik oder eine extreme Bedingung hinweisen
                            // GameOver(); // Eine Möglichkeit wäre, hier das Spiel zu beenden
                            // return;
                        }
                    }
                }
            }

            RedrawLandedBlocks(); // Aktualisiere das Canvas mit dem neu gelandeten Stein
            CheckForClearedLines();
            SpawnNewTetromino(); // Nächsten Stein spawnen
        }

        private void CheckForClearedLines()
        {
            int linesClearedThisTurn = 0;
            for (int y = BoardHeight - 1; y >= 0; y--)
            {
                bool lineIsFull = true;
                for (int x = 0; x < BoardWidth; x++)
                {
                    if (landedBlocks[x, y] == null)
                    {
                        lineIsFull = false;
                        break;
                    }
                }

                if (lineIsFull)
                {
                    linesClearedThisTurn++;
                    // Linie entfernen und obere Linien nach unten verschieben
                    for (int rowToClear = y; rowToClear > 0; rowToClear--)
                    {
                        for (int col = 0; col < BoardWidth; col++)
                        {
                            landedBlocks[col, rowToClear] = landedBlocks[col, rowToClear - 1];
                        }
                    }
                    // Oberste Linie leeren
                    for (int col = 0; col < BoardWidth; col++)
                    {
                        landedBlocks[col, 0] = null;
                    }
                    y++; // Überprüfe die gleiche Zeile erneut, da sie jetzt neue Blöcke enthält
                }
            }

            if (linesClearedThisTurn > 0)
            {
                // Punkte basierend auf Anzahl der gleichzeitig gelöschten Linien
                if (linesClearedThisTurn == 1) score += 40 * level;
                else if (linesClearedThisTurn == 2) score += 100 * level;
                else if (linesClearedThisTurn == 3) score += 300 * level;
                else if (linesClearedThisTurn >= 4) score += 1200 * level; // Tetris!

                linesCleared += linesClearedThisTurn;
                UpdateScoreDisplay();

                // Level erhöhen alle 10 gelöschten Linien
                if (linesCleared / 10 >= level) // Integer division
                {
                    level++;
                    UpdateLevelDisplay();
                    SetTimerInterval(); // Geschwindigkeit anpassen
                }
                RedrawLandedBlocks(); // Spielfeld nach Linienlöschung neu zeichnen
            }
        }

        private void UpdateScoreDisplay()
        {
            ScoreText.Text = score.ToString();
        }
        private void UpdateLevelDisplay()
        {
            LevelText.Text = level.ToString();
        }


        private void GameOver()
        {
            gameRunning = false;
            if (gameTimer != null)
            {
                gameTimer.Stop();
            }
            GameOverText.Visibility = Visibility.Visible;
            StartButton.IsEnabled = true; // Erlaube Neustart
            StartButton.Content = "Neustart";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }

    // Klasse für Tetromino-Steine
    public class Tetromino
    {
        public int[,] Shape { get; private set; }
        public SolidColorBrush Color { get; private set; }
        private int type; // Zum Speichern des Originaltyps für Rotationslogik (optional)

        // Formen der Tetrominoes (0=leer, 1=Block)
        // Indizes: [Typ][Rotation][Zeile][Spalte] - oder einfacher pro Typ
        private static readonly int[][,] Shapes = new int[][,]
        {
            // I
            new int[,] { { 1, 1, 1, 1 } }, // Könnte auch als 4x4 mit leeren Zeilen dargestellt werden für Rotation
            // J
            new int[,] { { 1, 0, 0 },
                         { 1, 1, 1 } },
            // L
            new int[,] { { 0, 0, 1 },
                         { 1, 1, 1 } },
            // O
            new int[,] { { 1, 1 },
                         { 1, 1 } },
            // S
            new int[,] { { 0, 1, 1 },
                         { 1, 1, 0 } },
            // T
            new int[,] { { 0, 1, 0 },
                         { 1, 1, 1 } },
            // Z
            new int[,] { { 1, 1, 0 },
                         { 0, 1, 1 } }
        };

        // Alternative Darstellung für I für einfachere Rotation
        private static readonly int[][,] ShapesAlt = new int[][,]
       {
            // I (als 4x4)
            new int[,] { { 0,0,0,0 }, { 1,1,1,1 }, { 0,0,0,0 }, { 0,0,0,0 } },
            // J
            new int[,] { { 1,0,0 }, { 1,1,1 }, { 0,0,0 } },
            // L
            new int[,] { { 0,0,1 }, { 1,1,1 }, { 0,0,0 } },
            // O
            new int[,] { { 1,1 }, { 1,1 } },
            // S
            new int[,] { { 0,1,1 }, { 1,1,0 }, { 0,0,0 } },
            // T
            new int[,] { { 0,1,0 }, { 1,1,1 }, { 0,0,0 } },
            // Z
            new int[,] { { 1,1,0 }, { 0,1,1 }, { 0,0,0 } }
       };


        public Tetromino(int type, SolidColorBrush color)
        {
            this.type = type;
            // Wähle die passende Shape-Definition. Für I ist die 4x4-Variante besser für die Rotation.
            if (type == 0) // I-Block
            {
                Shape = (int[,])ShapesAlt[type].Clone(); // Klonen, um Original nicht zu verändern
            }
            else
            {
                Shape = (int[,])Shapes[type].Clone();
                // Wenn ShapesAlt für alle verwendet wird, dann ShapesAlt hier
                // Shape = (int[,])ShapesAlt[type].Clone();
            }
            Color = color;
        }

        public Tetromino GetRotated()
        {
            // O-Block muss nicht rotieren
            if (type == 3) // O-Block
            {
                return new Tetromino(this.type, this.Color) { Shape = (int[,])this.Shape.Clone() };
            }

            int rows = Shape.GetLength(0);
            int cols = Shape.GetLength(1);
            int[,] rotatedShape = new int[cols, rows]; // Dimensionen tauschen

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    rotatedShape[x, rows - 1 - y] = Shape[y, x];
                }
            }
            return new Tetromino(this.type, this.Color) { Shape = rotatedShape };
        }
    }
}
