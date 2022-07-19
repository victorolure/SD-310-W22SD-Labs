**CONNECTING SQL SERVER MANAGEMENT STUDIO TO A COMPUTER SQL EXPRESS SERVER**

To connect your SSMS to the Sql express server on your computer, you have to update 
the version of your windows then take the following steps

1. Download the 2 applications listed below from the links
	- Microsoft SQL Server Management Studio(Basic Version)
		https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15
		
	- SQL Server Express
		https://www.microsoft.com/en-us/sql-server/sql-server-downloads

2.	After the Installation of SQL Server Express
	- Copy the connection string that is on display and save it somewhere accessible(e.g notepad, sticky notes)

3. Open Microsoft SQL Server Management Studio
	- On the connection string you copied from SQL Server Express, Copy the server name and paste it into
	  the Server Name field.



** HOW TO CREATE A NEW DATABASE**

To create a new database in  Microsoft SQL Server Management Studio(SSMS), take the following steps;

1. Open SSMS
2. Open the Object Explorer in the tabs.
3. Right Click on Databases and select new database.
4. Provide your preferred database name and click on Add.
5. Refresh and Expand the Databases in the Object explorer.
6. Expand the newly created database and right-click on Tables folder and select New table.
7. Create columns on your table.
8. Ensure that each table has a unique Id set as it's primary key.
9. Set the Identity specification to Yes under IsIdentity.

**HOW TO MODEL AND CONNECT TO THE CREATED DATABASE IN ASP.NET MODEL-VIEW-CONTROLLER PROJECT(MVC)**

1. Open Microsoft Visual Studio and Create new Asp.Net MVC project
2. Click on Tools on the tool bar and hover over Nugget Package manager to select Package Manager Console
3. On the package manager console, run the following command
	- dotnet tool install --global dotnet-ef
	This is installed globally once and you do not have to install it every time you are creating a new project on the same machine.
4. Install the following 3 packages with the command;
	
	- Install-Package Microsoft.EntityFrameworkCore.SqlServer
	- Install-Package Microsoft.EntityFrameworkCore.Design
	- Install-Package Microsoft.EntityFrameworkCore.Tools



5. Right click on the database on SSMS 
	- click on properties. 
	- click on "View connection properties".
	- copy the server name.

6. Click Tools on the tool bar and select "Connect to database"
	- paste the server name you copied from the database connection properties
	- Select the name of the database you want to connect from the dropdown list of databases

7. On the package manager console, run the following commands(after following step 8)
	- Scaffold-DbContext "Server=['Server Name'];Database=['Database Name'];Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

8. Replace the ['Server Name'] with the server name you copied from the database connection properties and Replace
   the ['Database Name'] with the name of the database.

9. The command will scaffold the Models and a DbContext Class in the folder added to the -OutputDir Flag at the end of the command.

10. Go to the appsettings.json file and add the ConnectionStrings attribute.
	- "ConnectionStrings": {
			"ContextClassName": "ConnectionString"
	   }
11. Go to the Program.cs file to add services to the container
	- builder.Services.AddDbContext<DbContextName>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContextClassName")));


** THE ROLE OF THE CONTEXT DATABASE CLASS**

- The Context Database Class simply-put is a representation of the DATABASE.
- Its properties include DbSets of all the tables in the database.
- This means that each Dbset consists of a list of all the rows of each table.


** WHY DO WE REGISTER CONECTIONSTRINGS IN appsettings.json and program.cs**

- To be able to get a connection string to add a service on the program.cs file, it is required to add a connectionstring object
  in appsettings.json to access the connectionstring of the context to be used on builder.configuration on program.cs.


