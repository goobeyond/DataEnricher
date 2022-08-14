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
Make sure the startup project is set to `DataEnricher.Console`. The input will be a file called `data.csv` in the root folder of the application. The output will be in the same folder, but called `enriched-data-<date>.csv`.

There are other data files in the root folder than can be used to test other scenarios for the application.
- data-broken: contains broken rows of different type. either a value is missing or an invalid LEI is provided.
- data-large: a larger data set to ensure application can handle larger sets.

## Limitations
The application is extremely rudimentary, missing a lot user friendly features. Also it could be further optimized by sleeping the app more effectively around the cache. We could also provide a status for the more larger data sets so the users can know when their enrichment is done.

# Deployment
The application itself can be set up to have ci/cd pipelines in azure DevOps and deployed as a function app. This pipeline would run all tests and ensure code quality is kept high by running code analyzers and test coverage checks on each pull request. We can take advantage of azure insights to keep logs. We can setup alerts on the logs, to inform us when transformations are failing.

In its current form, this application can theoretically enrich an input of any size. If we were to implement a mechanism to display the progress of enrichment, we can create an endpoint that allows users to upload their file, where we will store in a cloud storage solution. This can be used as a trigger to run the function app, apply the enrichment, and store the new file back into the storage. Users can then download their enrciched data when its ready.

There's a limitation with this approach, which is the time limit on the life of a function app. To address this we can make use of more sophisticated orchestrators such as Airflow.