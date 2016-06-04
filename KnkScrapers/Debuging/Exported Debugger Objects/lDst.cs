var lDst = new KnkSolutionMovies.Entities.Movie
{
	CreationDate = null,
	Deleted = false,
	DeletedDate = null,
	new KnkSolutionMovies.Extenders.MovieExtender
	{
		Plays = new System.Collections.Generic.List<KnkSolutionMovies.Entities.FilePlay>
		{
		}
	},
	IdMovie = null,
	IdSet = null,
	ImdbId = null,
	new System.Reflection.RuntimePropertyInfo
	{
		CustomAttributes = new System.Collections.ObjectModel.ReadOnlyCollection<System.Reflection.CustomAttributeData>
		{
		},
		m_otherMethod = null,
		m_parameters = null
	},
	MPARating = null,
	ModifiedDate = null,
	MovieSet = null,
	OriginalTitle = null,
	Rating = 0m,
	ReleaseDate = null,
	Seconds = null,
	Studio = null,
	TagLine = null,
	Title = null,
	TrailerUrl = null,
	UserCreationId = null,
	UserDeletedId = null,
	UserModifiedId = null,
	Votes = 0,
	Year = null,
	new KnkCore.KnkTableEntity
	{
		SourceTable = "vieMovies",
		TableBase = "Movies"
	},
	new KnkSolutionMovies.Lists.Movies
	{
		new KnkCore.KnkConnection
		{
		},
		Items = new System.Collections.Generic.List<KnkSolutionMovies.Entities.Movie>
		{
		},
		_Criteria = new KnkCore.KnkCriteria<KnkSolutionMovies.Entities.Movie, KnkSolutionMovies.Entities.Movie>
		{
			KnkLinkFields = null,
		},
		_List = new System.Collections.Generic.List<KnkSolutionMovies.Entities.Movie>
		{
		}
	},
	_primarykey = "IdMovie",
	_status = NoChanges
};
