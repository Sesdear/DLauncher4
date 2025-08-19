## ru
# Гайд на serverside DristLauncher'a 4

### 1. Список серверов
Файл `servers_index.json`, расположение `./servers_index.json`
Наполнение файла:
```
{
    "Server1": "ExampleServer1",
    "Server2": "ExampleServer2",
    "Server3": "ExampleServer3",
    "Server4": "ExampleServer4"
}
```
Тут находится список серверов, каждый пункт указывает на папку в корне сервера

### 2. Главный файл сервера
Файл `data.json`, расположение `./ExampleServer/data.json`
Наполнение:
```
{
	"ServerName":"Название сервера", 
	"Description":"Описание сервера",
	"Image":"Картинка к серверу",
	"MVersion":"Версия майнкрафта",
	"MModLoader":"Название мод лоадера майнкрафта",
	"MModLoaderVersion":"Версия мод лоадера майнкрафта",
	"ModPackSize":"Вес модпака",
	"ModPackVersion":"Версия модпака",
	"ModPackFiles":"minecraft" 
}
```
`ServerName`: Обязательно \
`Description`: Обязательно
`Image`: Обязательно
`MVersion`: Обязательно
`MModLoader`: Обязательно
`MModLoaderVersion`: Обязательно
`ModPackSize`: Опционально
`ModPackVersion`: Опционально
`ModPackFiles`: НЕ ТРОГАТЬ

### 3. Обновление сборки
##### ⚠️️ НЕ ЗАГРУЖАТЬ РАСПОКОВАНЫЕ РЕСУРС ПАКИ
##### ⚠️ В ПАПКЕ С СЕРВЕРОМ ДОЛЖЕНА БЫТЬ ПАПКА minecraft 
##### 1. Файл апдейтер
Инструкция по запуску:
```
python3 FileUpdater.py <Путь до директории>
```
Пример:
```
python3 FileUpdater.py ExampleServer
```
В указаной папке создается файл `filesManifest.json`
##### 2. Мод апдейтер
Инструкция по запуску:
```
python3 ModsUpdater.py <Путь до директории>
```
Пример:
```
python3 ModsUpdater.py ExampleServer
```
В указаной папке создается файл `modsManifest.json`

В принципе всё



### eng
# Guide for Serverside DristLauncher4

### 1. Server List
File: `servers_index.json`  
Location: `./servers_index.json`

Example content:
```json
{
    "Server1": "ExampleServer1",
    "Server2": "ExampleServer2",
    "Server3": "ExampleServer3",
    "Server4": "ExampleServer4"
}
```
This file contains the list of servers. Each entry points to a folder in the server root directory.

### 2. Main Server File

File: `data.json`
Location: `./ExampleServer/data.json`

Example content:

{
    "ServerName": "Server name",
    "Description": "Server description",
    "Image": "Server image",
    "MVersion": "Minecraft version",
    "MModLoader": "Minecraft mod loader name",
    "MModLoaderVersion": "Minecraft mod loader version",
    "ModPackSize": "Modpack size",
    "ModPackVersion": "Modpack version",
    "ModPackFiles": "minecraft"
}


`ServerName`: Required
`Description`: Required
`Image`: Required
`MVersion`: Required
`MModLoader`: Required
`MModLoaderVersion`: Required
`ModPackSize`: Optional
`ModPackVersion`: Optional
`ModPackFiles`: DO NOT CHANGE

### 3. Updating the Modpack

⚠️ DO NOT upload unpacked resource packs!
⚠️ The server folder must contain the minecraft directory!

##### 1. File Updater

Run with:
```
python3 FileUpdater.py <Path to directory>
```

Example:
```
python3 FileUpdater.py ExampleServer
```

This will generate a `filesManifest.json` inside the specified directory.

##### 2. Mods Updater

Run with:
```
python3 ModsUpdater.py <Path to directory>
```

Example:
```
python3 ModsUpdater.py ExampleServer
```

This will generate a `modsManifest.json` inside the specified directory.

That’s it ✅
