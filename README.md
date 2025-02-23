# D365IntegrationFunctionApp
This Azure service bus triggered function app in the isolated worker model integrates Dynamics 365 F&O with SFTP server. Basically this app has three layers: Model, Service, and the app entrance.

Several Azure components are used in this app. First of all, sensitive data used in this app such as sftp username & password are managed by Azure key Vault.  Moreover, file at-rest and in-transit are encrypted. The Retry pattern is applied in case SFTP site temporarily unavailable without the need to resubmit from D365. This application also integrates with Application insights which can send telemetry from the application to Azure portal so that we can analyze the performance and usage of the application.

