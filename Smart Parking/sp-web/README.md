# Веб-интерфейс для проекта Smart Parking
Короткое описание проекта.

## Что нужно установить?
Для запуска тестов, сборки и локального сервера требуются глобально установленный Node.js 6.9.4
([https://nodejs.org/download/release/v6.9.4//](https://nodejs.org/download/release/v6.9.4/))

## Разработка
Запуск локального сервера webpack-dev-server в режиме "hot". Запустите файл:

`scripts\server.cmd`

Приложение доступно на `8081` порту ([http://localhost:8081/](http://localhost:8081/))

## Сборка рабочей версии
`scripts\build.cmd`

Приложение собирается в папку `\release`
Для запуска release-версии, папку `\release` надо скопировать в проект `sp-api` и запустить файл

`sp-api\scripts\server.cmd`

В этом случае приложение доступно на `8080` порту ([http://localhost:8080/](http://localhost:8080/))

## Дополнительные команды

### Для установки зависимостей запустите файл:

`scripts\install_dependencies.cmd`