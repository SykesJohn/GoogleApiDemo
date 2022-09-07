using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.Common;

namespace GoogleApiDemoWASM.Pages;


public partial class AutoComplete
	{
	public AutoComplete()
		{
		}

	//[Inject]
	//private GoogleApi.GooglePlaces.AutoCompleteApi autoComplete { get; set; } = default!;

	protected override void OnParametersSet()
		{
		base.OnParametersSet();
		}

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
		//var list = await autoComplete.QueryAsync(req);
		var list = await autoComplete.QueryAsync(req).ConfigureAwait(false);
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