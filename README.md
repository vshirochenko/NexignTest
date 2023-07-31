# NexignTest

## API

### Игроки
1. Создание игрока: [POST] http://localhost:5000/api/users  
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
2. Получение списка игроков: [GET] http://localhost:5000/api/users  
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
4. Ход в опред. раунде: [PUT] http://localhost:5000/api/games/{gameId}/rounds/{roundId}  
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
     "Result": "" // -1/0/1 (-1 - lost; 0 - draw; 1 - won)
   }
   ```