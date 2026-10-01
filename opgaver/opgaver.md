# Blazor WebAssembly — Øvelser og Opgaver (Lektion 09)

Dette opgavesæt indeholder to praktiske opgaver, hvor du får afprøvet centrale koncepter i Blazor WebAssembly: komponentopdeling, state management, event handling, asynkrone API-kald og livscyklusmetoder.

---

## 🎮 Opgave 1: Byg et "Fire på stribe"-spil (Microsoft Learn)

### Formål
Formålet med denne opgave er at få praktisk erfaring med at bygge en fuldt funktionel, interaktiv Blazor-applikation ved at følge Microsofts officielle trin-for-trin guide. Gennem opgaven vil du arbejde med:
- Komponentstruktur i Blazor (`.razor`)
- Separering af forretningslogik og præsentationslag (`GameState.cs`)
- Event handling (klik på kolonner til at placere brikker)
- Styling og CSS-animationer direkte knyttet til Blazor-komponenter
- Håndtering af spiltilstand og sejrsbetingelser

### Opgavevejledning
1. Gå til Microsoft Learn modulet:
   👉 **[Byg et Fire på stribe-spil med Blazor](https://learn.microsoft.com/da-dk/training/modules/dotnet-connect-four/)**
2. Følg modulets enheder:
   * **Opret et nyt Blazor-projekt:** Sæt projektet op (du kan oprette en ny Blazor WebAssembly applikation via CLI med `dotnet new blazorwasm -o ConnectFour` eller i dit IDE).
   * **Opret spiltilstanden (`GameState.cs`):** Implementer spillets logik, tur-skifte (Spiller 1 vs. Spiller 2), brættets matrix og kontrol af fire på stribe.
   * **Byg spillepladen (`Board.razor`):** Opret bræt-komponenten, render de 42 felter (7 kolonner x 6 rækker), og håndter klik-begivenheder.
   * **Tilføj styling og animationer:** Tilføj CSS, så brikkerne glider ned på pladen med animationer, og farverne matcher den aktive spiller.
   * **Færdiggør spillet:** Vis en statusbesked om tur, annoncér vinderen, og tilføj en knap til at starte et nyt spil.

### 🧠 Refleksionsspørgsmål
Besvar eller diskuter følgende spørgsmål, når du har gennemført tutorialen:
1. Hvorfor er spillets logik isoleret i `GameState.cs` i stedet for at være skrevet direkte i `@code`-blokken i `Board.razor`? Hvilke fordele giver det (separation of concerns, testbarhed)?
2. Hvordan registreres og nulstilles spiltilstanden mellem spil?
3. Hvordan reagerer Blazor på, at en spiller klikker på en kolonne, og hvordan opdateres UI'et med den nye brik?

### 🌟 Ekstra udfordringer (valgfrit)
* **Score-tæller:** Tilføj en tæller, der holder styr på, hvor mange runder henholdsvis Spiller 1 og Spiller 2 har vundet under den aktuelle session.
* **Fortryd-funktion (Undo move):** Tilføj en knap, der lader spillerne fortryde det seneste træk.

---

## ☀️ Opgave 2: Vejr-dashboard med API-integration (Lifecycle & HttpClient)

### Formål
I denne opgave skal du bygge et vejr-dashboard i Blazor WebAssembly, som henter live vejrdata fra et eksternt REST API. Opgaven fokuserer på:
- Asynkrone livscyklusmetoder (`OnInitializedAsync`, `OnParametersSetAsync`)
- Registrering og brug af `HttpClient` via Dependency Injection (DI)
- Håndtering af asynkrone tilstande: *Loading*, *Success* og *Error*
- Strukturering af applikationen i genanvendelige child-komponenter og parameters (`[Parameter]`)

---

### 🌐 API: Open-Meteo (Gratis, kræver ingen API-nøgle)
Vi benytter det åbne API [Open-Meteo](https://open-meteo.com/). Du kan kalde endpointet direkte:

#### Eksempel på API-kald (Aarhus):
```text
https://api.open-meteo.com/v1/forecast?latitude=56.1567&longitude=10.2108&current=temperature_2m,relative_humidity_2m,weather_code,wind_speed_10m&daily=weather_code,temperature_2m_max,temperature_2m_min&timezone=auto
```

#### Koordinater til udvalgte danske byer:
| By | Latitude | Longitude |
| :--- | :--- | :--- |
| **Aarhus** | `56.1567` | `10.2108` |
| **København** | `55.6759` | `12.5655` |
| **Odense** | `55.3959` | `10.3883` |
| **Aalborg** | `57.0480` | `9.9187` |

---

### Krav til løsningen

#### 1. Modeller og Datastruktur
* Undersøg API-svaret fra Open-Meteo (f.eks. ved at åbne URL'en i browseren).
* Opret C#-modeller (klasser eller records), der matcher JSON-strukturen for:
  * Aktuelt vejr (`current`: temperatur, luftfugtighed, vindhastighed, `weather_code`).
  * Vejrudsigt (`daily`: datoer/tidspunkter, min- og max-temperaturer, `weather_code`).

#### 2. Service-lag (Dependency Injection)
* Opret et interface `IWeatherService` og en implementation `WeatherService`.
* Servicen skal indeholde en metode, f.eks.:
  ```csharp
  Task<WeatherForecastDto?> GetForecastAsync(double latitude, double longitude);
  ```
* Registrer `WeatherService` i `Program.cs` ved hjælp af Blazors service container.

#### 3. Brugergrænseflade & Komponentopdeling
Byg siden `WeatherDashboard.razor` (med rute f.eks. `@page "/weather-dashboard"`):
* **By-vælger:** En række knapper eller en `<select>` dropdown, hvor brugeren kan skifte mellem de forskellige byer.
* **Aktuelt vejr komponent (`CurrentWeather.razor`):**
  * Modtager data via `[Parameter]`.
  * Viser byens navn, aktuel temperatur i °C, vindhastighed (m/s) samt en vejrbeskrivelse eller ikon baseret på WMO-vejrkoden (f.eks. kode 0 = Solrigt, 1-3 = Skyet, 61-65 = Regn).
* **Vejrudsigt komponent (`DailyForecast.razor`):**
  * Modtager prognosen for de kommende dage og viser dem overskueligt (f.eks. som kort eller i en tabel med dag, forventet min/maks temperatur og vejrtype).
* **Tilstandshåndtering (UI States):**
  * **Loading:** Mens data hentes asynkront i `OnInitializedAsync()` eller ved by-skift, skal der vises en indlæsningsindikator (spinner eller "Henter data...").
  * **Error:** Hvis API-kaldet fejler (f.eks. ved manglende internet eller fejl i forespørgslen), skal der vises en pæn fejlmeddelelse frem for at applikationen crasher.

---

### 🌟 Ekstra udfordringer (valgfrit)
1. **Fritekstsøgning efter byer (Geocoding):**
   Benyt Open-Meteos gratis Geocoding API (`https://geocoding-api.open-meteo.com/v1/search?name=Berlin&count=5&language=da&format=json`) til at lave et søgefelt, hvor brugeren kan indtaste et vilkårligt bynavn i verden og hente vejret for det fundne resultat.
