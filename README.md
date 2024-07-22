# Instructions to deploy
Commands are to be run from this (solution) directory
```bash
# build and boot the docker container
docker compose up --build -d
# run DB migrations
dotnet ef database update --project TaxTool
````
You can now browse to the below URL to interact with the API
```bash
http://localhost:8080/swagger/index.html
```
