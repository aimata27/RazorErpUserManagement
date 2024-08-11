Here's my Pre-Interview Task.

GitHub Repositories
•	API - https://github.com/aimata27/RazorErpUserManagement.git
•	DB - https://github.com/aimata27/RazorErpUserManagementDatabase.git

Please take note on the following items for API:
1.	API Solution is on .Net 8.0
2.	JWT Authentication - Only "admin" and "user" roles are accepted on the backend code.
3.	Throttling Middleware - Applied Rate Limiter (Fixed Window) with 10 requests per minute. Exceeding the number of requests will result a 429 status code (too many requests).
4.	Applied SOLID principle.
5.	Applied OOP and Dependency Injection.
6.	Debugged/launched this on IIS Express only since this is only for test purposes.

For DB:
1.	Initially inserted data for the "admin" role just to access the admin endpoints. 
2.	Since the requirement for the companies are added outside of the API, I also initially created a table for Companies and inserted 3 sample data. 
3.	This could've been automatically implemented if I use EF Core Code First Approach but the requirement provided for data access is only Dapper. 
I also provided the necessary files (test screenshots, sql scripts, postman collection), if only needed.

If you have any questions please don't hesitate to contact me. Thank you so much and good luck to me! 
