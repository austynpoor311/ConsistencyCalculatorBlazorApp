// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConsistencyCalculator.Models
{
    public class Option
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("displayValue")]
        public string DisplayValue { get; set; }
    }

    public class Filter
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("options")]
        public List<Option> Options { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("rel")]
        public List<string> Rel { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("shortText")]
        public string ShortText { get; set; }

        [JsonPropertyName("isExternal")]
        public bool IsExternal { get; set; }

        [JsonPropertyName("isPremium")]
        public bool IsPremium { get; set; }
    }

    public class Opponent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }

    public class Team
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("isAllStar")]
        public bool IsAllStar { get; set; }
    }

    public class Event
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }

        [JsonPropertyName("atVs")]
        public string AtVs { get; set; }

        [JsonPropertyName("gameDate")]
        public DateTime GameDate { get; set; }

        [JsonPropertyName("score")]
        public string Score { get; set; }

        [JsonPropertyName("homeTeamId")]
        public string HomeTeamId { get; set; }

        [JsonPropertyName("awayTeamId")]
        public string AwayTeamId { get; set; }

        [JsonPropertyName("homeTeamScore")]
        public string HomeTeamScore { get; set; }

        [JsonPropertyName("awayTeamScore")]
        public string AwayTeamScore { get; set; }

        [JsonPropertyName("gameResult")]
        public string GameResult { get; set; }

        [JsonPropertyName("opponent")]
        public Opponent Opponent { get; set; }

        [JsonPropertyName("leagueName")]
        public string LeagueName { get; set; }

        [JsonPropertyName("leagueAbbreviation")]
        public string LeagueAbbreviation { get; set; }

        [JsonPropertyName("leagueShortName")]
        public string LeagueShortName { get; set; }

        [JsonPropertyName("team")]
        public Team Team { get; set; }
    }


    public class Events
    {
        public List<Event> Games { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("splitType")]
        public string SplitType { get; set; }

        [JsonPropertyName("events")]
        public List<CategoryEvent> Events { get; set; }

        [JsonPropertyName("totals")]
        public List<string> Totals { get; set; }
    }

    public class CategoryEvent
    {
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }

        [JsonPropertyName("stats")]
        public List<string> Stats { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("stats")]
        public List<string> Stats { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Summary
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("stats")]
        public List<Stat> Stats { get; set; }
    }

    public class SeasonType
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("categories")]
        public List<Category> Categories { get; set; }

        [JsonPropertyName("summary")]
        public Summary Summary { get; set; }
    }

    public class Glossary
    {
        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    public class PlayerStatistics
    {
        [JsonPropertyName("filters")]
        public List<Filter> Filters { get; set; }

        [JsonPropertyName("labels")]
        public List<string> Labels { get; set; }

        [JsonPropertyName("names")]
        public List<string> Names { get; set; }

        [JsonPropertyName("displayNames")]
        public List<string> DisplayNames { get; set; }

        [JsonPropertyName("events")]
        public Events Events { get; set; }

        [JsonPropertyName("seasonTypes")]
        public List<SeasonType> SeasonTypes { get; set; }

        [JsonPropertyName("glossary")]
        public List<Glossary> Glossary { get; set; }
    }
}
