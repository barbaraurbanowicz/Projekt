# Projekt - Barbara Urbanowicz 
# SpendingTracker
## Spis treści
* [Wstęp](#wstep)
* [Stack technologiczny](#stack-technologiczny)
* [Uruchomienie](#uruchomienie)


## Wstęp <a name="ogolne"></a>
Projekt na zaliczenie przedmiotów 
* Programowanie aplikacji backendowych
* Programowanie obiektowe

Aplikacja służąca do śledzenia wydatków oraz przychodów oraz sumowanie ich.

## Stack Technologiczny <a name="stack-technologiczny"></a>
* ASP.NET Web API .NET 6.0
* SQLServer 2019
* Docker
* WPF
* EntityFramework 
* Swagger

## Uruchomienie <a name="uruchomienie"></a>

### Aby uruchomić projekt należy w konsoli wpisać
```
docker-compose up --build
```
Komenda  ta pozwoli na stworzenie kontenerów z aplikacją API oraz bazą danych.
Następnie musimy uruchomić projekt WPF.


## Lista endpointów
### User
* /api/users/register - POST
* /api/users/login- POST
### Expense
* /api/expenses - GET 
* /api/expenses - POST
* /api/expenses/{id} - GET
* /api/expenses/{id} - PUT
* /api/expenses/{id} - DELETE
### Income
* /api/incomes - GET
* /api/incomes - POST
* /api/incomes/{id} - GET
* /api/incomes/{id} - PUT
* /api/incomes/{id} - DELETE
### Expense Category
* /api/expense/categories - GET
* /api/expense/categories - POST
* /api/expense/categories/{id} - GET
* /api/expense/categories/{id} - PUT
* /api/expense/categories/{id} - DELETE
### Income Category
* /api/income/categories - GET
* /api/income/categories - POST
* /api/income/categories/{id} - GET
* /api/income/categories/{id} - PUT
* /api/income/categories/{id} - DELETE
