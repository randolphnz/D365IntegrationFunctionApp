# D365IntegrationFunctionApp
This Azure function app is in the isolated worker model and uses dependency injeciton. 

The application has three layers: Model, Service, and function app entrance. The Retry pattern is applied in case SFTP site temporarily unavailable without the need to resubmit from D365. File at-rest and in-transit are encrypted. This application also integrates with Application insights which can send telemetry from the application to Azure portal so that we can analyze the performance and usage of the application.

Bascially spent 4 hours in designing and another 4 - 5 hours in implementation.  

Following are what I would have done if I had more time:

1. To create Unit Test project
2. To apply more design patterns like CQRS
3. To do parallel data operation
4. To refine telemetry codes
