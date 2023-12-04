
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        // URL de la API
        string apiUrl = "https://rickandmortyapi.com/api/character/?name=summer";

        // Llamada a la API y recuperación de la respuesta
        string jsonResponse = await GetApiResponse(apiUrl);

        // Parsear el JSON y extraer datos
        if (!string.IsNullOrEmpty(jsonResponse))
        {
            try
            {
                // Utilizar Newtonsoft.Json para deserializar el JSON a un objeto C#
                var responserecibida = JsonConvert.DeserializeObject<ResponseRecibida>(jsonResponse);
                var character = responserecibida.getCharacter();

                //Imprimir el nombre y los episodios de Summer
                Console.WriteLine($"nombre: {character.name}");
                foreach(string episodio in character.episode) {
                  Console.WriteLine($"episodio: {episodio}");
                }
                Console.WriteLine(character.episode.Count);
                
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar JSON: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("No se recibió respuesta de la API");
        }
    }

    static async Task<string> GetApiResponse(string apiUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Realizar la solicitud HTTP GET a la API
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer y devolver la respuesta como cadena
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error en la solicitud: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
            }

            return null;
        }
    }
}

// Modelo de datos que representa la estructura del JSON

class ResponseRecibida
{
    public Info info { get; set; }
    public List<Character> results { get; set; }
    
    public Character getCharacter()
    {
        return results[0];
    }

}
class Info
{
    public int count { get; set; }
    public int pages { get; set; }
    public string? next { get; set; }
    public string? prev { get; set; }
}
class Character
{
    public int id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string species { get; set; }
    public string type { get; set; }
    public string gender { get; set; }
    public Lugar origin { get; set; }
    public Lugar location { get; set; }
    public string image {  get; set; }
    public List<String> episode { get; set; }
    public string url { get; set; }
    public string created { get; set; }

}
class Lugar
{
    public string name { get; set; }
    public string url { get; set; }
}
