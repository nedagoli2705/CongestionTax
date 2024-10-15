using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class HolidayService
{
    private readonly HttpClient _httpClient;

    public HolidayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public virtual async Task<List<Holiday>> GetHolidaysAsync(int year, string countryCode)
    {
        var url = $"https://holidayapi.com/v1/holidays?year={year}&country={countryCode}&key=b386e74e-07b2-470d-b30e-a875883560c3";
        var response = await _httpClient.GetAsync(url/*$"https://holidayapi.com/v1/holidays?pretty&key=b386e74e-07b2-470d-b30e-a875883560c3&country={countryCode}&year={year}"*/);

        if (response.IsSuccessStatusCode)
        {
            var holidaysResponse = await response.Content.ReadFromJsonAsync<HolidaysResponse>();
            return holidaysResponse.Holidays;
        }

        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error fetching holidays. Status Code: {response.StatusCode}, Response: {errorContent}");
        }
    }
}

public class HolidaysResponse
{
    public List<Holiday> Holidays { get; set; }
}
public class Holiday
{
    public string Name { get; set; }
    public string Date { get; set; } // Keep it as a string initially
}
