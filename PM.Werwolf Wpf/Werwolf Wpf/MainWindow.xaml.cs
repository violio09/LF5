using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Für KeyDown
using System.IO;
using Microsoft.Win32;

namespace WerwolfWPF
{
    public partial class MainWindow : Window
    {
        private int anzahlSpieler;
        private List<string> spielerNamenListe; // NEU: Liste für Spielernamen
        private string[] spielerRollenNamen;
        private List<string> zuVergebendeRollenPool;
        private int aktuellerSpielerIndex; // Wird jetzt für Namenseingabe UND Rollenanzeige verwendet
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitialisiereSpielzustand();
        }

        private void InitialisiereSpielzustand()
        {
            SpieleranzahlEingabePanel.Visibility = Visibility.Visible;
            NamenEingabePanel.Visibility = Visibility.Collapsed; // NEU
            RollenAnzeigePanel.Visibility = Visibility.Collapsed;
            SpielEndePanel.Visibility = Visibility.Collapsed;

            ErrorTextBlock.Text = "";
            NameErrorTextBlock.Text = ""; // NEU
            StatusTextBlock.Text = "Willkommen zum Werwolf Spiel!";
            SpieleranzahlTextBox.Text = "";
            NameTextBox.Text = ""; // NEU

            aktuellerSpielerIndex = 0;
            spielerNamenListe = new List<string>(); // NEU
        }

