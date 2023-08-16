First download and drop the database inside data folder in backend-dotnetcore/data 



How to run with our bash scripts ( with docker installed , assume running on linux )
There are 3 files with the .sh extension. First we need to turn the files into executables by these 3 commands

```
chmod +x 1databaseSetup.sh
chmod +x 2dotnetSetup.sh
chmod +x 3npmReact.sh
```
After these 3 files turn into executables , then simple run in (3 different terminal tabs the following commands)

sudo ./1databaseSetup.sh (This will setup the database )
sudo ./2dotnetSetup.sh (This is will setup the dotnet (run the backend)) - For backend database "http:localhost:3001"
sudo ./3npmReact.sh (This will install the modules needed and run the localhost:3001) For frontend "http:localhost:3000"

Choose your favourite browser. 
Type in the url - http.localhost:3000
Enjoy!

P.S - In case mariadb returns an error (already binded) manually run "sudo systemctl stop mariadb"
      
