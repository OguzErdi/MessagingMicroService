# MessagingMicroService
A small microservice orianted messaging app which is developed in .Net Core 3.1.
- Contains two Web API; one is Message API, the other one is User API. Both project architecture is based on Clean Architecture.
- JWT Bearer Token was used for authentication.
- Message Project has Unit Test project.

**To run project Docker Desktop must be installed.**
**Go to solution root folder and run following command to start docker compose**
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
**After that, you can try api calls on Swagger pages of APIs**

- Message API: [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html).
  - **To get last message from user**: /api/Message/{senderUsername}
  - **To get message history**: /api/Message/History/{withWhom}
  - **To send message to user**: /api/Message/{senderUsername}

- User API: [http://localhost:9000/swagger/index.html](http://localhost:9000/swagger/index.html).
  - **To register**: /api/User/Register
  - **To get JWT token**: /api/User/Login
  - **To block user**: /api/User/Block/{username}
  - **To find out that you have blocked the user**: /api/User/IsBlockedUser/{username}
  - **To find out you have blocked by whom**: /api/User/IsBlockedByUser/{username}
  - **To find out is user exist**: /api/User/IsExist/{username}

**Also you can examine logs on Seq page**
- Seq Log Page: [http://localhost:5341/#/events](http://localhost:5341/#/events).



