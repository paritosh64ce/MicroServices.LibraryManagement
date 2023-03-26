# MicroServices.LibraryManagement
 Simple library management solution using microservices with .Net


- ### Book microservice @ http://localhost:3001
- ### Subscription microservice @ http://localhost:5001

- ### Api Gateway @ http://localhost:8001

---
- http://localhost:8001/gateway/book/1 ==> http://localhost:3001/api/book/1
    ```
    {
        "id": 1,
        "name": "Da Vinci Code",
        "author": "Dan Brown",
        "availableCopies": 5,
        "totalCopies": 5
    }
    ```
- http://localhost:8001/gateway/sub/Paritosh ==> http://localhost:5001/api/subscription/Paritosh
    ```
    [
        {
            "id": 1,
            "subscriberName": "Paritosh",
            "dateSubscribed": "2023-03-03T16:42:15.277361",
            "dateReturned": null,
            "bookId": 1
        }
    ]
    ```
- POST http://localhost:5001/api/subscription
    Request Payload: 
    ```
    {
        "SubscriberName": "Postman 2",
        "BookId": 3,
        "DateSubscribed": "2023-03-13"
    }
    ```
    Response: 
    ```
    {
        "id": 16,
        "subscriberName": "Postman 2",
        "dateSubscribed": "2023-03-13T17:39:00.0571982Z",
        "dateReturned": null,
        "bookId": 3
    }
    ```
---
---

### Done

1. Two microservices with GET, POST methods fetching data from SQL Server (above) implemented.
1. Microservice communication - Calling and validating data from `BookService` while adding `Subscription`.
1. Api gateway implemented with `Ocelot`.
1. Microservice communication with Retry and Circuit breaker using `Polly`.
1. Service discrovery with `Consul` (fully dynamic), `Ocelot`.
1. Removed hard-coded BookServiceUrl from SubscriptionService appsettings.json; get it from Consul registry instead.
1. Added docker-compose for bootstrapping `Consul`, `Kibana` and `ElasticSearch`.
1. Logging using `Serilog` with `ELK`.

---
### TODO

1. All above points done!
1. Microservice instance selection from Gateway in Round-robin fasion (`Ocelot` Cconfiguration).

---

### External dependencies
- download Consul from https://releases.hashicorp.com/consul/1.15.0/consul_1.15.0_windows_386.zip (for windows)
- start `consul.exe agent --dev`
- It will start `Consul` at http://localhost:8500
- Register from Startup files of microservices

- Alternatively, just do `docker-compose build`, `docker-compose up` to start `Consul`, `ElasticSearch` and `Kibana` all at once
  - The services may take some time initially to start. Validate below URLs.
  - Consule: http://localhost:8500
  - ElasticSearch: http://localhost:9200
  - Kibana: http://localhost:5601

- Fire some requests from Swagger or Postman and validate logs from `Kibana` home > Stack management > Index management
- Search the logs from `Kibana` home > Dev tools > Console and request `GET books-yyyy-MM/_search`

---
---

## Validating Service Discovery

### Start multiple instances of service from command prompt
- dotnet Api.Books.dll --urls `http://localhost:<PORT>` (usually 3001, 3002, etc)
- dotnet Api.Subscriptions.dll --urls `http://localhost:<PORT>` (usually 5001, 5002, etc)
- dotnet api.gateway.dll --urls `http://localhost:8001`

    > We also need to update `ConsulConfig.ServiceAddress` in `appsettings.json` so that the service gets registered with `Consul` with port number (for Book & Sub service)
    > `--urls` cli param is used for bootstrapping the service with appropriate URL
    > Hence, after starting one instance of a service, we need to update `appsettings.json` with new `ServiceAddress` before starting another instance.

    > ServiceId is generated with : {serviceName}_{uri.Host}:{uri.Port}

#### Another alternative to update appconfig.json is to provide the env variable from cli itself

> dotnet Api.Books.dll --urls http://localhost:3001 --ConsulConfig:ServiceAddress=http://localhost:3001
> dotnet Api.Books.dll --urls http://localhost:3002 --ConsulConfig:ServiceAddress=http://localhost:3002

> dotnet Api.Subscription.dll --urls http://localhost:5001 --ConsulConfig:ServiceAddress=http://localhost:5001

- Now try to consume http://localhost:5001/api/subscription POST request multiple times
- You'll see that random instance of BookService is being selected (between 3001 & 3002)

> dotnet api.gateway.dll --urls http://localhost:8001


---
---

## References
- https://swimburger.net/blog/dotnet/how-to-get-aspdotnet-core-server-urls#how-to-get-aspnet-core-server-urls-in-programcs-with-minimal-apis
- https://www.michaco.net/blog/ServiceDiscoveryAndHealthChecksInAspNetCoreWithConsul
- https://developer.hashicorp.com/consul/docs/services/discovery/dns-dynamic-lookups
- https://www.c-sharpcorner.com/article/logging-with-elasticsearch-kibana-serilog-using-asp-net-core-docker/