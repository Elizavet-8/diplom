# Веб-сервер NodeJS для проекта Smart Parking
Короткое описание проекта.

## Что необходимо установить?
Необходимо установить [MongoDB Server 3.2.22](https://www.mongodb.com/try/download/community).

Для запуска тестов, сборки и локального сервера требуются глобально установленный Node.js 6.9.4
([https://nodejs.org/download/release/v6.9.4/](https://nodejs.org/download/release/v6.9.4/))

## Для разработки
1) Без базы данных данное серверное API бесполезно, поэтому
   сначала необходимо локально запустить MongoDB Server (`mongod`, обычно лежит в `<путь до папки с установленным MongoDB>\Server\3.2.22\bin`).
   По умолчанию он запускается на 27017 порту - таким его и надо оставить.

2) Запуск локального NodeJS-сервера - Запустить файл `scripts\server.cmd`.

Этот файл установит все зависимости, проверит базу данных и затем запустит сервер.

Логин по умолчанию `admin`

Пароль по умолчанию `123`

Веб-интерфейс (и API) проекта будет доступен на `8080` порту ([http://localhost:8080/](http://localhost:8080/)).

**ВАЖНО!** Собранный веб-интерфейс находится в папке `\release`. Чтобы версия была самая свежая, вы можете собрать проект `sp-web` и заново скопировать папку `\release` в проект `sp-api`. Чтобы файлы фронтенда нормально грузились, необходимо вручную добавить текст `/release/` ко всем ссылкам в файле `release/index.html` (чтобы путь брался из папки release. Например, `/release/public/...` или `/release/styles.css`). В дальнейшем исправление ссылок можно сделать автоматическим, при сборке фронтенда.

## Дополнительные команды

### Для установки зависимостей запустите файл:

`scripts\install_dependencies.cmd`

### Для приведения базы данных к начальному состоянию:
1) Запустить MongoDB Server (`mongod`, обычно лежит в `<путь до папки с установленным MongoDB>\Server\3.2.22\bin`).
   По умолчанию он запускается на 27017 порту - таким его и надо оставить.

2) Запустить файл `scripts\reset_db.cmd`

### Для проверки наличия необходимых коллекций (и их создания при необходимости):
1) Запустить MongoDB Server (`mongod`, обычно лежит в `<путь до папки с установленным MongoDB>\Server\3.2.22\bin`).
   По умолчанию он запускается на 27017 порту - таким его и надо оставить.

2) Запустить файл `scripts\bootstrap_db.cmd`