---
marp: true
theme: default
paginate: true
html: true
header: 'C# & .NET — Blazor WebAssembly (WASM)'
footer: 'C# / .NET Lektion 09 — Blazor & WebAssembly'
style: |
  section {
    font-family: 'Segoe UI', Roboto, Helvetica, Arial, sans-serif;
    padding: 35px 45px;
    background-color: #f8f9fa;
    color: #212529;
  }
  h1 { color: #512BD4; font-size: 2.1em; margin-top: 0; }
  h2 {
    color: #170C3A;
    border-bottom: 3px solid #512BD4;
    padding-bottom: 6px;
    font-size: 1.4em;
    margin-top: 0;
    margin-bottom: 14px;
  }
  h3 { color: #512BD4; font-size: 1.1em; margin-top: 8px; margin-bottom: 4px; }
  p, ul, ol { margin-bottom: 8px; font-size: 0.88em; }
  li { margin-bottom: 4px; }
  blockquote {
    background: #eef2ff;
    border-left: 6px solid #512BD4;
    margin: 10px 0;
    padding: 8px 12px;
    border-radius: 0 8px 8px 0;
    font-size: 0.85em;
  }
  code {
    background-color: #282a36;
    color: #f8f8f2;
    border-radius: 4px;
    padding: 2px 6px;
    font-size: 0.82em;
  }
  pre {
    background-color: #1e1e2e !important;
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    font-size: 0.72em;
    line-height: 1.35;
    padding: 10px 14px;
    margin: 8px 0;
    color: #f8f8f2 !important;
  }
  pre code { font-size: 1em; padding: 0; background-color: transparent; color: #f8f8f2 !important; }

  /* High-Contrast Syntax Highlighting */
  .hljs-keyword, .hljs-selector-tag, .hljs-subst { color: #ff79c6 !important; font-weight: bold; }
  .hljs-title, .hljs-title.class_, .hljs-title.function_ { color: #50fa7b !important; }
  .hljs-built_in, .hljs-type { color: #8be9fd !important; }
  .hljs-string, .hljs-meta .hljs-string { color: #f1fa8c !important; }
  .hljs-number, .hljs-literal { color: #bd93f9 !important; }
  .hljs-comment, .hljs-doctag { color: #7f88a8 !important; font-style: italic; }
  .hljs-meta, .hljs-meta .hljs-keyword, .hljs-attr { color: #ffb86c !important; }
  .hljs-variable, .hljs-template-variable { color: #f8f8f2 !important; }
  .hljs-name, .hljs-tag, .hljs-selector-id, .hljs-selector-class { color: #ff79c6 !important; }
  .hljs-attribute { color: #50fa7b !important; }

  table { width: 100%; border-collapse: collapse; margin: 10px 0; font-size: 0.8em; }
  th { background-color: #512BD4; color: white; padding: 8px 10px; text-align: left; }
  td { border-bottom: 1px solid #dee2e6; padding: 6px 10px; }
  tr:nth-child(even) { background-color: #eef2ff; }
  .grid-2 { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; margin-top: 8px; }
  .grid-3 { display: grid; grid-template-columns: 1fr 1fr 1fr; gap: 14px; margin-top: 8px; }
  .card {
    background: white;
    padding: 12px 14px;
    border-radius: 8px;
    border-top: 4px solid #512BD4;
    box-shadow: 0 2px 8px rgba(0,0,0,0.08);
  }
  .card h3 { margin-top: 0; margin-bottom: 6px; font-size: 1.05em; }
  .card p, .card ul, .card ol { font-size: 0.82em; margin-bottom: 4px; }
  .card ul, .card ol { padding-left: 16px; }
  pre.ascii-diagram { font-size: 0.62em; line-height: 1.2; padding: 10px 14px; color: #50fa7b !important; }
---

<!-- _class: lead -->
# Blazor & Blazor WebAssembly (WASM)
### Byg moderne web-applikationer i C# i stedet for JavaScript

**C# / .NET Lektion 09**  
*Klient-side webudvikling med WebAssembly*

---

## 📋 Dagsorden

1. **Hvad er Blazor?** — Introduktion og kernekoncepter
2. **Hosting Modeller** — Server vs. WebAssembly vs. Auto Mode
3. **WebAssembly (WASM) i dybden** — Hvordan kører C# i browseren?
4. **Blazor WASM Arkitektur** — Indlæsning & afvikling
5. **Komponentmodellen (.razor)** — HTML, Razor & C#
6. **Data Binding & Events** — Interaktivitet i brugerfladen
7. **Komponent Livscyklus** — `OnInitialized`, `OnParametersSet`, osv.
8. **REST API Integration** — `HttpClient` & Asynkront data-hentning
9. **State Management & JS Interop** — Tilstand og JavaScript integration
10. **.NET 8/9 Render Modes & Best Practices** — Optimering og sikkerhed

---

## 🌐 1. Hvad er Blazor?

Blazor er Microsofts **Single Page Application (SPA)** framework til at bygge interaktive web-UI'er med **C#**.

<div class="grid-2">
<div class="card">

### ⚡ Hovedpunkter
* **Fuld C# i browseren**: Udskift eller suppler JavaScript med C#.
* **Genbrug af kode**: Del modeller, validering og forretningslogik mellem server og klient.
* **Komponentbaseret**: Byg genanvendelige UI-komponenter (`.razor`).
* **Del af .NET økosystemet**: Brug NuGet-pakker, LINQ, DI og stærk typning.

</div>
<div class="card">

### 💡 Hvorfor vælge Blazor?
* **Udvikler-produktivitet**: Samme sprog (C#) og tooling (Visual Studio / Rider / VS Code) i hele stacken.
* **Type-sikkerhed**: Færre runtime fejl i forhold til dynamisk JavaScript/TypeScript.
* **Modern Web Standards**: Kører direkte i browseren uden plugins.

</div>
</div>

---

## 🏗️ 2. Blazor Hosting Modeller Overview

Blazor tilbyder tre primære hosting-modeller til afvikling af applikationen:

<div class="grid-3">
<div class="card">

### 🖥️ Blazor Server
* **Afvikling**: Serveren kører C#-koden.
* **Kommunikation**: SignalR (WebSockets) sender UI-opdateringer og events.
* **Fordel**: Hurtig initial load, direkte DB-adgang.

</div>
<div class="card">

### ⚡ Blazor WASM
* **Afvikling**: Kører **100% i klientens browser** via WebAssembly.
* **Kommunikation**: Henter data via REST APIs / gRPC.
* **Fordel**: Offline support, afskærer serverbelastning.

</div>
<div class="card">

### 🔄 Blazor Auto (.NET 8+)
* **Afvikling**: Starter hurtigt som SSR/Server, downloader WASM i baggrunden.
* **Skift**: Skifter automatisk til WASM ved næste besøg!

</div>
</div>

---

## 🧩 3. WebAssembly (WASM) i Dybden

WebAssembly er en åben standard (W3C), der tillader afvikling af kompileret kode i browseren.

<div class="grid-2">
<div class="card">

### 🔍 Hvad er WebAssembly?
* et **lavniveau, binært instruktionsformat** (bytecode).
* Kører med næsten **native hastighed** i et sikkert sandkasse-miljø i browseren.
* Understøttes af alle moderne browsere (Chrome, Edge, Firefox, Safari).
* Ingen browser-plugins påkrævet (som i gamle dage med Silverlight / Flash).

</div>
<div class="card">

### ⚙️ Hvordan kører .NET på WASM?
* .NET Runtime (`dotnet.native.wasm`) kompileres til WebAssembly.
* Browseren downloader .NET WASM runtime samt applikationens `.dll` filer.
* Koden afvikles i browserens C# runtime (kaldet Mono/dotnet runtime).

</div>
</div>

---

## 🏛️ 4. Blazor WASM Arkitektur & Opstart

Hvordan starter en Blazor WebAssembly applikation op i brugerens browser?

<pre class="ascii-diagram">
+-------------------------------------------------------------------------------+
| BROWSER SANDBOX                                                               |
|                                                                               |
|  1. HTML / index.html ---> Indlæser `_framework/blazor.webassembly.js`       |
|                                     |                                         |
|  2. Indlæser WASM Runtime --------> `dotnet.native.wasm`                      |
|                                     |                                         |
|  3. Download Assemblies ----------> `App.dll`, `System.Text.Json.dll`, etc.   |
|                                     |                                         |
|  4. C# Execution Engine ----------> Afvikler Razor Komponenter & C# Logik     |
|                                     |                                         |
|  5. DOM Rendering ----------------> Opdaterer browserens DOM via JS Bridge   |
+-------------------------------------------------------------------------------+
</pre>

> [!NOTE]
> Efter den indledende download kører applikationen **helt uafhængigt** af en .NET server.

---

## ⚖️ 5. Blazor WASM vs. Blazor Server

| Egenskab | Blazor WebAssembly (WASM) | Blazor Server |
| :--- | :--- | :--- |
| **Afviklingssted** | Klientens browser | Webserver |
| **Serverkrav** | Kræver kun statisk webserver (Nginx, S3) | Kræver aktiv ASP.NET Core server |
| **Offline-støtte** | **Ja** (PWA support) | **Nej** (Kræver konstant SignalR) |
| **Første indlæsningstid** | Langsommere (skal hente runtime + DLLs) | Meget hurtig |
| **Ydeevne / Latens** | Ingen netværksforsinkelse ved UI-events | Latens ved hver brugerinteraktion |
| **Sikkerhed af kode** | DLL'er hentes til klient (kan dekompileres)| Koden forbliver fortrolig på serveren |

---

## ⚖️ 6. Fordele & Ulemper ved Blazor WASM

<div class="grid-2">
<div class="card">

### ✅ Fordele
* **Ingen server-skalering**: Ingen vedvarende WebSockets-forbindelser pr. bruger.
* **Offline & PWA**: Kan fungere offline som Progressive Web App.
* **Fleksibel hosting**: Kan hostes på GitHub Pages, Azure Static Web Apps, AWS S3.
* **Full-stack C#**: Brug de samme kodelokale datamodeller på klient og server.

</div>
<div class="card">

### ⚠️ Ulemper & Udfordringer
* **Initial download-størrelse**: Browseren skal hente runtime og `.dll`-filer.
* **Begrænset af browseren**: Kan ikke tilgå databaser direkte (skal bruge Web API).
* **Browser ydeevne**: Afhænger af klientens enhed og browser CPU/hukommelse.

</div>
</div>

---

## 🧩 7. Blazor Komponenter (.razor)

Alt i Blazor er bygget af **Razor-komponenter** (filer med `.razor` endelse).

<div class="grid-2">
<div class="card">

### 📄 Hvad indeholder en `.razor` fil?
* **HTML Markup**: Standard HTML til strukturering af UI.
* **Razor Syntaks**: `@` tegn til at tilgå C# variabler og løkker.
* **`@code` blok**: C# felter, properties, metoder og event handlers.
* **Direktiver**: `@page`, `@inject`, `@using`, `@bind`.

</div>
<div class="card">

### 🔄 Hvordan fungerer render-træet?
* Blazor opbygger et internt **RenderTree** i C#.
* Ved ændringer beregnes forskellen (**Diffing**).
* Kun de ændrede DOM-elementer opdateres i browseren.

</div>
</div>

---

## 💻 8. Eksempel: En Enkel Blazor WASM Komponent

En typisk tæller-komponent (`Counter.razor`):

```razor
@page "/counter"
@inject IJSRuntime JS

<PageTitle>Tæller Komponent</PageTitle>

<h2>Counter</h2>
<p>Aktuel tæller-værdi: <strong>@currentCount</strong></p>

<button class="btn btn-primary" @onclick="IncrementCount">Klik her</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

---

## 🔗 9. Data Binding & Event Handling

Blazor understøtter både **1-vejs** og **2-vejs** databinding samt event-håndtering.

<div class="grid-2">
<div class="card">

### 📥 Data Binding
* **1-vejs binding**: `@variable` viser værdien i HTML.
* **2-vejs binding**: `@bind-value="searchText"` opdaterer C# variabel når feltet ændres.
* **Event binding**: `@bind-value:event="oninput"` giver øjeblikkelig opdatering ved indtastning.

</div>
<div class="card">

### ⚡ Event Handling
* **Event Handlers**: `@onclick="HandleClick"`, `@onchange="OnInputChanged"`.
* **Argumenter**: `@onclick="(e => HandleCustom(e, itemId))"`.
* **Async Events**: `async Task HandleClickAsync()`.

</div>
</div>

```razor
<input @bind="userName" @bind:event="oninput" placeholder="Indtast navn..." />
<p>Velkommen, @userName!</p>
```

---

## 🔄 10. Komponent Livscyklus (Lifecycle Hooks)

Blazor komponenter har en veldefineret livscyklus med asynkrone hooks:

<div class="grid-2">
<div class="card">

### 🚀 Initialisering & Parameters
* `OnInitialized()` / `OnInitializedAsync()`  
  *Kører når komponenten oprettes. Ideelt til datahentning!*
* `OnParametersSet()` / `OnParametersSetAsync()`  
  *Kører når `@Parameter` værdier ændres fra parent.*

</div>
<div class="card">

### 🎨 Rendering & Cleanup
* `OnAfterRender(bool firstRender)`  
  *Kører efter DOM er opdateret. Bruges til JS Interop.*
* `IDisposable.Dispose()`  
  *Bruges til oprydning (afmelde events, timers osv.).*

</div>
</div>

```csharp
protected override async Task OnInitializedAsync()
{
    // Hent data fra REST API når komponenten indlæses
    products = await Http.GetFromJsonAsync<List<Product>>("api/products");
}
```

---

## 🌐 11. Kommunikation med Backend REST API'er

Da Blazor WASM kører i browseren, skal databaseadgang ske via et backend REST API.

<div class="grid-2">
<div class="card">

### 🔧 Registrering af HttpClient
I `Program.cs` tilføjes `HttpClient` til Dependency Injection:

```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("https://api.example.com/") 
});

await builder.Build().RunAsync();
```

</div>
<div class="card">

### 💡 Indsprøjtning i Komponenter
I din `.razor` fil indsprøjtes `HttpClient`:

```razor
@using System.Net.Http.Json
@inject HttpClient Http
```

* `GetFromJsonAsync<T>()`
* `PostAsJsonAsync<T>()`
* `PutAsJsonAsync<T>()`
* `DeleteAsync()`

</div>
</div>

---

## 💻 12. Eksempel: REST API Integration

Et komplet eksempel på data-hentning med loading state og fejlhåndtering:

```razor
@page "/todos"
@inject HttpClient Http

<h2>Todo Liste</h2>

@if (todos == null) {
    <p><em>Henter data fra API...</em></p>
} else {
    <ul>
        @foreach (var item in todos) {
            <li>@item.Title - <strong>@(item.IsCompleted ? "Færdig" : "I gang")</strong></li>
        }
    </ul>
}

@code {
    private List<TodoItem>? todos;

    protected override async Task OnInitializedAsync() {
        todos = await Http.GetFromJsonAsync<List<TodoItem>>("https://jsonplaceholder.typicode.com/todos");
    }

    public record TodoItem(int Id, string Title, bool IsCompleted);
}
```

---

## 💾 13. State Management i Blazor WASM

I Blazor WASM nulstilles in-memory tilstand (state), hvis brugeren opdaterer siden (F5).

<div class="grid-3">
<div class="card">

### 1️⃣ Singleton Services
* Bevarer tilstand på tværs af komponenter i den samme session.
* Perfekt til indkøbskurv eller bruger-session.

</div>
<div class="card">

### 2️⃣ LocalStorage
* Gemmer data i browserens storage (overlever F5 og lukning).
* Brug biblioteker som `Blazored.LocalStorage`.

</div>
<div class="card">

### 3️⃣ Cascading Parameters
* Sender tilstand ned igennem komponent-træet til underkomponenter.

</div>
</div>

> [!TIP]
> Brug en samlet `AppState` klasse registreret som `Scoped` eller `Singleton` i DI til central tilstandsstyring.

---

## 🌁 14. JavaScript Interoperabilitet (JS Interop)

Nogle gange har vi brug for browser-APIs eller eksisterende JS-biblioteker (f.eks. Charts, Maps).

<div class="grid-2">
<div class="card">

### 📞 C# kalder JavaScript
Brug `IJSRuntime` til at kalde JS funktioner:

```csharp
@inject IJSRuntime JS

@code {
    private async Task ShowAlert() {
        await JS.InvokeVoidAsync("alert", "Hej fra C#!");
    }
    
    private async Task<string> GetPrompt() {
        return await JS.InvokeAsync<string>("prompt", "Indtast dit navn:");
    }
}
```

</div>
<div class="card">

### 📞 JavaScript kalder C#
C# metoder markeret med `[JSInvokable]` kan kaldes fra JavaScript:

```csharp
[JSInvokable]
public static Task<string> GetHelloMessage() {
    return Task.FromResult("Svar fra C# WASM!");
}
```

* `DotNet.invokeMethodAsync('AssemblyName', 'GetHelloMessage')`

</div>
</div>

---

## 🚀 15. .NET 8 / 9 Render Modes

.NET 8 introducerede **Full-Stack Blazor**, hvor man kan blande rendering-strategier pr. komponent!

```razor
@* Gør denne komponent interaktiv via WebAssembly *@
@rendermode InteractiveWebAssembly
```

<div class="grid-3">
<div class="card">

### Static SSR
* Server-Side Rendered HTML uden WebSocket/WASM overhead.
* Hurtigst til statiske sider.

</div>
<div class="card">

### Interactive Server
* Kører på serveren via SignalR.
* Omgående interaktivitet.

</div>
<div class="card">

### Interactive WASM
* Kører i browseren via WebAssembly.
* Fuld klient-side autonomi.

</div>
</div>

> [!IMPORTANT]
> **Interactive Auto** giver det bedste fra begge verdener: Server-rendering første gang, og WASM på efterfølgende besøg!

---

## ⚡ 16. Ydeevne & Optimeringsstrategier

For at få den bedste ydeevne ud af Blazor WebAssembly:

<div class="grid-2">
<div class="card">

### 🚀 AOT (Ahead-Of-Time) Compilation
* **JIT (Default)**: .NET IL-kode fortolkes runtime i WASM.
* **AOT Compilation**: Kompilerer C# direkte til native WebAssembly binærkode under build.
* **Resultat**: Op til **3x-5x hurtigere** beregninger! *(Men giver større download-størrelse)*.

</div>
<div class="card">

### 📦 Trimming & Compression
* **IL Trimming**: Fjerner ubrugt .NET kode fra DLL'erne ved publish.
* **Brotli / Gzip**: Komprimerer filer før de sendes til browseren.
* **Lazy Loading**: Hent kun assemblies for specifikke ruter, når brugeren besøger dem.

</div>
</div>

---

## 🔒 17. Sikkerhed i Blazor WebAssembly

> [!CAUTION]
> **Husk**: Alt hvad der sendes til Blazor WebAssembly (DLL'er, kode, konfiguration) ligger på brugerens computer!

<div class="grid-2">
<div class="card">

### ❌ Hvad man IKKE må gøre
* Gem **aldrig** database connection strings i Blazor WASM.
* Gem **aldrig** følsomme API-nøgler eller passwords i klientkoden.
* Stol **aldrig** udelukkende på klient-validering (valider altid på serveren!).

</div>
<div class="card">

### ✅ Korrekt Sikkerhedsarkitektur
* Autentificering via **OAuth2 / OIDC / JWT** tokens (f.eks. MSAL, Duende IdentityServer).
* Alt data-adgang skal ske gennem et sikkert **Backend Web API**.
* API'et verificerer altid modtaget JWT token ved hvert request.

</div>
</div>

---

## 🎯 18. Hvornår skal man vælge Blazor WASM?

<div class="grid-2">
<div class="card">

### ✅ Blazor WASM er oplagt til:
* **Offline-første applikationer** (PWA).
* Apps med **mange klient-side beregninger** (f.eks. spil, billedbehandling, CAD).
* Apps med et **stort antal brugere**, hvor man vil undgå høje serveromkostninger til SignalR.
* **Intranet/Enterprise apps**, hvor initial download-tid ikke er kritisk.

</div>
<div class="card">

### ❌ Overvej Blazor Server / SSR til:
* **SEO-kritiske offentlige hjemmesider** (e-commerce, nyhedssider).
* Apps til enheder med **meget svag hardware eller langsomme forbindelser**.
* Apps der skal starte øjeblikkeligt uden initial load tid.

</div>
</div>

---

## 🏁 19. Opsummering

<div class="card">

### 🔑 Nøglepointer
1. **Blazor WASM** kører C# og .NET runtime direkte i browseren via WebAssembly uden server-afhængighed.
2. **Komponenter** skrives i Razor-filer (`.razor`) med en ren blanding af HTML og C#.
3. **Dataadgang** sker asynkront via `HttpClient` mod et backend REST API.
4. **JS Interop** giver adgang til browser APIs og JavaScript biblioteker ved behov.
5. **.NET 8/9 Render Modes** gør det muligt at kombinere SSR og WASM fleksibelt i den samme applikation.

</div>

---

<!-- _class: lead -->
# Spørgsmål & Diskussion ❓

### Tak for opmærksomheden!
*Klar til at bygge C# applikationer direkte i browseren?*
