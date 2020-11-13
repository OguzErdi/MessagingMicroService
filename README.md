# MessagingMicroService
A small microservice orianted messaging app which is developed in .Net Core 3.1.  
- Contains two Web API; one is Message API, the other one is User API.  
JWT Token is used for authantication.  

**To run project Docker Desktop must be installed.**
**Go to solution root folder and run following command to start docker compose**
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
**After that, you can try api calls on swagger page of APIs**
Message API: [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html).  
User API: [http://localhost:9000/swagger/index.html](http://localhost:9000/swagger/index.html). 

**Also you can examine logs on Seq page**
Seq Log Page: [http://localhost:5341/#/events](http://localhost:5341/#/events).  



