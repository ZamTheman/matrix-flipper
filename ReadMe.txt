Project is using an Angular front-end, .NET5 back-end and SQL-Server for storage.

Prerequisites:
Node 16.3 and updated version of NPM
Updated version of Angular and Angular CLI
DOTNET5
SQL Server
Use Import Data-Tier application in MSSM to restore the matrix-flipper-db.bacpac file

Url for API can be set in the environment.ts files. Current values is for a localhost VisualStudio running the API.
Database connection string can be set in the appsettings.json.

Considerations done:
The front-end is created to be able to be used from a desktop computer screen down to a rather small mobile phone. The limiting factor will be when it is no longer viable to click the individual cells. It also built with support for other sizes than 6x6 since that seems to be a natural next step. An enum is used for the states to make it rather easy to add aditional ones when needed.

The back-end is using a simple 3 layer architecture with Controller, Services and DataAccess. I have used dependency injection to avoid hard couplings and for potential unit-tests of individual classes. 

The DB structure is super simple and is basically emulating a document database structure.

Potential enhancement:
* The CSS and selectors could be structured in a better way. Currently it is focused on getting the design ok
* The front-end could be broken down into smaller components
* Both the back-end and front-end could implement unit-test
* If the dataobjects would need more relationsships the simple json-structure storage could be broken up into more traditional table structures
