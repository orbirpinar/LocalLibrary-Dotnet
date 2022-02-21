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
