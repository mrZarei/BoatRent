# BoatRent :speedboat:
## Assumptions 
In this task i have assumed that:
- There is no validation on boat existence and if a rented boat does not exist in our database it would be added first.
- Our `HourlyFee` and `BasicFee` are constants and the same for all types of boats.

## Projects details
Our solution consist of four projects:
- `BoatRent.Core`
- `BoatRent.Core.Test`
- `BoatRent.Data`
- `BoatRent.Web`
### BoatRent.Core
this project is our main project which implements the task and includes our `Domains`, `Service`, and `Repository Interface` which is responsible for the persistence of the data.
In the Core project, we take advantage of `Reflection` to implement our Boat Factory and generate different boat type classes.
### BoatRent.Core.Tests
This is where our core unit tests live inside the solution as its name implies. I used a MSTest project in order to implement the tests.
### BoatRent.Data
In this project, I tried to show an example of the implementation of the `IBoatRentalRepository` that is the core's repository interface. I used the `EntityFramwork Core` 
for this purpose and the `Code first` approach.
### BoatRent.Web
This is a simple UI for our task. This UI has been implemented via Asp.net Core MVC and Razor views.\
\
:rocket::rocket:_In the end, I should remind you that this project is not fully tested and bug-free and the main purpose of that is to show the general idea of separation of concern, TDD pattern, Factory Pattern, 
Domain-Driven Design Pattern, etc._ :)
