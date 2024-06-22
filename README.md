# apitel-bank
The mono repo for everything

## Editing ERD Diagram

Ensure to install the recommended VSCode extensions for this repo. In the diagrams folder there is db.dbml file. This is our ERD. Use VS Code to edit the ERD. The DBML extension will help you visualise the diagram as you edit it. You can learn more about DBML [here](https://dbml.dbdiagram.io/docs/).

## Running locally

Be sure to be in wsl or in a linux host then run `docker compose up` from the root folder. This will/should start all app services. This compose file is meant to only be used locally. A different approach should be used when deploying to the cloud.
