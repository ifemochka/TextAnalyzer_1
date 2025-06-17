Лейбович Алёна Максмовна БПИ235, БДЗ 2
# TextAnalyzer. Инструмент для анализа отчетов на антиплагиат и их статистическую обработки.

**Микросервисы**

**1. API Gateway**

Принимает запросы от клиентов и перенаправляет их к соответствующим сервисам.

```
APIGateway/
|
├── Controllers/
│   │
│   └── FileController.cs
│
├── Program.cs
│
├── APIGateway.csproj

```

**2. File Storing Service**

Принимает, сохраняет и выдаёт .txt файлы.


```
FileStoringService/
│
├── Controllers/
│   │
│   └── FileController.cs
│   
├── Services/
│   │
│   └── FileStorageService.cs
│   
├── Models/
│   │
│   └── FileMetadata.cs
│   
├── Data/
│   │
│   └── AppDbContext.cs
│   
├── Program.cs
│   
└── FileStoringService.csproj

```

**3. File Analysis Service**

Обрабатывает тексты.


```
FileAnalysisService/
│   
├── Controllers/
│   │
│   └── AnalysisController.cs
│   
├── Services/
│   │
│   └── TextAnalysisService.cs
│   
├── Models/
│   │
│   └── FileStatistics.cs
│   │
│   └── ComparisonResult.cs
│   
├── Program.cs
│   
└── FileAnalysisService.csproj

```
