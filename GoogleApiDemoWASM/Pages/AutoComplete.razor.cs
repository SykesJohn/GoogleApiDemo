using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.AutoComplete.Response;
using GoogleApi.Entities.Places.Common;
using Org.BouncyCastle.Asn1.X509;
using System.Globalization;

namespace GoogleApiDemoWASM.Pages;

public partial class AutoComplete
	{
	public static string GoogleApiKey;
	public List<Prediction> AddressOptions { get; protected set; } = new();
	public string SelectedOptionId { get; set; } = "";
	public Prediction? SelectedOption => AddressOptions != null ? AddressOptions.FirstOrDefault(o => o.PlaceId == SelectedOptionId) : null;


	public async Task<List<Prediction>> GetOptions(string Address)
		{
		AddressOptions.Clear();
		PlacesAutoCompleteRequest req = new()
			{
			Input=Address,
			Language=GoogleApi.Entities.Common.Enums.Language.English,
			Key=GoogleApiKey,
			Components=new List<KeyValuePair<GoogleApi.Entities.Common.Enums.Component, string>>()
				{
				new KeyValuePair<GoogleApi.Entities.Common.Enums.Component, string>(GoogleApi.Entities.Common.Enums.Component.Country, "ca"),
				new KeyValuePair<GoogleApi.Entities.Common.Enums.Component, string>(GoogleApi.Entities.Common.Enums.Component.Country, "us")
				}
			};
		var list = await GoogleApi.GooglePlaces.AutoComplete.QueryAsync(req);
		if(list == null || list.Predictions == null)
			return AddressOptions;
		if(list.Status != GoogleApi.Entities.Common.Enums.Status.Ok)
			return AddressOptions; //TODO: error indication
		foreach(var a in list.Predictions)
			AddressOptions.Add(a);
		return AddressOptions;
		}

	public async Task PredictionSelected(string Selected)
		{
		SelectedOptionId=Selected;
		}

	private async Task OnDestinationChanged()
		{
		try
			{
			}
		catch(Exception Ex)
			{

			}

		}
	}