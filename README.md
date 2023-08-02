# NexignTest

![CI](https://github.com/vshirochenko/NexignTest/actions/workflows/ci.yml/badge.svg)

## Локальный запуск

### Запуск api-сервиса
Запустите файл `run.ps1`

### Выполнение тестов
Запустите файл `test.ps1`

## API

### Игроки
1. Создание игрока: [POST] http://localhost:5000/api/players  
   Request: 
   ```
   {
     "Name": "" // string
   }
   ```
   Response:
   ```
   {
     "Id": "", // Guid
     "Name": "" // string
   }
   ```
2. Получение списка игроков: [GET] http://localhost:5000/api/players  
   Response:
   ```
   [
     {
       "Id": "", // Guid
       "Name": "" // string
     }
   ]
   ```
### Игра
1. Создание игры: [POST] http://localhost:5000/api/games  
   Response:
   ```
   {
     "Id": "" // Guid
   }
   ```
2. Подключение к игре: [POST] http://localhost:5000/api/games/{gameId}/join  
   Request:
   ```
   {
     "OpponentId": ""  // Guid
   }
   ```
3. Старт нового раунда: [POST] http://localhost:5000/api/games/{gameId}/rounds  
4. Ход в опред. раунде: [PUT] http://localhost:5000/api/games/{gameId}/rounds/{roundId}/turn    
   Request:
   ```
   {
     "PlayerId": "", // Guid
     "Turn": "" // 0/1/2 (Rock/Scissors/Paper)
   }
   ```
   Response:
   ```
   {
     "Result": "" // 0/1/2/3 (0 - NotReady, 1 - Won, 2 - Draw, 3 - Lost)
   }
   ```