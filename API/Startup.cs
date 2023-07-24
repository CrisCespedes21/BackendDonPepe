namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }   
        public Startup(IConfiguration configuration) 
        {   
            this.Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddOptions(); 
            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIService v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
                options => options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            this.Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
