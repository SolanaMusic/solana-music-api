USE [solana-music];
GO

-- SubscriptionPlans
SET IDENTITY_INSERT dbo.SubscriptionPlans ON;
IF NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlans WHERE Id = 1)
BEGIN
    INSERT INTO dbo.SubscriptionPlans ([Id], [Name], [DurationInMonths], [Type], [MaxMembers], [TokensMultiplier], [CreatedDate], [UpdatedDate])
    VALUES (1, 'Basic Plan', '1', 1, 1, 1, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlans WHERE Id = 2)
BEGIN
    INSERT INTO dbo.SubscriptionPlans ([Id], [Name], [DurationInMonths], [Type], [MaxMembers], [TokensMultiplier], [CreatedDate], [UpdatedDate])
    VALUES (2, 'Premium Plan', '1', 1, 1, 1.5, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlans WHERE Id = 3)
BEGIN
    INSERT INTO dbo.SubscriptionPlans ([Id], [Name], [DurationInMonths], [Type], [MaxMembers], [TokensMultiplier], [CreatedDate], [UpdatedDate])
    VALUES (3, 'Family Plan', '1', 2, 5, 1, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.SubscriptionPlans OFF;

-- Countries
SET IDENTITY_INSERT dbo.Countries ON;
IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 1)
BEGIN
    INSERT INTO dbo.Countries ([Id], [Name], [CountryCode], [CreatedDate], [UpdatedDate])
    VALUES (1, 'United States', 'USA', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 2)
BEGIN
    INSERT INTO dbo.Countries ([Id], [Name], [CountryCode], [CreatedDate], [UpdatedDate])
    VALUES (2, 'Ukraine', 'UA', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Countries WHERE Id = 3)