        private void SpielStartenButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
            if (int.TryParse(SpieleranzahlTextBox.Text, out anzahlSpieler))
            {
                if (anzahlSpieler >= 3)
                {
                    spielerRollenNamen = new string[anzahlSpieler];
                    spielerNamenListe = new List<string>(anzahlSpieler); // Kapazität initialisieren
                    aktuellerSpielerIndex = 0;

                    SpieleranzahlEingabePanel.Visibility = Visibility.Collapsed;
                    NamenEingabePanel.Visibility = Visibility.Visible; // Zum Namenseingabe-Panel wechseln
                    StatusTextBlock.Text = "Namen eingeben";
                    AktualisiereNamensEingabeAufforderung();
                }
                else
                {
                    ErrorTextBlock.Text = "Es werden mindestens 3 Spieler benötigt.";
                }
            }
            else
            {
                ErrorTextBlock.Text = "Ungültige Eingabe. Bitte gib eine Zahl ein.";
            }
        }

        private void AktualisiereNamensEingabeAufforderung()
        {
            PromptTextBlockNamensEingabe.Text = $"Spieler {aktuellerSpielerIndex + 1}, bitte gib deinen Namen ein:";
            NameTextBox.Text = ""; // Textbox leeren
            NameErrorTextBlock.Text = ""; // Fehlermeldung leeren
            NameTextBox.Focus(); // Fokus auf die Textbox setzen
        }

        private void NameBestätigenButton_Click(object sender, RoutedEventArgs e)
        {
            ProzessiereNamenseingabe();
        }

        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProzessiereNamenseingabe();
            }
        }

        private void ProzessiereNamenseingabe()
        {
            string aktuellerName = NameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(aktuellerName))
            {
                NameErrorTextBlock.Text = "Der Name darf nicht leer sein.";
                return;
            }

            spielerNamenListe.Add(aktuellerName);
            aktuellerSpielerIndex++;
            NameErrorTextBlock.Text = ""; // Fehler löschen

            if (aktuellerSpielerIndex < anzahlSpieler)
            {
                AktualisiereNamensEingabeAufforderung();
            }
            else
            {
                // Alle Namen eingegeben, weiter zur Rollenverteilung
                NamenEingabePanel.Visibility = Visibility.Collapsed;
                RollenVorbereitenUndMischen();

                RollenAnzeigePanel.Visibility = Visibility.Visible;
                aktuellerSpielerIndex = 0; // Index für Rollenanzeige zurücksetzen
                AktualisiereSpielerAnzeige();
                StatusTextBlock.Text = "Rollenverteilung";
            }
        }


        private void RollenVorbereitenUndMischen()
        {
            zuVergebendeRollenPool = new List<string>();

            int maxWerwolf = 5;
            int maxJäger = 1;
            int maxHexe = 1;
            int maxAmor = 1;
            int maxBürgermeister = 1;
            int aktuelleRollenAnzahlInPool = 0;

            if (aktuelleRollenAnzahlInPool < anzahlSpieler && maxJäger > 0)
            {
                zuVergebendeRollenPool.Add("Jäger"); maxJäger--; aktuelleRollenAnzahlInPool++;
            }
            if (aktuelleRollenAnzahlInPool < anzahlSpieler && maxHexe > 0)
            {
                zuVergebendeRollenPool.Add("Hexe"); maxHexe--; aktuelleRollenAnzahlInPool++;
            }
            if (aktuelleRollenAnzahlInPool < anzahlSpieler && maxAmor > 0)
            {
                zuVergebendeRollenPool.Add("Amor"); maxAmor--; aktuelleRollenAnzahlInPool++;
            }
            if (aktuelleRollenAnzahlInPool < anzahlSpieler && maxBürgermeister > 0)
            {
                zuVergebendeRollenPool.Add("Bürgermeister"); maxBürgermeister--; aktuelleRollenAnzahlInPool++;
            }

            int werwölfeHinzuzufügen = Math.Min(maxWerwolf, anzahlSpieler - aktuelleRollenAnzahlInPool);
            for (int k = 0; k < werwölfeHinzuzufügen; k++)
            {
                if (aktuelleRollenAnzahlInPool < anzahlSpieler)
                {
                    zuVergebendeRollenPool.Add("Werwolf"); aktuelleRollenAnzahlInPool++;
                }
                else break;
            }

            while (aktuelleRollenAnzahlInPool < anzahlSpieler)
            {
                zuVergebendeRollenPool.Add("Dorfbewohner"); aktuelleRollenAnzahlInPool++;
            }

            zuVergebendeRollenPool = zuVergebendeRollenPool.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < anzahlSpieler; i++)
            {
                if (i < zuVergebendeRollenPool.Count)
                {
                    spielerRollenNamen[i] = zuVergebendeRollenPool[i];
                }
                else
                {
                    spielerRollenNamen[i] = "Dorfbewohner";
                    MessageBox.Show($"Warnung: Nicht genügend spezifische Rollen für Spieler {i + 1}. Rolle wurde auf Dorfbewohner gesetzt.", "Rollenverteilungsfehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void AktualisiereSpielerAnzeige()
        {
            if (aktuellerSpielerIndex < anzahlSpieler && aktuellerSpielerIndex < spielerNamenListe.Count) // Sicherstellen, dass Index gültig ist
            {
                // ANPASSUNG: Spielername verwenden
                AktuellerSpielerTextBlock.Text = $"{spielerNamenListe[aktuellerSpielerIndex]}, du bist an der Reihe.";
                RollenTextBlock.Text = "";
                HinweisTextBlock.Visibility = Visibility.Collapsed;
                RolleAnzeigenButton.Visibility = Visibility.Visible;
                NächsterSpielerButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                RollenAnzeigePanel.Visibility = Visibility.Collapsed;
                SpielEndePanel.Visibility = Visibility.Visible;
                StatusTextBlock.Text = "Spielbereit!";
            }
        }

        private void RolleAnzeigenButton_Click(object sender, RoutedEventArgs e)
        {
            if (aktuellerSpielerIndex < anzahlSpieler)
            {
                RollenTextBlock.Text = $"Deine Rolle ist: {spielerRollenNamen[aktuellerSpielerIndex]}";
                HinweisTextBlock.Visibility = Visibility.Visible;
                RolleAnzeigenButton.Visibility = Visibility.Collapsed;
                NächsterSpielerButton.Visibility = Visibility.Visible;
            }
        }

        private void NächsterSpielerButton_Click(object sender, RoutedEventArgs e)
        {
            aktuellerSpielerIndex++;
            AktualisiereSpielerAnzeige();
        }

        private void NeustartButton_Click(object sender, RoutedEventArgs e)
        {
            InitialisiereSpielzustand();
        }

        private void RollenSpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            if (spielerRollenNamen == null || spielerRollenNamen.Length == 0 || spielerNamenListe.Count != spielerRollenNamen.Length)
            {
                MessageBox.Show("Es wurden noch keine Rollen vollständig vergeben oder Namen fehlen, die gespeichert werden könnten.", "Keine Daten zum Speichern", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Textdatei (*.txt)|*.txt|Alle Dateien (*.*)|*.*";
            saveFileDialog.Title = "Rollenverteilung speichern";
            saveFileDialog.FileName = "Werwolf_Rollen.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    List<string> lines = new List<string>();
                    lines.Add("Werwolf Spiel - Rollenverteilung");
                    lines.Add($"Anzahl Spieler: {anzahlSpieler}");
                    lines.Add("----------------------------------");
                    for (int i = 0; i < spielerRollenNamen.Length; i++)
                    {
                        // ANPASSUNG: Spielername in der Ausgabe verwenden
                        lines.Add($"{spielerNamenListe[i]}: {spielerRollenNamen[i]}");
                    }
                    lines.Add("----------------------------------");
                    lines.Add($"Exportiert am: {DateTime.Now}");

                    File.WriteAllLines(saveFileDialog.FileName, lines);
                    MessageBox.Show("Rollen wurden erfolgreich gespeichert!", "Speichern erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Speichern der Datei: {ex.Message}", "Speicherfehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
