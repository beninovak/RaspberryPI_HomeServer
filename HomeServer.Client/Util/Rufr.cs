using System;
using System.Net.Http;
using System.Threading.Tasks;
using HomeServer.Core;

namespace HomeServer.Client.Util;

public static class Rufr {
    private static HttpClient httpClient;

    static Rufr() {
        httpClient = new HttpClient() {
            BaseAddress = new Uri(Constants.TEST_URL),
        };
    }

    public static async Task<string> MakeRequest(string path) {
        using (HttpResponseMessage response = await httpClient.GetAsync(path)) {
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadAsStringAsync();
            } else {
                
            }
        }

        return "Bl bula brt!";
    }
}