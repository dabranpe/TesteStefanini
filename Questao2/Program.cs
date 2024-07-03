using Newtonsoft.Json;
using Questao2;
using System.Net.Http;
using System.Text;
using static System.Net.WebRequestMethods;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        return getTeam1Goals(team, year) + getTeam2Goals(team, year);
    }

    private static int getTeam1Goals(string team, int year, int page = 1)
    {
        var rank = getRank(team, year, 1, page);

        var total = rank.data.Sum(x => x.team1goals);

        if (page < rank.total_pages)            
            total += getTeam1Goals(team, year, page+1);

        return total;

    }

    private static int getTeam2Goals(string team, int year, int page = 1)
    {        
        var rank = getRank(team, year, 2 ,page);

        var total = rank.data.Sum(x => x.team2goals);

        if (page < rank.total_pages)
            total += getTeam2Goals(team, year, page + 1);

        return total;
    }

    private static Rank getRank(string team, int year, int type, int page = 1)
    {
        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{type}={team}&page={page}";

        HttpClient _httpClient = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

        var response = _httpClient.SendAsync(request).Result;
        var result = response.Content.ReadAsStringAsync().Result;

        var rank = JsonConvert.DeserializeObject<Rank>(result);

        return rank;

    }

}