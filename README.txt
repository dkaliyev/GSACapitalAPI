Name: Daniyar Kaliyev

How to use:
* Open GSACapitalAPI solution in Visual Studio
* Start GSACapitalAPI project
* Kestrel server should be running on port 8000

Libraries used:
* Entity Framework - object-relation mapping/database access
* MSTest - unit testing

Solution consists of 5 different projects:
* GSACapitalAPI - Main application containing the logic for API controllers. Starts the web service on port 8000
* DTO - contains data transfer objects. 
* DataAccess - contains interfaces and classes implementing Repository and Unit of Work design patterns. The project also includes database entity classes and Entity Framework context class.
* Utilities - contains helper classes to perform mapping from text files into DTOs and also calculator classes. 
* Utilities.Test - project for unit testing Utility classes

Data is stored in LocalDB(v11) within MyDatabase database using Windows authentification.
Capitals with 0 value are not used in Compound daily returns calculations. 