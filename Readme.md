# .Net Mvc Library Project

## LOCAL ENVIRONMENT

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

---
## DOCKER ENVIRONMENT

- Go to compose directory

`cd compose`

- First build and run databases, selenium and rabbitmq

`docker-compose -f infrastructure.yml up -d --build`

- Second build and run spring and .net app

`docker-compose -f services.yml up -d --build`

- you can access web application --> `localhost:5000`
- and scraping app --> `localhost:8080`

---
**_Admin Credentials To Access To Web App_**

| Email           |  Password  |  
| -------------   |:----------:| 
| admin@admin.com | 123_Secret |  
---
## Java Selenium Bot-Process
To spin up RabbitMq and selenium driver

`cd scraping.library`

`docker-compose up -d`

In order to populating to author and books table
Simply open the terminal 

- Go to java project

`cd scraping.library`

- To Run Project 

`mvn spring-boot:run`

### For scraping best books
Hit the url from a browser

`http://localhost:8080/seed` 

and wait

### For scraping specific book by title
- Open to mvc project and go to seeder url
- Type desired book title and submit
- After one or two minutes the book and book's author information should be saved to the database
- Check out the gif below

![Scraping By Title](./screenshots/seeder_library.gif)

---


### Database Populating Lifecycle

- Extracting to book and author information with selenium.
- Send the RabbitMQ
- .Net  application consume to this queue
- .Net application save to data to mssql or postgres database



