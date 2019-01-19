# Design choices: 
## Why certain approaches were used, why others were not selected 
For this project I decided to use the latest tools (ASP.NET Core and Angular).
For database approach I decided to use Entity Framework Core using InMemory since it was required (now it can be easily change to other provider you want).
Also in order to help my development, I installed Swagger in order to retrieve the metadata from API and generate a page to test HTTP requests.

## Any design patterns used 
1. The application is completely layered. It's possible to see Controllers and Services layer in C# and the same in TypeScript with Components and Services.
2. REST endpoints are well defined and tried to accomplish a good definition for them using the HTTP verbs
3. The server side uses Dependency Injection. I decided to use the built-in in ASP.NET Core.
4. Tried to make use of ViewModels for the communication in the API in order to control what we expose on that.

## Anything extra you would have done give more time 
1. Unit tests for the services layer
2. Unit tests for the angular layer
3. E2E tests using Protractor
4. Better design (perhaps using Angular Material)
5. Robust authentication module
6. Monitoring stuff like Application Insights?!?

## Anything else you feel I should know 
This project was built using VSCode (IDE) and Kestrel (server). I didn't test if it works on IIS, but you can see it deployed in Azure Websites.
This happened because I used MacOS for development but I can use also Windows to work.
Accordingly with Coverlet, this project has 52.6% of Coverage.

For this project I don't try to grab exceptions (requests with error) in client side, that should be done to inform the user the action didn't work.