BEGIN
    INSERT INTO dbo.Countries ([Id], [Name], [CountryCode], [CreatedDate], [UpdatedDate])
    VALUES (3, 'Austria', 'AT', GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.Countries OFF;


-- Genres
SET IDENTITY_INSERT dbo.Genres ON;
IF NOT EXISTS (SELECT 1 FROM dbo.Genres WHERE Id = 1)
BEGIN
    INSERT INTO dbo.Genres ([Id], [Name], [CreatedDate], [UpdatedDate])
    VALUES (1, 'Classic', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Genres WHERE Id = 2)
BEGIN
    INSERT INTO dbo.Genres ([Id], [Name], [CreatedDate], [UpdatedDate])
    VALUES (2, 'Rock', GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.Genres OFF;


-- Artists
IF NOT EXISTS (SELECT 1 FROM dbo.Artists WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Artists ON;
    INSERT INTO dbo.Artists ([Id], [UserId], [Name], [CountryId], [Bio], [ImageUrl], [CreatedDate], [UpdatedDate])
    VALUES (1, 2, 'John Doe', 1, 'An emerging artist in the rock music scene.', 'images\artists\artist-image1.jpg', GETDATE(), GETDATE());
    SET IDENTITY_INSERT dbo.Artists OFF;
END

-- Albums
IF NOT EXISTS (SELECT 1 FROM dbo.Albums WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Albums ON;
    INSERT INTO dbo.Albums ([Id], [Title], [ReleaseDate], [ImageUrl], [Description], [CreatedDate], [UpdatedDate])
    VALUES (1, 'Rock Classics', '2024-02-26', 'covers\albums\album-image1.jpg',
            'A collection of the best rock hits', GETDATE(), GETDATE());
    SET IDENTITY_INSERT dbo.Albums OFF;
END


-- Tracks
SET IDENTITY_INSERT dbo.Tracks ON;
IF NOT EXISTS (SELECT 1 FROM dbo.Tracks WHERE Id = 1)
BEGIN
    INSERT INTO dbo.Tracks ([Id], [Title], [AlbumId], [ImageUrl], [Duration],
        [FileUrl], [PlaysCount], [ReleaseDate], [CreatedDate], [UpdatedDate])
    VALUES (1, 'Test track', NULL, 'covers\tracks\track-image1.png',
        '00:03:28', 'tracks\track1.mp3', 0, '2024-02-26', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Tracks WHERE Id = 2)
BEGIN
    INSERT INTO dbo.Tracks ([Id], [Title], [AlbumId], [ImageUrl], [Duration],
        [FileUrl], [PlaysCount], [ReleaseDate], [CreatedDate], [UpdatedDate])
    VALUES (2, 'Album track 1', 1, null,
        '00:01:40', 'tracks\album-track1.mp3', 0, '2024-02-26', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Tracks WHERE Id = 3)
BEGIN
    INSERT INTO dbo.Tracks ([Id], [Title], [AlbumId], [ImageUrl], [Duration],
        [FileUrl], [PlaysCount], [ReleaseDate], [CreatedDate], [UpdatedDate])
    VALUES (3, 'Album track 2', 1, null,
        '00:01:17', 'tracks\album-track2.mp3', 0, '2024-02-26', GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.Tracks OFF;


-- TrackGenres
SET IDENTITY_INSERT dbo.TrackGenres ON;
IF NOT EXISTS (SELECT 1 FROM dbo.TrackGenres WHERE TrackId = 1 AND GenreId = 1)
BEGIN
    INSERT INTO dbo.TrackGenres ([Id], [TrackId], [GenreId], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.TrackGenres WHERE TrackId = 2 AND GenreId = 2)
BEGIN
    INSERT INTO dbo.TrackGenres ([Id], [TrackId], [GenreId], [CreatedDate], [UpdatedDate])
    VALUES (2, 2, 2, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.TrackGenres WHERE TrackId = 3 AND GenreId = 2)
BEGIN
    INSERT INTO dbo.TrackGenres ([Id], [TrackId], [GenreId], [CreatedDate], [UpdatedDate])
    VALUES (3, 3, 2, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.TrackGenres OFF;


-- ArtistTracks
SET IDENTITY_INSERT dbo.ArtistTracks ON;
IF NOT EXISTS (SELECT 1 FROM dbo.ArtistTracks WHERE ArtistId = 1 AND TrackId = 1)
BEGIN
    INSERT INTO dbo.ArtistTracks ([Id], [ArtistId], [TrackId], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.ArtistTracks WHERE ArtistId = 1 AND TrackId = 2)
BEGIN
    INSERT INTO dbo.ArtistTracks ([Id], [ArtistId], [TrackId], [CreatedDate], [UpdatedDate])
    VALUES (2, 1, 2, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.ArtistTracks WHERE ArtistId = 1 AND TrackId = 3)
BEGIN
    INSERT INTO dbo.ArtistTracks ([Id], [ArtistId], [TrackId], [CreatedDate], [UpdatedDate])
    VALUES (3, 1, 3, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.ArtistTracks OFF;


-- ArtistAlbums
SET IDENTITY_INSERT dbo.ArtistAlbums ON;
IF NOT EXISTS (SELECT 1 FROM dbo.ArtistAlbums WHERE AlbumId = 1 AND ArtistId = 1)
BEGIN
    INSERT INTO dbo.ArtistAlbums ([Id], [AlbumId], [ArtistId], [CreatedDate] ,[UpdatedDate])
    VALUES (1, 1, 1, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.ArtistAlbums OFF;


-- ArtistSubscribers
IF NOT EXISTS (SELECT 1 FROM dbo.ArtistSubscribers WHERE ArtistId = 1 AND SubscriberId = 1)
BEGIN
    SET IDENTITY_INSERT dbo.ArtistSubscribers ON;
    INSERT INTO dbo.ArtistSubscribers ([Id], [ArtistId], [SubscriberId], [CreatedDate] ,[UpdatedDate])
    VALUES (1, 1, 1, GETDATE(), GETDATE());
    SET IDENTITY_INSERT dbo.ArtistSubscribers OFF;
END


-- Playlists
IF EXISTS (SELECT 1 FROM dbo.AspNetUsers WHERE Id = 1) AND NOT EXISTS (SELECT 1 FROM dbo.Playlists WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Playlists ON;
    INSERT INTO dbo.Playlists ([Id], [Name], [OwnerId], [CoverUrl], [CreatedDate], [UpdatedDate])
    VALUES (1, 'My Favorite Songs', 1, 'covers\playlists\playlist-image1.jpg', GETDATE(), GETDATE());
    SET IDENTITY_INSERT dbo.Playlists OFF;
END


-- PlaylistTracks
SET IDENTITY_INSERT dbo.PlaylistTracks ON;
IF EXISTS (SELECT 1 FROM dbo.Playlists WHERE Id = 1) AND EXISTS (SELECT 1 FROM dbo.Tracks WHERE Id = 1)
   AND NOT EXISTS (SELECT 1 FROM dbo.PlaylistTracks WHERE PlaylistId = 1 AND TrackId = 1)
BEGIN
    INSERT INTO dbo.PlaylistTracks ([Id], [PlaylistId], [TrackId], [CreatedDate] ,[UpdatedDate])
    VALUES (1, 1, 1, GETDATE(), GETDATE());
END

IF EXISTS (SELECT 1 FROM dbo.Playlists WHERE Id = 1) AND EXISTS (SELECT 1 FROM dbo.Tracks WHERE Id = 3)
   AND NOT EXISTS (SELECT 1 FROM dbo.PlaylistTracks WHERE PlaylistId = 1 AND TrackId = 3)
BEGIN
    INSERT INTO dbo.PlaylistTracks ([Id], [PlaylistId], [TrackId], [CreatedDate] ,[UpdatedDate])
    VALUES (2, 1, 3, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.PlaylistTracks OFF;


-- UserProfiles
SET IDENTITY_INSERT dbo.UserProfiles ON;
IF NOT EXISTS (SELECT 1 FROM dbo.UserProfiles WHERE Id = 1)
BEGIN
    INSERT INTO dbo.UserProfiles ([Id], [UserId], [CountryId], [AvatarUrl], [TokensAmount], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, 'images\users\user-image1.png', 100, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.UserProfiles WHERE Id = 2)
BEGIN
    INSERT INTO dbo.UserProfiles ([Id], [UserId], [CountryId], [AvatarUrl], [TokensAmount], [CreatedDate], [UpdatedDate])
    VALUES (2, 2, 1, 'images\users\user-image1.png', 200, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.UserProfiles WHERE Id = 3)
BEGIN
    INSERT INTO dbo.UserProfiles ([Id], [UserId], [CountryId], [AvatarUrl], [TokensAmount], [CreatedDate], [UpdatedDate])
    VALUES (3, 3, 1, 'images\users\user-image1.png', 300, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.UserProfiles WHERE Id = 4)
BEGIN
    INSERT INTO dbo.UserProfiles ([Id], [UserId], [CountryId], [AvatarUrl], [TokensAmount], [CreatedDate], [UpdatedDate])
    VALUES (4, 4, 1, 'images\users\user-image1.png', 400, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.UserProfiles OFF;


-- Currencies
SET IDENTITY_INSERT dbo.Currencies ON;
IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Id = 1)
BEGIN
    INSERT INTO dbo.Currencies ([Id], [Code], [Symbol], [CreatedDate], [UpdatedDate])
    VALUES (1, 'USD', '$', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Id = 2)
BEGIN
    INSERT INTO dbo.Currencies ([Id], [Code], [Symbol], [CreatedDate], [UpdatedDate])
    VALUES (2, 'SOL', 'SOL', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Currencies WHERE Id = 3)
BEGIN
    INSERT INTO dbo.Currencies ([Id], [Code], [Symbol], [CreatedDate], [UpdatedDate])
    VALUES (3, 'TUNE', 'TUNE', GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.Currencies OFF;


-- SubscriptionPlanCurrency
SET IDENTITY_INSERT dbo.SubscriptionPlanCurrencies ON;
IF NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlanCurrencies WHERE Id = 1)
BEGIN
    INSERT INTO dbo.SubscriptionPlanCurrencies ([Id], [SubscriptionPlanId], [CurrencyId], [Price], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, 4.99, GETDATE(), GETDATE());

    INSERT INTO dbo.SubscriptionPlanCurrencies ([Id], [SubscriptionPlanId], [CurrencyId], [Price], [CreatedDate], [UpdatedDate])
    VALUES (2, 1, 3, 500, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlanCurrencies WHERE Id = 2)
BEGIN
    INSERT INTO dbo.SubscriptionPlanCurrencies ([Id], [SubscriptionPlanId], [CurrencyId], [Price], [CreatedDate], [UpdatedDate])
    VALUES (3, 2, 1, 9.99, GETDATE(), GETDATE());

    INSERT INTO dbo.SubscriptionPlanCurrencies ([Id], [SubscriptionPlanId], [CurrencyId], [Price], [CreatedDate], [UpdatedDate])
    VALUES (4, 2, 3, 1000, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.SubscriptionPlanCurrencies WHERE Id = 3)
BEGIN
    INSERT INTO dbo.SubscriptionPlanCurrencies ([Id], [SubscriptionPlanId], [CurrencyId], [Price], [CreatedDate], [UpdatedDate])
    VALUES (5, 3, 1, 14.99, GETDATE(), GETDATE());

    INSERT INTO dbo.SubscriptionPlanCurrencies ([Id], [SubscriptionPlanId], [CurrencyId], [Price], [CreatedDate], [UpdatedDate])
    VALUES (6, 3, 3, 1500, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.SubscriptionPlanCurrencies OFF;


-- Subscriptions
SET IDENTITY_INSERT dbo.Subscriptions ON;
IF NOT EXISTS (SELECT 1 FROM dbo.Subscriptions WHERE Id = 1)
BEGIN
    INSERT INTO dbo.Subscriptions ([Id], [OwnerId], [SubscriptionPlanId], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Subscriptions WHERE Id = 2)
BEGIN
    INSERT INTO dbo.Subscriptions ([Id], [OwnerId], [SubscriptionPlanId], [CreatedDate], [UpdatedDate])
    VALUES (2, 4, 3, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.Subscriptions OFF;


-- UserSubscriptions
SET IDENTITY_INSERT dbo.UserSubscriptions ON;
IF NOT EXISTS (SELECT 1 FROM dbo.UserSubscriptions WHERE UserId = 1 AND SubscriptionId = 1)
BEGIN
    INSERT INTO dbo.UserSubscriptions ([Id], [UserId], [SubscriptionId], [IsActive], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, 1, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.UserSubscriptions WHERE UserId = 3 AND SubscriptionId = 2)
BEGIN
    INSERT INTO dbo.UserSubscriptions ([Id], [UserId], [SubscriptionId], [IsActive], [CreatedDate], [UpdatedDate])
    VALUES (2, 3, 2, 1, GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.UserSubscriptions WHERE UserId = 4 AND SubscriptionId = 2)
BEGIN
    INSERT INTO dbo.UserSubscriptions ([Id], [UserId], [SubscriptionId], [IsActive], [CreatedDate], [UpdatedDate])
    VALUES (3, 4, 2, 1, GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.UserSubscriptions OFF;


-- Transactions
IF EXISTS (SELECT 1 FROM dbo.AspNetUsers WHERE Id = 1) AND NOT EXISTS (SELECT 1 FROM dbo.Transactions WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Transactions ON;
    INSERT INTO dbo.Transactions ([Id], [UserId], [CurrencyId], [Amount], [Status], [TransactionType], [PaymentMethod], [CreatedDate], [UpdatedDate])
    VALUES (1, 1, 1, 14.99, 2, 1, 1, GETDATE(), GETDATE());
    SET IDENTITY_INSERT dbo.Transactions OFF;
END


-- NftCollection
SET IDENTITY_INSERT dbo.NftCollections ON;
IF NOT EXISTS (SELECT 1 FROM dbo.NftCollections WHERE Id = 1)
BEGIN
INSERT INTO dbo.NftCollections ([Id], [Name], [Supply], [Address], [AssociationId], [AssociationType], [ImageUrl], [CreatedDate], [UpdatedDate])
VALUES (1, 'Dev Collection', 5, '2k7pY8DyNxcDJTatztZPMRTp4G8jStnj8eRa8Ri6VtQR', 1, 0,
        'https://gateway.irys.xyz/9Y59MVJMgvLUcmxAiSv9xWaREGMTpntQuobhgPDoUtA1', GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.NftCollections OFF;


-- Nfts
SET IDENTITY_INSERT dbo.Nfts ON;
IF NOT EXISTS (SELECT 1 FROM dbo.Nfts WHERE Id = 1)
BEGIN
INSERT INTO dbo.Nfts ([Id], [Name], [CollectionId], [Address], [Owner], [Price], [CurrencyId], [Rarity], [ImageUrl], [CreatedDate], [UpdatedDate])
VALUES (1, 'Dev NFT #0', 1, '3BBqZLWqGutpyUn1tsEuJD7qjm3NbNp6vgMyhoDCLEyX', '7eJ1xj1EKK3nZPP4AkUHeMikD2L2Q7HMMZPPb5uhQ1zq', 2, 2, 4,
           'https://gateway.irys.xyz/27bJ1JmEoBQL2dbHSyu4PFRkfxvmzh9zunmLKfxURFn8', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Nfts WHERE Id = 2)
BEGIN
INSERT INTO dbo.Nfts ([Id], [Name], [CollectionId], [Address], [Owner], [Price], [CurrencyId], [Rarity], [ImageUrl], [CreatedDate], [UpdatedDate])
VALUES (2, 'Dev NFT #1', 1, '6LWxG6oCo8oF6jHJmQfdjRAD6zswfJrUtyoRB5fe19oV', '7eJ1xj1EKK3nZPP4AkUHeMikD2L2Q7HMMZPPb5uhQ1zq', 2, 2, 4, 
        'https://gateway.irys.xyz/Hu2zUP9nyDpVn9yzWnPH8TqqVaLeFago13VCk6aJewPz', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Nfts WHERE Id = 3)
BEGIN
INSERT INTO dbo.Nfts ([Id], [Name], [CollectionId], [Address], [Owner], [Price], [CurrencyId], [Rarity], [ImageUrl], [CreatedDate], [UpdatedDate])
VALUES (3, 'Dev NFT #2', 1, '5VaBqrDmSxqPrfUixZcn1cmL9K7dN5N8o5c5vJc4eNQn', '7eJ1xj1EKK3nZPP4AkUHeMikD2L2Q7HMMZPPb5uhQ1zq', 2, 2, 4, 
        'https://gateway.irys.xyz/EuiQEr82w7bThrqwp6S4agykJW1qzt1QrEWJLr2MU9mg', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Nfts WHERE Id = 4)
BEGIN
INSERT INTO dbo.Nfts ([Id], [Name], [CollectionId], [Address], [Owner], [Price], [CurrencyId], [Rarity], [ImageUrl], [CreatedDate], [UpdatedDate])
VALUES (4, 'Dev NFT #3', 1, 'AnzZZDcoxWn9wZitJTyYn4a9rhUUKDEtbVrdandSzynB', '7eJ1xj1EKK3nZPP4AkUHeMikD2L2Q7HMMZPPb5uhQ1zq', 2, 2, 4, 
        'https://gateway.irys.xyz/22AAY3pnQWHZZSatw4fdjHpRu2n5vTjubxyaLTs2p4tR', GETDATE(), GETDATE());
END

IF NOT EXISTS (SELECT 1 FROM dbo.Nfts WHERE Id = 5)
BEGIN
INSERT INTO dbo.Nfts ([Id], [Name], [CollectionId], [Address], [Owner], [Price], [CurrencyId], [Rarity], [ImageUrl], [CreatedDate], [UpdatedDate])
VALUES (5, 'Dev NFT #4', 1, 'CFBdVoP1KiVP3TrkxrkGQRozkJxovwGWoupDtPoziLPP', '7eJ1xj1EKK3nZPP4AkUHeMikD2L2Q7HMMZPPb5uhQ1zq', 2, 2, 4, 
        'https://gateway.irys.xyz/3YB21AWWE6b6YDhksYVVMtY8mrqH48jWb6HRmKckYHq5', GETDATE(), GETDATE());
END
SET IDENTITY_INSERT dbo.Nfts OFF;