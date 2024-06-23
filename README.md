# apitel-bank
The mono repo for everything

## Editing ERD Diagram

Ensure to install the recommended VSCode extensions for this repo. In the diagrams folder there is db.dbml file. This is our ERD. Use VS Code to edit the ERD. The DBML extension will help you visualise the diagram as you edit it. You can learn more about DBML [here](https://dbml.dbdiagram.io/docs/).

## Running locally

Be sure to be in wsl or in a linux host then run `docker compose up` from the root folder. This will start all app services and restart them as you edit the source files. This compose file is meant to only be used locally. A different approach should be used when deploying to the cloud.

## Local Endpoints

When trying to connect to a service from another service, use the container name as the host name. Both the container name and ports can be found in the `compose.yml` file. When trying to connect from the host machine (localhost) to the running service, use localhost as the host name and the port found in `compose.yml` for that service. These are the localhost urls for the different services:

1. SQL Database -> localhost:1433
2. Kafka Message Broker -> localhost:9092
3. Kafka UI -> http://localhost:8080
4. Main Banking Service -> localhost:3001
5. Reporting Service -> localhost:3002
6. Bank Partner Service -> localhost:3003
7. Bank Manager Portal -> http://localhost:3000
   