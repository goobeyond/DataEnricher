# Data Enricher
## Intro
This application enriches CSV data by appending results from the GLEIF endpoint, making calculations based on legal country of entity and outputing them as a separate CSV file.

## Assumptions
- Everything is to be hosted on an Azure environment.
- All fields are manadatory, a missing field will result in that row being skipped and an error being logged.
- The results from the GLEIF endpoint remain the same during the enrichment run.
- All fields are clean, any mis-typed element means the row is skipped and error is logged.

The application goes through the input file line by line and attempts to read and enrich it. If a line fails conversion, it will be logged in the console. Then enrichment is attempted. the GLEIF endpoint returns not data, an error is logged, but the row will be in the output. If data is returned but contains unsupported elements, such as legal country. It will be reflected in the output.
The response from the GLEIF endpoint is cached for the duration of the run. This is to prevent hitting the rate-limit (60 calls per minute) too quickly. After every 60 rows, a pause is made via `Thread.Sleep` to make sure we don't hit the rate-limit.

## How to run
The application will run as a console app. The input will be a file called `data.csv` in the root folder of the application. The output will be in the same folder, but called `enriched-data-<date>.csv`.

## Limitations
The application is extremely rudimentary, missing a lot user friendly features. Also it could be further optimized by sleeping the app more effectively around the cache. We could also provide a status for the more larger data sets so the users can know when their enrichment is done.

# Deployment
The application itself can be set up to have ci/cd pipelines in azure DevOps and deployed as a function app. We can take advantage of azure insights to keep logs. We can setup alerts on the logs, to inform us when a transformation is failing.

In its current form, this application can theoretically enrich an input of any size. If we were to implement a mechanism to display the progress of enrichment, we can create an endpoint that allows users to upload their file, where we will store in a cloud storage solution, spin up a new instance of this application per request and store the output in the cloud. Users can then download their enrciched data whenever they please.

There's a limitation with this approach, which is the time limit on the life of a function app. To address this we can make use of more sophisticated orchestrators such as the Azure Data Factory or Airflow.