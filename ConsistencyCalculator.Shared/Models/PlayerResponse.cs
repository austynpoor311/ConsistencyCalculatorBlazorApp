using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsistencyCalculator.Models
{
    public class AlternateIds
    {
        [JsonPropertyName("sdr")]
        public string Sdr { get; set; }
    }

    public class PlayerLink
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

    public class BirthPlace
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

    public class College
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Headshot
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("alt")]
        public string Alt { get; set; }
    }

    public class Position
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("leaf")]
        public bool Leaf { get; set; }
    }

    public class PlayerTeam
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Team2
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Statistics
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Notes
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Contracts
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Experience
    {
        [JsonPropertyName("years")]
        public int Years { get; set; }
    }

    public class CollegeAthlete
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class BaseYearCompensation
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

    public class PoisonPillProvision
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

    public class Season
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class TradeKicker
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("percentage")]
        public double Percentage { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("tradeValue")]
        public int TradeValue { get; set; }
    }

    public class Contract
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }

        [JsonPropertyName("birdStatus")]
        public int BirdStatus { get; set; }

        [JsonPropertyName("baseYearCompensation")]
        public BaseYearCompensation BaseYearCompensation { get; set; }

        [JsonPropertyName("poisonPillProvision")]
        public PoisonPillProvision PoisonPillProvision { get; set; }

        [JsonPropertyName("incomingTradeValue")]
        public int IncomingTradeValue { get; set; }

        [JsonPropertyName("outgoingTradeValue")]
        public int OutgoingTradeValue { get; set; }

        [JsonPropertyName("minimumSalaryException")]
        public bool MinimumSalaryException { get; set; }

        [JsonPropertyName("optionType")]
        public int OptionType { get; set; }

        [JsonPropertyName("salary")]
        public int Salary { get; set; }

        [JsonPropertyName("salaryRemaining")]
        public int SalaryRemaining { get; set; }

        [JsonPropertyName("yearsRemaining")]
        public int YearsRemaining { get; set; }

        [JsonPropertyName("season")]
        public Season Season { get; set; }

        [JsonPropertyName("team")]
        public Team Team { get; set; }

        [JsonPropertyName("tradeKicker")]
        public TradeKicker TradeKicker { get; set; }

        [JsonPropertyName("tradeRestriction")]
        public bool TradeRestriction { get; set; }

        [JsonPropertyName("unsignedForeignPick")]
        public bool UnsignedForeignPick { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

    public class EventLog
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
    }

    public class Draft
    {
        [JsonPropertyName("displayText")]
        public string DisplayText { get; set; }

        [JsonPropertyName("round")]
        public int Round { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("selection")]
        public int Selection { get; set; }

        [JsonPropertyName("team")]
        public Team Team { get; set; }
    }

    public class Status
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }
    }

    public class PlayerResponse
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("alternateIds")]
        public AlternateIds AlternateIds { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("displayWeight")]
        public string DisplayWeight { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("displayHeight")]
        public string DisplayHeight { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("links")]
        public List<PlayerLink> Links { get; set; }

        [JsonPropertyName("birthPlace")]
        public BirthPlace BirthPlace { get; set; }

        [JsonPropertyName("college")]
        public College College { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("headshot")]
        public Headshot Headshot { get; set; }

        [JsonPropertyName("jersey")]
        public string Jersey { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }

        [JsonPropertyName("injuries")]
        public List<InjuryResponse> Injuries { get; set; }

        [JsonPropertyName("linked")]
        public bool Linked { get; set; }

        [JsonPropertyName("team")]
        public Team Team { get; set; }

        [JsonPropertyName("teams")]
        public List<PlayerTeam> Teams { get; set; }

        [JsonPropertyName("statistics")]
        public Statistics Statistics { get; set; }

        [JsonPropertyName("notes")]
        public Notes Notes { get; set; }

        [JsonPropertyName("contracts")]
        public Contracts Contracts { get; set; }

        [JsonPropertyName("experience")]
        public Experience Experience { get; set; }

        [JsonPropertyName("collegeAthlete")]
        public CollegeAthlete CollegeAthlete { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("contract")]
        public Contract Contract { get; set; }

        [JsonPropertyName("eventLog")]
        public EventLog EventLog { get; set; }

        [JsonPropertyName("draft")]
        public Draft Draft { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }
    }
}

