using MovieActorLookup.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton(new MovieService("https://api.themoviedb.org/3", "45986ef3bc3be0f98701339092f28ce3", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NTk4NmVmM2JjM2JlMGY5ODcwMTMzOTA5MmYyOGNlMyIsInN1YiI6IjY1YzU3MGVhNTM0NjYxMDE2MjhhY2VlZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.UK_3zXedq_xs_ma1zCbh6-s0bEKdlUH8XWkpAWpDLZ0"));
builder.Services.AddSingleton(new ShowService(
    "https://api.themoviedb.org/3",
    "45986ef3bc3be0f98701339092f28ce3",
    "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0NTk4NmVmM2JjM2JlMGY5ODcwMTMzOTA5MmYyOGNlMyIsInN1YiI6IjY1YzU3MGVhNTM0NjYxMDE2MjhhY2VlZCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.UK_3zXedq_xs_ma1zCbh6-s0bEKdlUH8XWkpAWpDLZ0"
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
