# Gringotts Bank API
A simple online banking API demo project.

## Prerequisites
 - [Docker](https://www.docker.com/get-started)
 - [Docker Compose](https://docs.docker.com/compose/install/)

## Architecture
This application is developed with .NET 5. Entity Framework is used as an ORM layer and PostgreSQL is used as a database. 
The domain-driven design approach is used for managing and validating business rules. [Concurrency Token](https://www.npgsql.org/efcore/modeling/concurrency.html) 
guarantees transaction consistency. Swagger serves the documentation of endpoints. 
GitHub Actions runs the deployment process.

## Demo
https://gringotts-bank-api.herokuapp.com/swagger/index.html

## Running locally
1. Clone the repo `git clone https://github.com/umutakturk/gringotts-bank.git`
2. Run `docker-compose -f docker-compose.yml -f docker-compose.override.yml up`
3. Go to https://localhost:5100/swagger/index.html

## License
MIT
