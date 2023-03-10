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
---
### Done

1. Two microservices with basic GET methods fetching data from SQL Server (above) implemented
1. Api gateway implemented with `Ocelot`

---
### TODO

1. Service discovery
1. Microservice communication - Calling and validating data from `BookService` while adding `Subscription`
1. Logging

---

### External dependencies
- download Consul from https://releases.hashicorp.com/consul/1.15.0/consul_1.15.0_windows_386.zip (for windows)
- start `consul.exe agent --dev`
- It will start `Consul` at http://localhost:8500
- Register 