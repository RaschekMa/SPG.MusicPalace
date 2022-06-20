using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.AlbumApp;
using Spg.MusicPalace.Application.ArtistApp;
using Spg.MusicPalace.Application.SongApp;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Infrastructure;
using SPG.MusicPalace.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureSqLite();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<MusicPalaceDbContext>();
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IDateTimeService, DateTimeService>();

builder.Services.AddTransient<IRepositoryBase<Song>, RepositoryBase<Song>>();
builder.Services.AddTransient<IRepositoryBase<Album>, RepositoryBase<Album>>();
builder.Services.AddTransient<IRepositoryBase<Artist>, RepositoryBase<Artist>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
