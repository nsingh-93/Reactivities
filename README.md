# Basic README for running Reactivities project

### Running the API project
- Open a terminal and cd into the API project folder (this is the startup project)
- In the terminal, run the following
    - ```dotnet run```
    > If there are issues with the certs then run the following to set it back up\
    > ```dotnet dev-certs https --clear```\
    > ```dotnet dev-certs https --trust```

### Creating and Applying EF Migration
- Open the terminal and cd to the solution folder (Reactivities)
- In the terminal, run the following
    - ```dotnet ef migrations add [name of migration file] -p Persistence -s API```
        - -p is the flag for the project folder in which the migration folder and file will be added to (where the DBContext is contained)
        - -s is the flag for the startup project

- To apply the migration, run the following in the terminal
    - ```dotnet ef database update -p Persistence -s API```
        - The -p and -s flags are the same as when creating the migration