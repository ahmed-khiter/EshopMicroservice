
var builder = WebApplication.CreateBuilder(args);
//add Services to  the Container 

var app = builder.Build();

//Configure the http request pipeline


app.Run();


