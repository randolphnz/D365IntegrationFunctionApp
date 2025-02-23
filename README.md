# D365IntegrationFunctionApp
This Azure service bus triggered function app in the isolated worker model integrates Dynamics 365 F&O with SFTP server. Basically this app has three layers: Model, Service, and the app entrance.

Several Azure components are used in this app. First of all, sensitive data used in this app such as sftp username & password are managed by Azure key Vault.  Moreover, file at-rest and in-transit are encrypted. The Retry pattern is applied in case SFTP site temporarily unavailable without the need to resubmit from D365. This application also integrates with Application insights which can send telemetry from the application to Azure portal so that we can analyze the performance and usage of the application.

Roughly spent 4 hours in solution design followed by anoter 4 hours for implementation and integration test.  

Following are what I would have done if I had more time:

1. To do more Unit Test
2. To apply more design patterns like CQRS
3. To do parallel data operation
4. To refine codes for telemetry
