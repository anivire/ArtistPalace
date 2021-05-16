use ArtistPalace;

go

create procedure AddToSuggestArtists
(
    @twitterTag varchar(150),
    @type varchar(150),
    @acceptCommissions bit,
    @pricePerHour int,
    @isAccepted bit,
    @isRejected bit,
    @artworkLink nvarchar(MAX)
)
as
insert into SuggestArtists(TwitterTag, Type, AcceptCommissions, PricePerHour, IsAccepted, IsRejected, ArtworkLink)
values (@twitterTag, @type, @acceptCommissions, @pricePerHour, @isAccepted, @isRejected, @artworkLink)