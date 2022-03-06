# .Net Mvc Library Project

To spin up mssql docker image

`docker-compose -f docker-compose.mssql.yml up -d`

Or If you want to use postgres You should change AddDbContext in Program.cs
```
builder.Services.AddDbContext<LibraryContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryContextPostgres"))
// options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryContextMsSql"))
);
```
And To spin up postgres docker image

`docker-compose -f docker-compose.postgres.yml up -d`

## Java Selenium Bot-Process

In order to populating to author and books table in to the database
Simply open the terminal and type below.

`mvn spring-boot:run`

and then hit the 
`http://localhost:8080/seed` from browser and wait.

### Database Populating Lifecycle

- Extracting to book and author information with selenium.
- Send the RabbitMQ
- .Net  application consume to this queue
- .Net application save to data to mssql or postgres database



