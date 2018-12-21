using System;
using BTI7252.DataAccess;
using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;

namespace BTI7252_SmartHomeCommander
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// Bootstrap shit
			services.AddTransient<IMqttSender, MqttSender>();
			services.AddTransient<IMqttConnectionManager, MqttConnectionManager>();
			services.AddTransient<ICouchBucketManager, CouchBucketManager>();
			services.AddTransient<ICouchConnectionManager, CouchConnectionManager>(c =>
				new CouchConnectionManager(Configuration.GetValue<Uri>("Couchdb:Url"),
					Configuration.GetValue<string>("Couchdb:Username"),
					Configuration.GetValue<string>("Couchdb:Password")));
			services.AddTransient<ICouchRepository, CouchRepository>(c =>
				new CouchRepository(c.GetService<ICouchBucketManager>(),
					Configuration.GetValue<string>("Couchdb:BucketName")));

			// Create a new MQTT client.
			var factory = new MqttFactory();
			var mqttClient = factory.CreateMqttClient();
			services.AddSingleton(mqttClient);
			var mqttOptions = new MqttClientOptionsBuilder()
				.WithClientId($"CommandApi-{Guid.NewGuid()}")
				.WithCommunicationTimeout(TimeSpan.FromSeconds(Configuration.GetValue<int>("Mqtt:Timeout")))
				.WithKeepAlivePeriod(TimeSpan.FromSeconds(100))
				.WithCredentials(Configuration.GetValue<string>("Mqtt:User"),
					Configuration.GetValue<string>("Mqtt:Password"))
				.WithTcpServer(Configuration.GetValue<string>("Mqtt:Host"), 1883)
				.Build();

			services.AddSingleton(mqttOptions);

			services.AddSwaggerDocument();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
            {
				app.UseDeveloperExceptionPage();
            }
			else
            {
				app.UseHsts();
            }

			// Register the Swagger generator and the Swagger UI middlewares
			app.UseSwagger();
			app.UseSwaggerUi3();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}