# Basic README for running Reactivities project

### Running the API project
- Open a terminal and cd into the API project folder (this is the startup project)
- In the terminal, run the following
    - ```dotnet run```
    > If there are issues with the certs then run the following to set it back up\
    > ```dotnet dev-certs https --clear```\
    > ```dotnet dev-certs https --trust```

- Can also use the following to keep the API running and have it hot reload when changes are made
    - ```dotnet watch```

### Creating and Applying EF Migration
- Open the terminal and cd to the solution folder (Reactivities)
- In the terminal, run the following
    - ```dotnet ef migrations add [name of migration file] -p Persistence -s API```
        - -p is the flag for the project folder in which the migration folder and file will be added to (where the DBContext is contained)
        - -s is the flag for the startup project

- To apply the migration, run the following in the terminal
    - ```dotnet ef database update -p Persistence -s API```
        - The -p and -s flags are the same as when creating the migration

### Running the React project
- Open a terminal and cd into the client project folder (this is the React project)
- In the terminal, run the following
    - ```npm run dev```
        - The output in the terminal will show which port it is running on


### Notes on Bruno for Identity
- Using Bruno, the JWT does not save as a cookie automatically as it does in the course
    - There needs to be an extra post response script added to the login with JWT to make it work
        - ```bru.setVar("accessToken", res.body.accessToken);```
    - Then in the collection, the authorization needs to be set to Bearer and then {{accessToken}} will be the token
    - When using the login with cookie = true, then any requests that need the cookie need to have their specific request auth set to no authfor it to work