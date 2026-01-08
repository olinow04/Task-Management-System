# Task Management System

System zarządzania zadaniami w projektach z możliwością tworzenia projektów, dodawania zadań, przypisywania użytkowników oraz kontrolowania statusów.

## Technologie
- ASP.NET Core MVC (.NET 10)
- Entity Framework Core
- SQL Server (Docker)
- Bootstrap 5
- Identity (autoryzacja i autentykacja)

## Wymagania
- .NET 10 SDK
- Docker Desktop
- Visual Studio 2022 / VS Code (opcjonalnie)

## Instalacja i uruchomienie

### Opcja 1: Docker (Zalecane)

1. Sklonuj repozytorium:
```bash
git clone https://github.com/olinow04/Task-Management-System
cd Task-Management-System
```

2. Uruchom Docker Desktop

3. Zbuduj i uruchom kontenery:
```bash
docker compose up --build
```

4. Aplikacja będzie dostępna pod adresem: `http://localhost:5000`

### Opcja 2: Lokalne uruchomienie

1. Sklonuj repozytorium
2. Przywróć pakiety:
```bash
dotnet restore
```

3. Zastosuj migracje:
```bash
cd Task-Management-System
dotnet ef database update
```

4. Uruchom aplikację:
```bash
dotnet run
```

## Konfiguracja

### Baza danych (Docker)
- Server: `localhost,1433` (z hosta) lub `sqlserver` (w sieci Docker)
- Database: `TaskManagementDB`
- User: `sa`
- Password: `YourStrong@Passw0rd`

### Connection String
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=sqlserver;Database=TaskManagementDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
}
```

## Testowi użytkownicy

Aplikacja automatycznie tworzy dwóch użytkowników przy pierwszym uruchomieniu:

### Administrator
- Email: `admin@taskmanager.com`
- Hasło: `Admin123!`
- Uprawnienia: Pełny dostęp - CRUD dla wszystkich projektów i zadań

### Zwykły użytkownik
- Email: `user@taskmanager.com`
- Hasło: `User123!`
- Uprawnienia: Przeglądanie projektów, dodawanie i edycja zadań (bez usuwania)

## Funkcjonalności

### Autoryzacja
- Rejestracja i logowanie użytkowników (ASP.NET Identity)
- Dwie role: **Admin** i **User**
- Admin: pełny dostęp (edycja, usuwanie projektów i zadań)
- User: przeglądanie, tworzenie i edycja zadań

### Projekty (CRUD)
- Tworzenie nowych projektów
- Edycja projektów (tylko Admin)
- Usuwanie projektów (tylko Admin)
- Przeglądanie szczegółów projektu z listą zadań
- Statusy: Active, Completed, OnHold, Cancelled

### Zadania (CRUD)
- Tworzenie zadań przypisanych do projektów
- Edycja zadań
- Usuwanie zadań (tylko Admin)
- Statusy: ToDo, InProgress, Testing, Done, Blocked
- Priorytety: Low, Medium, High, Critical
- Terminy wykonania

### Formularze z walidacją
1. **Formularz projektu**: Nazwa (wymagana, max 100 znaków), Opis (opcjonalny, max 500 znaków), Daty, Status
2. **Formularz zadania**: Tytuł (wymagany, max 200 znaków), Opis (opcjonalny, max 1000 znaków), Projekt (wymagany), Priorytet, Status, Termin
3. **Rejestracja użytkownika**: Email, Hasło (min. 6 znaków, wymagana cyfra)

### API REST dla TaskItem

Endpointy API (wymaga autoryzacji):

- `GET /api/TaskItemsApi` - Lista wszystkich zadań
- `GET /api/TaskItemsApi/{id}` - Szczegóły zadania
- `POST /api/TaskItemsApi` - Utworzenie nowego zadania
- `PUT /api/TaskItemsApi/{id}` - Aktualizacja zadania
- `DELETE /api/TaskItemsApi/{id}` - Usunięcie zadania (tylko Admin)

Przykład użycia (wymaga tokenu autoryzacji):
```bash
curl -X GET http://localhost:5000/api/TaskItemsApi \
  -H "Authorization: Bearer {token}"
```

## Struktura bazy danych

### Encje

1. **Project** (Projekt)
   - Id, Name, Description, StartDate, EndDate, Status, OwnerId
   - Relacje: wiele TaskItems, wiele ProjectMembers

2. **TaskItem** (Zadanie)
   - Id, Title, Description, CreatedDate, DueDate, Priority, Status, ProjectId, AssignedToUserId
   - Relacje: jeden Project, wiele Comments

3. **ProjectMember** (Członek projektu)
   - Id, ProjectId, UserId, Role, JoinedDate
   - Relacje: jeden Project

4. **Comment** (Komentarz)
   - Id, Content, CreatedDate, TaskItemId, UserId
   - Relacje: jeden TaskItem

### Związki między encjami
- Project → TaskItems (1:N)
- Project → ProjectMembers (1:N)
- TaskItem → Comments (1:N)

## Docker - Polecenia pomocnicze

### Zatrzymanie aplikacji
```bash
docker compose down
```

### Restart kontenerów
```bash
docker compose restart
```

### Logi aplikacji
```bash
docker compose logs -f webapp
docker compose logs -f sqlserver
```

### Czyszczenie (usunięcie wolumenów z danymi)
```bash
docker compose down -v
docker compose up --build
```

## Troubleshooting

### Błąd połączenia z bazą danych
SQL Server potrzebuje ~20-30 sekund na start. Poczekaj chwilę i odśwież stronę.

### Połączenie z bazą przez SSMS/Azure Data Studio
- Server: `localhost,1433`
- Authentication: SQL Server Authentication
- Login: `sa`
- Password: `YourStrong@Passw0rd`
- Zaznacz: Trust server certificate

### Migracje nie zostały zastosowane
```bash
cd Task-Management-System
dotnet ef database update
```

## Autor
Projekt stworzony jako aplikacja edukacyjna.

