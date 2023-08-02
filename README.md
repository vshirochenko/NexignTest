# NexignTest

![CI](https://github.com/vshirochenko/NexignTest/actions/workflows/ci.yml/badge.svg)

## Правила игры:
Игра проводится между двумя игроками в 5 раундов. Игра может завершиться досрочно (если будет понятно, что один из игроков "сильно вырвался вперед").  
В каждом раунде игроки могут выполнить только по одному ходу (выбросив камень, ножницы или бумагу).
Одна из особенностей - очередной раунд необходимо создавать вручную, дергая api. Кол-во раундов по ум. - 5.  
Результат каждого раунда может иметь 3 результата: поражение, ничья и победа. 

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
2. Получение списка игр: [GET] http://localhost:5000/api/games  
   Response:
   ```
   [
     {
       "Id": "" // Guid
     }
   ]
   ```
3. Подключение к игре: [POST] http://localhost:5000/api/games/{gameId}/join  
   Request:
   ```
   {
     "OpponentId": ""  // Guid
   }
   ```
4. Старт нового раунда: [POST] http://localhost:5000/api/games/{gameId}/rounds  
5. Ход в опред. раунде: [PUT] http://localhost:5000/api/games/{gameId}/rounds/{roundId}/turn    
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