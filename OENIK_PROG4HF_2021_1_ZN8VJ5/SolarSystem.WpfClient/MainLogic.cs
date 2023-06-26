namespace SolarSystem.WpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Main logic that calls the API.
    /// </summary>
    public class MainLogic : IMainLogic, IDisposable
    {
        private string url = "https://localhost:44394/PlanetsApi/";
        private HttpClient client = new HttpClient();
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        /// <summary>
        /// Calls the API and retursn all planets in db.
        /// </summary>
        /// <returns>All planets in db as VM planet.</returns>
        public IReadOnlyCollection<VMPlanet> ApiGetAllPlanets()
        {
            string json = this.client.GetStringAsync(this.url + "all").Result;
            var list = JsonSerializer.Deserialize<List<VMPlanet>>(json, this.jsonOptions);
            return list;
        }

        /// <summary>
        /// Calls the API to delte a planet form the db.
        /// </summary>
        /// <param name="planet">Planet to be deleted.</param>
        public void ApiDeletePlanet(VMPlanet planet)
        {
            bool success = false;
            if (planet != null)
            {
                string json = this.client.GetStringAsync(this.url + "delete/" + planet.Id.ToString()).Result;
                JsonDocument doc = JsonDocument.Parse(json);

                success = doc.RootElement.EnumerateObject().First().Value.GetRawText() == "true";
            }

            this.SendMessage(success);
        }

        /// <summary>
        /// Edits a planet with the given edit function.
        /// </summary>
        /// <param name="planet">Planet to be edited.</param>
        /// <param name="editor">Edit function that carries out the modification.</param>
        public void EditPlanet(VMPlanet planet, Func<VMPlanet, bool> editor)
        {
            VMPlanet clone = new VMPlanet();
            if (planet != null)
            {
                clone.CopyFrom(planet);
            }

            clone.AllHosts = this.GetAllStars();
            bool? success = editor?.Invoke(clone);
            if (success == true)
            {
                if (planet != null)
                {
                    success = this.ApiEditPlanet(clone, true);
                }
                else
                {
                    success = this.ApiEditPlanet(clone, false);
                }
            }

            this.SendMessage(success == true);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.client.Dispose();
        }

        private void SendMessage(bool success)
        {
            string msg = success ? "Operation completed successfully" : "Operation failed";
            Messenger.Default.Send(msg, "PlanetResult");
        }

        private List<Data.Models.Star> GetAllStars()
        {
            string json = this.client.GetStringAsync(this.url + "stars").Result;
            var list = JsonSerializer.Deserialize<List<Data.Models.Star>>(json, this.jsonOptions);
            return list;
        }

        private bool ApiEditPlanet(VMPlanet planet, bool isEditing)
        {
            if (planet == null)
            {
                return false;
            }

            string myUrl = isEditing ? this.url + "update" : this.url + "add";

            Dictionary<string, string> postData = new Dictionary<string, string>();

            if (isEditing)
            {
                postData.Add(nameof(planet.Id), planet.Id.ToString());
            }

            postData.Add(nameof(planet.Name), planet.Name);
            postData.Add(nameof(planet.Distance), planet.Distance.ToString());
            postData.Add(nameof(planet.Mass), planet.Mass.ToString());
            postData.Add(nameof(planet.Diameter), planet.Diameter.ToString());
            postData.Add(nameof(planet.Molecules), planet.Molecules);
            postData.Add("HostId", planet.Host.Id.ToString());

            string json = this.client.PostAsync(myUrl, new FormUrlEncodedContent(postData)).
                Result.Content.ReadAsStringAsync().Result;

            JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.EnumerateObject().First().Value.GetRawText() == "true";
        }
    }
}
